using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GalleryHandler : MonoBehaviour
{
    public GameObject galleryPanel;
    public GameObject imagePrefab;

    private void Start()
    {
        LoadGalleryImages();
    }

    public void LoadGalleryImages()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath, "myAR_screenshot_*.png");

        foreach (string file in files)
        {
            GameObject newImage = Instantiate(imagePrefab, galleryPanel.transform);
            Texture2D texture = LoadTextureFromFile(file);

            newImage.GetComponent<RawImage>().texture = texture;
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
    }
}
