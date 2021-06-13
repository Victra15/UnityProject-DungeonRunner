using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.IO;

[System.Serializable]
public class skill
{
    public Sprite skillImage;
    public string skillName;
    public string skillInfo;
    public UnityEvent skillActivate;
}

[System.Serializable]
public class CharacterStat
{
    public RuntimeAnimatorController characterAnimator;
    public Sprite characterSprite;
    public Sprite characterPortrait;
    public int CharacterNo;
    public string name;
    public int maxHP;
    public int maxMP;
    public int atk;
    public float defRate;
    public float statusEffectRate;
    public float criticalRate;
    public float maxHPGrowthRate;
    public float maxMPGrowthRate;
    public float atkGrowthRate;
    public skill passiveSkill;
    public List<skill> skills;
    public bool isSelected;
    public int[] EXPTable = new int[Constants.MaxLevel - 1];
}

[System.Serializable]
public class EnemyStat
{
    public GameObject EnemyPrefab;
    public int EnemyNo;
    public string name;
    public int maxHP;
    public int atk;
    public float defRate;
    public float statusEffectRate;
    public float criticalRate;
}


public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public int[] EXPTable = new int[Constants.MaxLevel - 1] {   25,
                                                                50,
                                                                100,
                                                                250,
                                                                500,
                                                                1000,
                                                                2500,
                                                                5000,
                                                                10000,
                                                                25000,
                                                                50000,
                                                                100000,
                                                                250000,
                                                                500000,
                                                                1000000
                                                            };
    public List<CharacterStat> Characters;
    public List<EnemyStat> Enemies;
    public GameObject remainTurnStatusIcon;
    public GameObject oneTurnStatusIcon;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;

        var obj = FindObjectsOfType<DataBaseManager>();
        if (obj.Length == 1)
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
        for (int loop = 0; loop < Characters.Count; loop++)
        {
            Characters[loop].EXPTable = EXPTable;
        }
    }



    /*
     * enemy skills
     * 
     */

    public void EnemyBasicAttack(int EnemyIdx, int PlayerIdx)
    {
        int victimHp = GameManager.instance.playCharacters[PlayerIdx].currHP;
        int Damage = (int)(GameManager.instance.inGameEnemies[EnemyIdx].currAtk * (1 - GameManager.instance.playCharacters[PlayerIdx].currDefRate));

        GameManager.instance.playCharacters[PlayerIdx].currHP = victimHp - Damage;
        PlayerPanel.instance.transform.GetChild(PlayerIdx).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[PlayerIdx].currHP;
        GameManager.instance.OnDamagedAnim(PlayerPanel.instance.transform.GetChild(PlayerIdx).GetChild(1), Damage);
        PlayerPanel.instance.PlayerDeath();
    }


    /*
     * player skills
     * 
     */

    public IEnumerator WaitForOneEnemyTargetSelect()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "대상을 선택하세요 (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        GameManager.instance.IsEnemySelectModeOn = true;

        while (GameManager.instance.IsEnemySelectModeOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }
        CommandPanel.ClearText();
        PlayerPanel.instance.PlayerActionUnLock();
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        GameManager.instance.currUsingSkill = null;
    }

    public IEnumerator WaitForOnePlayerTargetSelect()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "대상을 선택하세요 (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        GameManager.instance.IsPlayerSelectModeOn = true;

        while (GameManager.instance.IsPlayerSelectModeOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }
        CommandPanel.ClearText();
        PlayerPanel.instance.PlayerActionUnLock();
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        GameManager.instance.currUsingSkill = null;
    }

    public IEnumerator WaitForSelfSelect()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "스킬을 사용하시겠습니까? (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        PlayerPanel.instance.transform.GetChild(CommandPanel.selectedCharacterIndex).GetChild(2).gameObject.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                break;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }
        CommandPanel.ClearText();
        PlayerPanel.instance.transform.GetChild(CommandPanel.selectedCharacterIndex).GetChild(2).gameObject.SetActive(false);
        PlayerPanel.instance.PlayerActionUnLock();
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        GameManager.instance.currUsingSkill = null;
    }

    public IEnumerator WaitForEveryEnemySelect()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "스킬을 사용하시겠습니까? (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        for (int loop = 0; loop < EnemyPanel.instance.transform.childCount; loop++)
        {
            EnemyPanel.instance.transform.GetChild(loop).GetChild(0).gameObject.SetActive(true);
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                break;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }

        for (int loop = 0; loop < EnemyPanel.instance.transform.childCount; loop++)
        {
            EnemyPanel.instance.transform.GetChild(loop).GetChild(0).gameObject.SetActive(false);
        }
        CommandPanel.ClearText();
        PlayerPanel.instance.PlayerActionUnLock();
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        GameManager.instance.currUsingSkill = null;
    }

    public IEnumerator WaitForEveryPlayerSelect()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "스킬을 사용하시겠습니까? (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        for (int loop = 0; loop < PlayerPanel.instance.transform.childCount; loop++)
        {
            PlayerPanel.instance.transform.GetChild(loop).GetChild(2).gameObject.SetActive(true);
        }

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                break;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }

        for (int loop = 0; loop < PlayerPanel.instance.transform.childCount; loop++)
        {
            PlayerPanel.instance.transform.GetChild(loop).GetChild(2).gameObject.SetActive(false);
        }
        CommandPanel.ClearText();
        PlayerPanel.instance.PlayerActionUnLock();
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        GameManager.instance.currUsingSkill = null;
    }

    public void PlayerAttack(int PlayerIdx, int EnemyIdx, int Damage)
    {
        int victimHp = GameManager.instance.inGameEnemies[EnemyIdx].currHP;

        GameManager.instance.inGameEnemies[EnemyIdx].currHP = victimHp - Damage;
        EnemyPanel.instance.transform.GetChild(EnemyIdx).GetChild(1).GetComponent<Slider>().value = GameManager.instance.inGameEnemies[EnemyIdx].currHP;
        GameManager.instance.OnDamagedAnim(EnemyPanel.instance.transform.GetChild(EnemyIdx), Damage);
    }

    public bool isStatusEffectOn(string SkillImageName, int currCharacterIdx ,int effectDuration)
    {
        Transform statusPanel = PlayerPanel.instance.transform.GetChild(currCharacterIdx).GetChild(5);

        for (int loop = 0; loop < statusPanel.childCount; loop++) 
        {
            if(statusPanel.GetChild(loop).GetComponent<Image>().sprite.name == SkillImageName)
            {
                GameManager.instance.playCharacters[currCharacterIdx].statusCounter[loop] += effectDuration;
                statusPanel.GetChild(loop).GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0}", GameManager.instance.playCharacters[currCharacterIdx].statusCounter[loop] - GameManager.instance.TurnCounter);
                return true;
            }
        }
        return false;
    }

    public IEnumerator BasicAttack()
    {
        yield return WaitForOneEnemyTargetSelect();

        int playerIdx = CommandPanel.selectedCharacterIndex;
        int enemyIdx = EnemyToolTip.selectedEnemyIdx;
        int Damage = (int)(GameManager.instance.playCharacters[playerIdx].currAtk * (1 - GameManager.instance.inGameEnemies[enemyIdx].currDefRate));
        
        PlayerAttack(playerIdx, enemyIdx, Damage);
    }

    public IEnumerator DefenceStance()
    {
        yield return WaitForSelfSelect();

        int currCharacterIdx = CommandPanel.selectedCharacterIndex;
        string skillImageName = GameManager.instance.playCharacters[currCharacterIdx].skills[0].skillImage.name;
        int effectDuration = 3;

        if(!isStatusEffectOn(skillImageName, currCharacterIdx, effectDuration))
        {
            GameManager.instance.playCharacters[currCharacterIdx].currDefRate += 0.3f;
            GameManager.instance.playCharacters[currCharacterIdx].statusCounter.Add(GameManager.instance.TurnCounter + effectDuration);

            int statusIdx = GameManager.instance.playCharacters[currCharacterIdx].statusCounter.Count - 1;
            int currTurn = GameManager.instance.TurnCounter;

            GameObject StatusIcon = Instantiate(remainTurnStatusIcon, PlayerPanel.instance.transform.GetChild(currCharacterIdx).GetChild(5));
            StatusIcon.GetComponent<Image>().sprite = GameManager.instance.playCharacters[currCharacterIdx].skills[0].skillImage;
            StatusIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0}", GameManager.instance.playCharacters[currCharacterIdx].statusCounter[statusIdx] - GameManager.instance.TurnCounter);

            while (GameManager.instance.TurnCounter < GameManager.instance.playCharacters[currCharacterIdx].statusCounter[statusIdx])
            {
                if(currTurn != GameManager.instance.TurnCounter)
                {
                    currTurn = GameManager.instance.TurnCounter;
                    StatusIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0}", GameManager.instance.playCharacters[currCharacterIdx].statusCounter[statusIdx] - GameManager.instance.TurnCounter);
                }
                yield return null;
            }
            Destroy(StatusIcon);
            GameManager.instance.playCharacters[currCharacterIdx].statusCounter.RemoveAt(statusIdx);
            GameManager.instance.playCharacters[currCharacterIdx].currDefRate -= 0.3f;
        }
    }
    public IEnumerator Sacrifice()
    {
        yield return WaitForOnePlayerTargetSelect();

        int selectedCharacterIdx = CharacterTooltip.selectedCharacterIdx;
        int currCharacterIdx = CommandPanel.selectedCharacterIndex;
        string skillImageName = GameManager.instance.playCharacters[currCharacterIdx].skills[1].skillImage.name;
        int effectDuration = 1;

        if (!isStatusEffectOn(skillImageName, selectedCharacterIdx, effectDuration))
        {
            GameManager.instance.playCharacters[selectedCharacterIdx].statusCounter.Add(GameManager.instance.TurnCounter + effectDuration);

            int statusIdx = GameManager.instance.playCharacters[selectedCharacterIdx].statusCounter.Count - 1;
            int currTurn = GameManager.instance.TurnCounter;

            GameObject StatusIcon = Instantiate(oneTurnStatusIcon, PlayerPanel.instance.transform.GetChild(selectedCharacterIdx).GetChild(5));
            StatusIcon.GetComponent<Image>().sprite = GameManager.instance.playCharacters[currCharacterIdx].skills[1].skillImage;

            int currHp = GameManager.instance.playCharacters[selectedCharacterIdx].currHP;
            while (GameManager.instance.TurnCounter < GameManager.instance.playCharacters[selectedCharacterIdx].statusCounter[statusIdx])
            {
                if(currHp != GameManager.instance.playCharacters[selectedCharacterIdx].currHP)
                {
                    int HalfDamage = (currHp - GameManager.instance.playCharacters[selectedCharacterIdx].currHP) / 2;
                    GameManager.instance.playCharacters[currCharacterIdx].currHP -= HalfDamage;
                    GameManager.instance.playCharacters[selectedCharacterIdx].currHP += HalfDamage;
                }
                yield return null;
            }
            Destroy(StatusIcon);
            GameManager.instance.playCharacters[selectedCharacterIdx].statusCounter.RemoveAt(statusIdx);
        }
    }
}


