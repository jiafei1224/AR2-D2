using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.iOS;

public class DepthMap : MonoBehaviour
{
    // public RawImage rawImage;
    public float captureRate = 1f; // capture rate in seconds

    private bool captureNextFrame = false;
    private int captureWidth;
    private int captureHeight;

    private void Start()
    {
        // Set the camera to render depth textures
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        captureWidth = Screen.width;
        captureHeight = Screen.height;

        // Start capturing frames
        StartCoroutine(CaptureFrames());
    }

    private IEnumerator CaptureFrames()
    {
        while (true)
        {
            // Wait for the specified time between frames
            yield return new WaitForSeconds(1f / captureRate);

            // Set the capture trigger to true
            captureNextFrame = true;
        }
    }

    private void OnPostRender()
    {
        if (captureNextFrame)
        {
            // Read the camera's depth texture into a RenderTexture object
            RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 0, RenderTextureFormat.ARGB32);
            GetComponent<Camera>().targetTexture = rt;
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;

            // Create a Texture2D object and read the pixels from the RenderTexture object
            Texture2D texture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
            texture.Apply();

            // Convert the depth values to grayscale and encode the texture as a PNG file
            Texture2D depthTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.Alpha8, false);
            for (int x = 0; x < captureWidth; x++)
            {
                for (int y = 0; y < captureHeight; y++)
                {
                    Color color = texture.GetPixel(x, y);
                    float depth = color.r + color.g * 256f + color.b * 256f * 256f;
                    depth /= 16777216f;
                    depthTexture.SetPixel(x, y, new Color(depth, depth, depth, 1f));
                }
            }
            depthTexture.Apply();
            byte[] bytes = depthTexture.EncodeToPNG();

            // Save the image to the photo library
            string filename = "ARCapture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string filePath = Application.persistentDataPath + "/" + filename;
            NativeGallery.SaveImageToGallery(bytes, "MyGallery", filename);

            // Cleanup
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            Destroy(texture);
            Destroy(depthTexture);

            // Reset capture trigger
            captureNextFrame = false;

            // Log file path for debugging
            Debug.Log("AR capture saved to gallery: " + filename);
        }
    }
}
