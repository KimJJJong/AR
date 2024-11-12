using UnityEngine;

public class CameraScreenManager : MonoBehaviour
{
    public ScreenshotHandler screenshotHandler;
    public GalleryHandler galleryHandler;

    public void OnCaptureButtonPressed()
    {
        screenshotHandler.CaptureScreenshot();
    }

    public void OnGalleryButtonPressed()
    {
        galleryHandler.LoadGalleryImages();
    }
}
