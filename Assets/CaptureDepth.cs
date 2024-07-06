using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.iOS;

public class CaptureDepth : MonoBehaviour
{
    // Set the desired FPS for capturing frames
    public int captureFPS = 1;

    // Set the RawImage to capture frames from
    public RawImage rawImage;

    // Start is called before the first frame update
    private bool captureNextFrame = false;

    private int captureWidth = 1668;
    private int captureHeight = 2388;

    private void Start()
    {
        // Start the coroutine for capturing frames at the desired FPS
        StartCoroutine(CaptureFrames());
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();

    }

    private IEnumerator CaptureFrames()
    {
        while (true)
        {
            // Wait for the specified time between frames
            yield return new WaitForSeconds(1f / captureFPS);

            // Set the capture trigger to true
            captureNextFrame = true;
        }
    }

    private void OnPostRender()
    {
        // Check if capture is needed
        if (captureNextFrame)
        {
            // Create a new texture to hold the captured image
            Texture2D texture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

            // Read pixels from the RawImage into the texture
            Rect rawImageRect = rawImage.rectTransform.rect;
            Vector2 rawImagePos = rawImage.rectTransform.position;
            texture.ReadPixels(new Rect(rawImagePos.x, rawImagePos.y, rawImageRect.width, rawImageRect.height), 0, 0);

            // Apply the changes to the texture
            texture.Apply();

            // Save the texture as a PNG image
            byte[] bytes = texture.EncodeToPNG();
            string filename = "ARCapture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string filePath = Application.persistentDataPath + "/" + filename;

            // Save the image to the photo library
            NativeGallery.SaveImageToGallery(bytes, "MyGallery", filename);

            // Cleanup
            Destroy(texture);

            // Reset capture trigger
            captureNextFrame = false;

            // Log file path for debugging
            Debug.Log("AR capture saved to gallery: " + filename);
        }
    }
}
