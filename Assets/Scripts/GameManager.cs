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
    public int level { get; set; }
    public int currHP { get; set; }
    public int currMP { get; set; }
    public int currAtk { get; set; }
    public float currDefRate { get; set; }
    public float currStatusEffectRate { get; set; }
    public float currCriticalRate { get; set; }
    public int currEXP { get; set; }
}


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] backGroundLayer;
    [SerializeField] GameObject MoveButton;
    [SerializeField] DataBaseManager dataBaseManager;
    [SerializeField] List<CharacterStat> characterStatData;
    [SerializeField] SceneChanger sceneChanger;
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
        sceneChanger.GetComponent<SceneChanger>().goToScene("InGameScene");
    }

    public void ReturnToTitleScene()
    {
        sceneChanger.GetComponent<SceneChanger>().goToScene("StartScene");
        Destroy(dataBaseManager.gameObject);
        Destroy(gameObject);
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
        characterStatData = dataBaseManager.GetComponent<DataBaseManager>().Characters;
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
