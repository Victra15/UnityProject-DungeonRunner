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
    public bool isActionFinished;

}

[System.Serializable]
public class InGameEnemy : EnemyStat
{
    public int currHP;
    public int currAtk;
    public float currDefRate;
    public float currStatusEffectRate;
    public float currCriticalRate;
    public bool isActionFinished;
}


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsEnemySelectModeOn = false;
    public bool IsPlayerSelectModeOn = false;
    public bool IsBattleFinished;
    public bool IsPlayerTurnFinished;
    public bool IsEnemyTurnFinished;

    public IEnumerator currUsingSkill;

    public static GameManager instance;
    public PlayableCharacter[] playCharacters = new PlayableCharacter[Constants.PartyMemberCount];
    public int[] playCharactersNo = new int[Constants.PartyMemberCount];
    public List<InGameEnemy> inGameEnemies = new List<InGameEnemy>();
    public List<int> inGameEnemiesNo = new List<int>();

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

    void loadEnemyInfo(InGameEnemy inGameEnemy, EnemyStat enemy)
    {
        inGameEnemy.EnemyPrefab = enemy.EnemyPrefab;
        inGameEnemy.name = enemy.name;
        inGameEnemy.maxHP = enemy.maxHP;
        inGameEnemy.atk = enemy.atk;
        inGameEnemy.defRate = enemy.defRate;
        inGameEnemy.statusEffectRate = enemy.statusEffectRate;
        inGameEnemy.criticalRate = enemy.criticalRate;
        inGameEnemy.currHP = enemy.maxHP;
        inGameEnemy.currAtk = enemy.atk;
        inGameEnemy.currDefRate = enemy.defRate;
        inGameEnemy.currStatusEffectRate = enemy.statusEffectRate;
        inGameEnemy.currCriticalRate = enemy.criticalRate;
    }

    public void GoToIngameScene()
    {
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

    public IEnumerator BattleStart()
    {
        IsBattleFinished = false;
        Debug.Log("Battle Start!");
        while (!IsBattleFinished)
        {
            yield return PlayerTurn();

            if (inGameEnemies.Count <= 0)
            {
                IsBattleFinished = true;
            }

            yield return EnemyTurn();

            yield return null;
        }
        Debug.Log("Battle Finished.");
    }

    public IEnumerator PlayerTurn()
    {
        IsPlayerTurnFinished = false;
        PlayerPanel.instance.ReadyToBattle();

        while (!IsPlayerTurnFinished)
        {
            if (IsAllPlayerActionFinished())
            {
                IsPlayerTurnFinished = true;
            }
            yield return null;
        }
    }

    public bool IsAllPlayerActionFinished()
    {
        for(int loop = 0; loop < playCharacters.Length; loop++)
        {
            if(!playCharacters[loop].isActionFinished)
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator EnemyTurn()
    {
        IsEnemyTurnFinished = false;
        EnemyReadyToBattle();

        while (!IsEnemyTurnFinished)
        {
            for (int loop = 0; loop < inGameEnemies.Count; loop++)
            {
                yield return EnemyAction(loop);
                yield return new WaitForSeconds(1.5f);
            }

            if (IsAllEnemyActionFinished())
            {
                IsEnemyTurnFinished = true;
            }
            yield return null;
        }
    }

    public IEnumerator EnemyAction(int enemyIdx)
    {

        inGameEnemies[enemyIdx].isActionFinished = true;
        yield return null;
    }
    public void EnemyReadyToBattle()
    {
        for (int loop = 0; loop < inGameEnemies.Count; loop++)
        {
            inGameEnemies[loop].isActionFinished = false;
        }
    }

    public bool IsAllEnemyActionFinished()
    {
        for (int loop = 0; loop < inGameEnemies.Count; loop++)
        {
            if (!inGameEnemies[loop].isActionFinished)
            {
                return false;
            }
        }
        return true;
    }

    public void stopUsingSkill()
    {
        CommandPanel.ClearText();
        PlayerPanel.instance.PlayerActionUnLock();
        IsEnemySelectModeOn = false;
        IsPlayerSelectModeOn = false;
        StopCoroutine(currUsingSkill);
    }

    public void BasicAttack()
    {
        if (currUsingSkill != null)
        {
            stopUsingSkill();
        }
        currUsingSkill = DataBaseManager.instance.BasicAttack();
        StartCoroutine(currUsingSkill);
    }

    public void SpawnEnemy()
    {
        inGameEnemiesNo.Add(1);
        inGameEnemiesNo.Add(1);
        inGameEnemiesNo.Add(1);
        inGameEnemiesNo.Add(1);

        for (int EnemyNoArrloop = 0; EnemyNoArrloop < inGameEnemiesNo.Count; EnemyNoArrloop++)
        {
            for (int EnemyStatArrloop = 0; EnemyStatArrloop < DataBaseManager.instance.Enemies.Count; EnemyStatArrloop++)
            {
                if (inGameEnemiesNo[EnemyNoArrloop] == DataBaseManager.instance.Enemies[EnemyStatArrloop].EnemyNo)
                {
                    inGameEnemies.Add(new InGameEnemy());
                    loadEnemyInfo(inGameEnemies[EnemyNoArrloop], DataBaseManager.instance.Enemies[EnemyStatArrloop]);
                }
            }
        }
    }


    /*
     *-------------------------------------
     */


    private void Awake()
    {
        if (instance == null)
            instance = this;

        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
