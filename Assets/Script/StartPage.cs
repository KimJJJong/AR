using UnityEngine;
using UnityEngine.UI;
using System.IO; 
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    public GameObject loginPage;
    public GameObject introPage;
    public Text introScript1;
    public Text introScript2;
    public GameObject introButton;

    private string[] text1Options = { "<b>안녕!</b>\n만나서 반가워", "넌 <b>이름이</b>\n뭐야?"};
    private string[] text2Options = { "혼자 자취하면서\n고민이 많았지?\n\n이젠 괜찮아!", "<b>난 _____________야!</b>", };
    private int pageCount = 0;
    private int pageNum = 0;

    // Start is called before the first frame u pdate
    void Start()
    {
        loginPage.SetActive(true);
        introPage.SetActive(false);
        introButton.SetActive(false);

        introScript1.text = text1Options[pageCount];
        introScript2.text = text2Options[pageCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (pageNum == 1) {
            if (Input.GetMouseButtonDown(0))
            {
                if (pageCount < text1Options.Length - 1)
                {
                    pageCount++;
                    introScript1.text = text1Options[pageCount];
                    introScript2.text = text2Options[pageCount];
                    introButton.SetActive(true);
                }
            }
        }
        
    }

    public void turnIntroPage() {
        loginPage.SetActive(false);
        introPage.SetActive(true);
        pageNum = 1;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
