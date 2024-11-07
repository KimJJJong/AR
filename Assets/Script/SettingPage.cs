using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO; 
using System.Text.RegularExpressions;

public class SettingPage : MonoBehaviour
{
    public GameObject categorySuggest1;
    public GameObject categorySuggest2;
    public GameObject categorySuggest3;
    public GameObject categorySuggest4;

    // Start is called before the first frame update
    void Start()
    {
        categorySuggest1.SetActive(false);
        categorySuggest2.SetActive(false);
        categorySuggest3.SetActive(false);
        categorySuggest4.SetActive(false);

        string LowestCategory = PlayerPrefs.GetString("LowestCategory", null);

        switch ( LowestCategory ) 
		{
            case "텅텅 빈 냉장고":
            categorySuggest1.SetActive(true);
            break;
            
            case "냄새 폴폴 빨래더미":
            categorySuggest2.SetActive(true);
            break;

            case "잔고 없는 지갑":
            categorySuggest3.SetActive(true);
            break;

            case "엉망진창 자취방":
            categorySuggest4.SetActive(true);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
