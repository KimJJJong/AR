using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject[] MenuPanle;


    public Button[] myButton;   //1 : home      // 2 : Mission  //  3 : �޷�  //  4 : PetLv
    private void Start()
    {
        myButton = GetComponentsInChildren<Button>();

        foreach(var panel in MenuPanle)
            panel.gameObject.SetActive(false);

        myButton[0].onClick.AddListener(()=> MyHomeButton());
        myButton[1].onClick.AddListener(() => MissionButton());
        myButton[2].onClick.AddListener(() => GrowthDiaryButton());
        myButton[3].onClick.AddListener(() => PetLvButton());
    }

    public void MyHomeButton()
    {
        MyHome myHome= myButton[0].gameObject.GetComponent<MyHome>();
        MenuPanle[0].gameObject.SetActive(true);

    }

    public void MissionButton()
    {
        Mission mission = myButton[1].gameObject.GetComponent<Mission>();
        MenuPanle[1].gameObject.SetActive(true);

    }

    public void GrowthDiaryButton()
    {
        GrowthDiary growDiary = myButton[2].gameObject.GetComponent<GrowthDiary>();
        MenuPanle[2].gameObject.SetActive(true);

    }

    public void PetLvButton()
    {
        PetLv petLv = myButton[3].gameObject.GetComponent<PetLv>();
        MenuPanle[3].gameObject.SetActive(true);

    }





}