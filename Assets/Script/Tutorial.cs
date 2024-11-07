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

    [Header("Ʃ�丮�� Box")]
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
                textContent.text = tutorial[2];//����
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
                textContent.text = "�� ��� ����";
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
        tutorial[0] = "��! ���� �����? \n \n ��¦ ��� ������� ��� \n �ȶ�!! �ε�� ������!";
        tutorial[1] = "���� ��� ������? ��? \n [��� ���ϴ� ���� ���� �ּ���]";
        tutorial[2] = "��� ���ٵ�� ������!";
        tutorial[3] = "�� ���߾��!";
        tutorial[4] = "�̼� �����ϱ�\n �� ��ư�� ������~~";
        tutorial[5] = "���� ����\n �� ��ư�� ������~~";
        tutorial[6] = "�� ����\n �̹�ư�� ���� ��~~";
        tutorial[7] = "���� Ȩ\n �̹�ư�� ���� �Բ� ��Ȱ�ϴ�~~";
        tutorial[8] = "AR/ ON/OFF\n Ȥ�� ���� ������ ���� �����~~";

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
