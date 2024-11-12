using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GalleryHandler : MonoBehaviour
{
    public GameObject galleryPanel;
    public GameObject imagePrefab;
    public GameObject FullImgPanel; 
    public Image fullScreenImage; 

    private void Start()
    {
        FullImgPanel.SetActive(false);
    }

    public void LoadGalleryImages()
    {
        foreach (Transform child in galleryPanel.transform)
        {
            Destroy(child.gameObject);
        }

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

    public void AddImageToGallery(Texture2D imageTexture)
    {
        GameObject newImageObject = Instantiate(imagePrefab, galleryPanel.transform);

        RawImage imageComponent = newImageObject.GetComponent<RawImage>();
        imageComponent.texture = imageTexture;

        Button imageButton = newImageObject.GetComponent<Button>();
    
        if (imageButton != null)
        {
            imageButton.onClick.AddListener(() => {
                Debug.Log("Image clicked");
                ShowFullScreenImage(imageTexture);
            });
            imageButton.onClick.AddListener(() => ShowFullScreenImage(imageTexture));
        }
    }


    public void ShowFullScreenImage(Texture2D imageTexture)
    {
        fullScreenImage.material.mainTexture = imageTexture; // 클릭한 이미지로 설정
        FullImgPanel.SetActive(true);
    }

    public void CloseFullScreenImage()
    {
        FullImgPanel.SetActive(false);
    }
}
