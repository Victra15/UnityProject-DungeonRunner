using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] backGroundLayer;
    public GameObject MoveButton;
    public GameObject characterPanel;
    public GameObject[] characterSelectButton;
    
    /*
     *-------------------------------------
     *StartScene Method
     *-------------------------------------
     */
    public void GameExit()
    {
        Debug.Log("나가기");
        Application.Quit();
    }
    public void GameStart()
    {
        Debug.Log("처음부터");
    }
    public void GameContinue()
    {
        Debug.Log("이어하기");
    }

    /*
     *-------------------------------------
     */
    public void ShowEachCharacterData(int index)
    {
    }

    public void ControlBackGroundMove()
    {
        if(backGroundLayer[0].GetComponent<MoveBackground>().curr_speed == 0)
        {
            MoveButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Stop");
            for(int loop = 0; loop < backGroundLayer.Length; loop++)
                backGroundLayer[loop].GetComponent<MoveBackground>().move();
        }
        else
        {
            MoveButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Go");
            for (int loop = 0; loop < backGroundLayer.Length; loop++)
                backGroundLayer[loop].GetComponent<MoveBackground>().stop();
        }
    }
    void Start()
    {
        characterPanel = GetComponent<GameObject>()
            characterSelectButton = ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
