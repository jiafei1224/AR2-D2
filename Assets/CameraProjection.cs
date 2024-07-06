using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CameraProjection : MonoBehaviour
{
    public RawImage rawImage;
    public Camera mainCamera;
    public RenderTexture renderTexture;

    void Start()
    {
        // Create a new RenderTexture with the same dimensions as the screen
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // Set the target texture of the main camera to the RenderTexture
        mainCamera.targetTexture = renderTexture;
        // Set the texture of the RawImage to the RenderTexture
        rawImage.texture = renderTexture;
    }

    void OnDestroy()
    {
        // Release the RenderTexture when the script is destroyed
        renderTexture.Release();
    }
}