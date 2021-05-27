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
    public GameObject CharacterPanel;
    public GameObject DataBaseManager;
    public GameObject CharacterSelectButton;
    public GameObject CharacterStatusPannel;
    public CharacterStat selectedCharacter;
    public List<TextMeshProUGUI> statusInfo;
    public Sprite DefaultPortrait;

    
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
    public void ShowEachCharacterData(int characterNo)
    {
        List<CharacterStat> Characters = DataBaseManager.GetComponent<DataBaseManager>().Characters;
        selectedCharacter = Characters[characterNo];
        statusInfo[0].text = string.Format("{0}",selectedCharacter.name);
        statusInfo[1].text = string.Format("{0}",selectedCharacter.maxHP);
        statusInfo[2].text = string.Format("{0}",selectedCharacter.maxMP);
        statusInfo[3].text = string.Format("{0}",selectedCharacter.atk);
        statusInfo[4].text = string.Format("{0}%", selectedCharacter.defRate * 100);

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
