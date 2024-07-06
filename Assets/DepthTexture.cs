using UnityEngine;
using System.Collections;

public class DepthTexture : MonoBehaviour
{
    public Shader depthShader;
    private Material depthMaterial;

    void Start()
    {
        depthMaterial = new Material(depthShader);
        depthMaterial.hideFlags = HideFlags.HideAndDontSave;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, depthMaterial);
    }
}
