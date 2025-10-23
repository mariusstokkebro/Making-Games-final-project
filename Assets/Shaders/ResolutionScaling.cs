using UnityEngine;

public class ResolutionScaling : MonoBehaviour
{
    [SerializeField] private RenderTexture pixelTexture;
    [SerializeField] private RenderTexture depthTexture;

    [SerializeField] private int targetWidth;
    [SerializeField] private int targetHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetWidth = Screen.currentResolution.width / 3;
        targetHeight = Screen.currentResolution.height / 3;

        SetResolution(targetWidth, targetHeight);
    }

    void Update()
    {
        if (Screen.width / 3 != targetWidth || Screen.height / 3 != targetHeight)
        {
            SetResolution(targetWidth, targetHeight);
        }
    }

    void SetResolution(int width, int height)
    {
        if (pixelTexture != null)
        {
            pixelTexture.Release();
            pixelTexture.width = width;
            pixelTexture.height = height;
            pixelTexture.Create();
        }
        if (depthTexture != null)
        {
            depthTexture.Release();
            depthTexture.width = width;
            depthTexture.height = height;
            depthTexture.Create();
        }
    }
}
