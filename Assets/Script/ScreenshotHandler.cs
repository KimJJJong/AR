using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private bool isCapturing = false;

    public void CaptureScreenshot()
    {
        if (!isCapturing)
            StartCoroutine(CaptureScreenshotCoroutine());
    }

    private IEnumerator CaptureScreenshotCoroutine()
    {
        isCapturing = true;

        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        string fileName = $"myAR_screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        isCapturing = false;
        Debug.Log("Screenshot saved: " + filePath);

        #if UNITY_ANDROID
            // 갤러리에 추가
            new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                .CallStatic<AndroidJavaObject>("currentActivity")
                .Call("sendBroadcast", new AndroidJavaObject("android.content.Intent", "android.intent.action.MEDIA_SCANNER_SCAN_FILE", 
                new AndroidJavaObject("android.net.Uri", "file://" + filePath)));
        #endif
    }
}
