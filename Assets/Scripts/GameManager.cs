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
    public static GameManager instance;
    public PlayableCharacter[] playCharacters = new PlayableCharacter[Constants.PartyMemberCount];
    public int[] playCharactersNo = new int[Constants.PartyMemberCount];

    public void GotoCharacterSelectScene()
    {
        SceneChanger.instance.GetComponent<SceneChanger>().goToScene("CharacterSelectScene");
    }    

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
        List<CharacterStat> characterStatData = DataBaseManager.instance.GetComponent<DataBaseManager>().Characters;

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
        SceneChanger.instance.GetComponent<SceneChanger>().goToScene("InGameScene");
    }

    public void loadCharacters()
    {
        playCharactersNo[0] = 1;
        playCharactersNo[1] = 3;
        playCharactersNo[2] = 4;

        List<CharacterStat> characterStatData = DataBaseManager.instance.GetComponent<DataBaseManager>().Characters;

        for (int CharacterNoArrloop = 0; CharacterNoArrloop < playCharactersNo.Length; CharacterNoArrloop++)
        {
            for (int CharacterStatArrloop = 0; CharacterStatArrloop < characterStatData.Count; CharacterStatArrloop++)
            {
                if (playCharactersNo[CharacterNoArrloop] == characterStatData[CharacterStatArrloop].CharacterNo)
                {
                    playCharacters[CharacterNoArrloop] = new PlayableCharacter();
                    loadCharacterInfo(playCharacters[CharacterNoArrloop], characterStatData[CharacterStatArrloop]);
                }
            }
        }

        PlayerPanel.instance.load();
    }

    public void ReturnToTitleScene()
    {
        Debug.Log(SceneChanger.instance.gameObject.name);
        SceneChanger.instance.GetComponent<SceneChanger>().goToScene("StartScene");
        Destroy(DataBaseManager.instance.gameObject);
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
    

    public void meleeAtack()
    {

    }

    /*
     *-------------------------------------
     */


    private void Awake()
    {
        if (instance == null)
            instance = this;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
