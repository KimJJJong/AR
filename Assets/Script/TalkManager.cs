using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
   // public GameObject talkBlockPrefab;  
    public GameObject talkBlock;    
    private TextMeshPro talkBlockText;
    private Camera mainCamera;
    private bool isTalking;



    private void Start()
    {
        mainCamera = Camera.main;
        //talkBlock = Instantiate(talkBlockPrefab);


       talkBlockText = talkBlock.GetComponent<TextMeshPro>();
       
        if (talkBlockText == null)
        {
            Debug.LogError("TextMeshPro component not found in the talkBlock!");
        }
        talkBlock.SetActive(false);
    }

    private void Update()
    {
        if (talkBlock.activeSelf)
        {
            // Calculate the direction from the talkBlock to the camera
            Vector3 cameraDirection = mainCamera.transform.position - talkBlock.transform.position;
            cameraDirection.y = 0; // Optionally ignore Y axis to prevent tilting
            talkBlock.transform.rotation = Quaternion.LookRotation(-cameraDirection);
        }
    }
    /// <summary>
    /// TalkBox ����
    /// </summary>
    /// <param name="text">TEXT�� ����</param>
    /// <param name="pos">��ġ</param>
    /// <param name="direction">����</param>
    public void TalkBox(string text , Vector3 pos/*, Vector3 direction*/)
    {
        talkBlock.transform.position = pos;
        Debug.Log("text");
       // talkBlock.transform.rotation = Quaternion.LookRotation(direction);
       if( !isTalking )
        StartCoroutine(TalkText(text));
    }

    public GameObject GetTalkBox()
    {  return talkBlock; }

    /// <summary>
    /// ��ȭâ�� �ؽ�Ʈ�� �����ϰ� 3�� �� ����
    /// </summary>
    /// <param name="text">ǥ���� �ؽ�Ʈ</param>
    /// <returns></returns>
    IEnumerator TalkText(string text)
    {
        isTalking = true;
       talkBlock.SetActive(true);
        Debug.Log(text);
        string tmp = (string)text;

        talkBlockText.text = text;

        yield return new WaitForSeconds(3);
        isTalking=false;
       talkBlock.SetActive(false);
    }


}
