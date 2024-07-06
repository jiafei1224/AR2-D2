using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using NativeGalleryNamespace;
using Unity.Collections;
public class SaveDepthView : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public int frameRate = 24;
    private float timeSinceLastFrame = 0;
    private bool startSaving = true;

    private void Update()
    {
        if (startSaving)
        {
            timeSinceLastFrame += Time.deltaTime;

            if (timeSinceLastFrame >= 1f / frameRate)
            {
                StartCoroutine(SaveDepthImage());
                timeSinceLastFrame = 0;
            }
        }
    }

    public void StartSaving()
    {
        startSaving = true;
    }

    private IEnumerator SaveDepthImage()
    {
        yield return new WaitForEndOfFrame();

        XRCpuImage cpuImage;
        if (arCameraManager.TryAcquireLatestCpuImage(out cpuImage))
        {
            using (cpuImage)
            {
                Texture2D depthTexture = new Texture2D(cpuImage.width, cpuImage.height, TextureFormat.R16, false);
                cpuImage.Convert(new XRCpuImage.ConversionParams
                {
                    inputRect = new RectInt(0, 0, cpuImage.width, cpuImage.height),
                    outputDimensions = new Vector2Int(cpuImage.width, cpuImage.height),
                    outputFormat = TextureFormat.R16,
                    transformation = XRCpuImage.Transformation.None
                }, depthTexture.GetRawTextureData<byte>());

                depthTexture.Apply();

                Texture2D colorTexture = ConvertDepthToColor(depthTexture);

                byte[] bytes = colorTexture.EncodeToPNG();

                string filename = "ARCapture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string filePath = Application.persistentDataPath + "/" + filename;

            // Save the image to the photo library
                NativeGallery.SaveImageToGallery(bytes, "MyGallery", filename);

                Destroy(depthTexture);
                Destroy(colorTexture);
            }
        }
    }


    private Texture2D ConvertDepthToColor(Texture2D depthTexture)
    {
        int width = depthTexture.width;
        int height = depthTexture.height;
        Texture2D colorTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

        NativeArray<ushort> depthDataNativeArray = depthTexture.GetRawTextureData<ushort>();
        ushort[] depthData = depthDataNativeArray.ToArray();

        ushort minDepth = ushort.MaxValue;
        ushort maxDepth = ushort.MinValue;

        for (int i = 0; i < depthData.Length; i++)
        {
            ushort depthValue = depthData[i];
            minDepth = (ushort)Mathf.Min(minDepth, depthValue);
            maxDepth = (ushort)Mathf.Max(maxDepth, depthValue);
        }

        Color[] colorArray = new Color[width * height];
        for (int i = 0; i < colorArray.Length; i++)
        {
            ushort depthValue = depthData[i];
            float normalizedDepth = Mathf.InverseLerp(minDepth, maxDepth, depthValue);
            colorArray[i] = new Color(normalizedDepth, normalizedDepth, normalizedDepth, 1);
        }

        colorTexture.SetPixels(colorArray);
        colorTexture.Apply();

        return colorTexture;
    }


}
