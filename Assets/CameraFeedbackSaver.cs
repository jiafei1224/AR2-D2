using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using NativeGalleryNamespace;

using UnityEngine.XR.ARSubsystems;

public class CameraFeedbackSaver : MonoBehaviour
{
    public ARCameraManager cameraManager;
   

    private float lastSaveTime = 0f;

    void start(){
        cameraManager = FindObjectOfType<ARCameraManager>();
    }

    private void OnEnable()
    {
        cameraManager.frameReceived += SaveScreenshot;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= SaveScreenshot;
    }

    private void SaveScreenshot(ARCameraFrameEventArgs eventArgs)
    {
        if (Time.time - lastSaveTime >= 0.0333f) // save at 30 FPS
        {
            Texture2D texture = eventArgs.textures[0];
            byte[] bytes = texture.EncodeToPNG();
            string filename = "ARCapture_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string filePath = Application.persistentDataPath + "/" + filename;

            // Save the image to the photo library
            NativeGallery.SaveImageToGallery(bytes, "MyGallery", filename);
        }
    }
}