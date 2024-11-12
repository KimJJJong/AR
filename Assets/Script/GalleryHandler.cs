using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GalleryHandler : MonoBehaviour
{
    public GameObject galleryPanel;
    public GameObject imagePrefab;
    public GameObject FullImgPanel; 
    public RawImage fullScreenImage; 

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

            RawImage imageComponent = newImage.GetComponent<RawImage>();
            imageComponent.texture = texture;

            Button imageButton = newImage.GetComponent<Button>();
            
            if (imageButton != null)
            {
                imageButton.onClick.AddListener(() => {
                    Debug.Log("Image clicked");
                    ShowFullScreenImage(texture);
                });
            }
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        return texture;
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
