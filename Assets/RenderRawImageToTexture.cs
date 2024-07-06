using System.Text;


using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.iOS;
public class RenderRawImageToTexture : MonoBehaviour
{
    public RawImage rawImage;
    public RenderTexture renderTexture;

    private Camera renderCamera;

    private void Awake()
    {
        // Create a new camera to render the raw image onto the texture
        GameObject cameraObj = new GameObject("Render Camera");
        renderCamera = cameraObj.AddComponent<Camera>();

        // Set the camera's clear flags to "Solid Color" to avoid rendering the scene
        renderCamera.clearFlags = CameraClearFlags.SolidColor;

        // Set the camera's target texture to the render texture
        renderCamera.targetTexture = renderTexture;

        // Set the camera's culling mask to only include the raw image's layer
        renderCamera.cullingMask = 1 << LayerMask.NameToLayer(rawImage.gameObject.layer.ToString());
    }

    private void OnDestroy()
    {
        // Destroy the render camera when the script is destroyed
        Destroy(renderCamera.gameObject);
    }

    private void LateUpdate()
    {
        // Set the camera's position and rotation to match the raw image's
        renderCamera.transform.position = rawImage.transform.position;
        renderCamera.transform.rotation = rawImage.transform.rotation;

        // Render the raw image onto the texture
        renderCamera.Render();
    }
}
