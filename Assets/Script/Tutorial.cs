using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    [Header("Main Ui")]
    public GameObject uiButton;

    [Header("튜토리얼 Box")]
    public TextMeshProUGUI textContent;

    [Header("InfoPanel")]
    public Button panelButton;
    public GameObject panel;
    public TextMeshProUGUI paneText;

    [Header("BlackPanel")]
    public Button blackPanelButton;
    public GameObject BlackPanel;

    string[] tutorial;
    int _process;
    bool missionComplete;
    public TalkManager talkManager;
    void Start()
    {

        tutorial = new string[100];
        _process = 0;
        panelButton.onClick.AddListener(PanelButtonClick);
        blackPanelButton.onClick.AddListener(BlackPanelButtonClick);
        uiButton.SetActive(false);
        SetTutorialText();
        UpdateText();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            UpdateText();
    }


    void UpdateText()
    {
        Debug.Log(_process);
        switch(_process)
        {
            case 0:
                textContent.text = tutorial[0];
                break;
            case 1:

                textContent.text = tutorial[1];
                if (!GameManager.Instance.canSpawn)
                {
                    _process++;
                    UpdateText();
                }
                break;
            case 2:
                textContent.text = tutorial[2];//쓰담
                if(talkManager.GetTalkBox().activeSelf)
                {
                _process++;
                UpdateText();
                }
                break;
            case 3:
                textContent.text = tutorial[3];
                BlackPanel.SetActive(true);

                _process++;
                break;
            case 4:
                textContent.text = "앱 기능 설명";
                uiButton.SetActive(true);
                panel.SetActive(true);
                paneText.text = tutorial[4];

                //_process++;

                break;
            case 5:
                //                textContent.text = "";
                paneText.text = tutorial[5];

                //_process++;

                break;
            case 6:
                //               
                paneText.text = tutorial[6];


                break;
            case 7:
                //                textContent.text = "";
                paneText.text = tutorial[7];

//                _process++;

                break;
            case 8:
                //                textContent.text = "";
                paneText.text = tutorial[8];

//                _process++;

                break;
            case 9:
                //                textContent.text = "";
                paneText.text = " ";
                panel.SetActive(false);
                BlackPanel.SetActive(false);
                textContent.text = " ";
  //              _process++;

                break;
        }
    }
    public void SetTutorialText()
    {
        tutorial[0] = "앗! 여긴 어디지? \n \n 깜짝 놀라 숨어버니 펭라 \n 똑똑!! 두드려 보세요!";
        tutorial[1] = "나는 어디에 있을까? 삐? \n [펭라를 원하는 곳에 놓아 주세요]";
        tutorial[2] = "펭라를 쓰다듬어 보세요!";
        tutorial[3] = "참 잘했어요!";
        tutorial[4] = "미션 인증하기\n 이 버튼은 설정한~~";
        tutorial[5] = "성장 일지\n 이 버튼은 설정한~~";
        tutorial[6] = "펫 레벨\n 이버튼은 나의 펫~~";
        tutorial[7] = "마이 홈\n 이버튼은 나와 함께 생활하는~~";
        tutorial[8] = "AR/ ON/OFF\n 혹시 나를 만나는 동안 배경이~~";

    }


    void PanelButtonClick()
    {
        //panel.SetActive(false);
    }
    void BlackPanelButtonClick()
    {
        _process++;
        if(_process == 1)
        {
            GameManager.Instance.canSpawn = true;
            BlackPanel.SetActive(false);

        }



        UpdateText();
        


    }

}
