using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Constants
{
    public const int PartyMemberCount = 3;
    public const int MaxLevel = 16;
}

[System.Serializable]
public class PlayableCharacter : CharacterStat
{
    public int level;
    public int currHP;
    public int currMP;
    public int currAtk;
    public float currDefRate;
    public float currStatusEffectRate;
    public float currCriticalRate;
    public int currEXP;
}


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] backGroundLayer;
    public GameObject MoveButton;
    public DataBaseManager dataBaseManager;
    public List<CharacterStat> characterStatData;
    public PlayableCharacter[] playCharacters = new PlayableCharacter[Constants.PartyMemberCount];
    public int[] playCharactersNo = new int[Constants.PartyMemberCount];
    

    /*
     *-------------------------------------
     *CharacterSelectScene Method
     *-------------------------------------
     */
    void loadCharacterInfo(PlayableCharacter playcharacter, CharacterStat character)
    {
        playcharacter.level = 1;
        playcharacter.characterAnimator = character.characterAnimator;
        playcharacter.characterSprite = character.characterSprite;
        playcharacter.characterPortrait = character.characterPortrait;
        playcharacter.CharacterNo = character.CharacterNo;
        playcharacter.name = character.name;
        playcharacter.maxHP = character.maxHP;
        playcharacter.maxMP = character.maxMP;
        playcharacter.atk = character.atk;
        playcharacter.defRate = character.defRate;
        playcharacter.statusEffectRate = character.statusEffectRate;
        playcharacter.criticalRate = character.criticalRate;
        playcharacter.maxHPGrowthRate = character.maxHPGrowthRate;
        playcharacter.maxMPGrowthRate = character.maxMPGrowthRate;
        playcharacter.atkGrowthRate = character.atkGrowthRate;
        playcharacter.passiveSkill = character.passiveSkill;
        playcharacter.skills = character.skills;
        playcharacter.isSelected = character.isSelected;
        playcharacter.currHP = character.maxHP;
        playcharacter.currMP = character.maxMP;
        playcharacter.currAtk = character.atk;
        playcharacter.currDefRate = character.defRate;
        playcharacter.currStatusEffectRate = character.statusEffectRate;
        playcharacter.currCriticalRate = character.criticalRate;
        playcharacter.currEXP = 0;
        playcharacter.EXPTable = character.EXPTable;
    }

    public void GoToIngameScene()
    {
        for(int CharacterNoArrloop = 0; CharacterNoArrloop < playCharactersNo.Length; CharacterNoArrloop++)
        {
            for(int CharacterStatArrloop = 0; CharacterStatArrloop < characterStatData.Count; CharacterStatArrloop++)
            {
                if(playCharactersNo[CharacterNoArrloop] == characterStatData[CharacterStatArrloop].CharacterNo)
                {
                    playCharacters[CharacterNoArrloop] = new PlayableCharacter();
                    loadCharacterInfo(playCharacters[CharacterNoArrloop], characterStatData[CharacterStatArrloop]);
                }
            }
        }
        SceneManager.LoadScene("InGameScene");
    }

    public void ReturnToTitleScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    /*
     *-------------------------------------
     */


    /*
    *-------------------------------------
    *InGameScene Method
    *-------------------------------------
    */

    public void ControlBackGroundMove()
    {
        if (backGroundLayer[0].GetComponent<MoveBackground>().curr_speed == 0)
        {
            MoveButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Stop");
            for (int loop = 0; loop < backGroundLayer.Length; loop++)
                backGroundLayer[loop].GetComponent<MoveBackground>().move();
        }
        else
        {
            MoveButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Go");
            for (int loop = 0; loop < backGroundLayer.Length; loop++)
                backGroundLayer[loop].GetComponent<MoveBackground>().stop();
        }
    }


    /*
     *-------------------------------------
     */


    private void Awake()
    {
        var obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for (int loop = 0; loop < playCharactersNo.Length; loop++)
        {
            playCharactersNo[loop] = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
