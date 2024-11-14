using UnityEngine;
using System.IO;

public class CameraScreenManager : MonoBehaviour
{
    public ScreenshotHandler screenshotHandler;
    public GalleryHandler galleryHandler;
    public GameObject GalleryPanel;

    void Start()
    {
        GalleryPanel.SetActive(false);
    }

    public void OnCaptureButtonPressed()
    {
        screenshotHandler.CaptureScreenshot();
    }

    public void OnGalleryButtonPressed()
    {
        GalleryPanel.SetActive(true);
        galleryHandler.LoadGalleryImages();
    }

    public void ExitGalleryButtonPressed()
    {
        GalleryPanel.SetActive(false);
    }
}
