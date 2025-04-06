using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PixelationEffect : MonoBehaviour
{
    public RenderTexture pixelRenderTexture; // Assigned in inspector
    public Material blitMaterial; // optional

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pixelRenderTexture == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        if (blitMaterial)
            Graphics.Blit(pixelRenderTexture, destination, blitMaterial);
        else
            Graphics.Blit(pixelRenderTexture, destination);
    }
}
