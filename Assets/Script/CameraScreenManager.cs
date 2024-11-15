using UnityEngine;
using System.IO;

public class CameraScreenManager : MonoBehaviour
{
    public ScreenshotHandler screenshotHandler;
    public GalleryHandler galleryHandler;
    public GameObject galleryPanel;

    public GameObject blackPanel;
    public GameObject tutorialText;
    public GameObject cameraButtons;
    //public GameObject menuButton;
    public GameObject infoPanel;

    void start()
    {
        galleryPanel.SetActive(false);
        cameraButtons.SetActive(false);
    }
    
    public void CameraModeStart()
    {
        blackPanel.SetActive(false);
        tutorialText.SetActive(false);
        //menuButton.SetActive(false);
        infoPanel.SetActive(false);
        cameraButtons.SetActive(true);
        Debug.Log("Camera Opened");
    }

    public void OnCaptureButtonPressed()
    {
        screenshotHandler.CaptureScreenshot();
    }

    public void OnGalleryButtonPressed()
    {
        galleryPanel.SetActive(true);
        galleryHandler.LoadGalleryImages();
    }

    public void ExitGalleryButtonPressed()
    {
        galleryPanel.SetActive(false);
    }

    public void ExitCameraButtonPressed()
    {
        blackPanel.SetActive(true);
        tutorialText.SetActive(true);
        galleryPanel.SetActive(false);
        //menuButton.SetActive(true);
        infoPanel.SetActive(true);
        cameraButtons.SetActive(false);
    }
}