using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private bool isCapturing = false; // 중복 캡처 방지 플래그
    public Camera arCamera;
    private RenderTexture dynamicRenderTexture;

    public void CaptureScreenshot()
    {
        if (!isCapturing)
            StartCoroutine(CaptureScreenshotCoroutine());
    }

    private IEnumerator CaptureScreenshotCoroutine()
    {
        isCapturing = true;

        yield return new WaitForEndOfFrame();

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        if (dynamicRenderTexture == null || dynamicRenderTexture.width != screenWidth || dynamicRenderTexture.height != screenHeight)
        {
            if (dynamicRenderTexture != null)
                dynamicRenderTexture.Release();

            dynamicRenderTexture = new RenderTexture(screenWidth, screenHeight, 24, RenderTextureFormat.ARGB32);
            dynamicRenderTexture.Create();
        }

        arCamera.targetTexture = dynamicRenderTexture;

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = dynamicRenderTexture;
        arCamera.Render();

        // Texture2D
        Texture2D image = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0);
        image.Apply();

        RenderTexture.active = currentRT;
        arCamera.targetTexture = null;

        // 파일 저장
        byte[] bytes = image.EncodeToPNG();
        string fileName = $"myAR_screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved: " + filePath);

        isCapturing = false;
        Destroy(image);

        #if UNITY_ANDROID
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext"))
            {
                using (AndroidJavaObject mediaScanner = new AndroidJavaObject("android.media.MediaScannerConnection"))
                {
                    mediaScanner.CallStatic("scanFile", context, new string[] { filePath }, null, null);
                }
            }
        #endif
    }
}