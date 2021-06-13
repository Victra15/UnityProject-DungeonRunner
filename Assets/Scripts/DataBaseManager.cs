using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
            instance = this;

        var obj = FindObjectsOfType<DataBaseManager>();
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
        for(int loop = 0; loop < Characters.Count; loop ++)
        {
            Characters[loop].EXPTable = EXPTable;
        }
        
    }


    
    /*
     * enemy skills
     * 
     */

    public void EnemyBasicAttack(int victimCharacterIdx, int attackerEnemyIdx)
    {
        GameManager.instance.playCharacters[victimCharacterIdx].currHP -= GameManager.instance.inGameEnemies[attackerEnemyIdx].currAtk;
        PlayerPanel.instance.transform.GetChild(victimCharacterIdx).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[victimCharacterIdx].currHP;
        EnemyPanel.instance.EnemyDeath();
    }










    /*
     * player skills
     * 
     */
     
    public IEnumerator BasicAttack()
    {
        PlayerPanel.instance.PlayerActionLock();

        CommandPanel.infoText.text = "대상을 선택하세요 (스킬 사용 취소는 마우스 오른쪽 클릭)";
        CommandPanel.tempText = CommandPanel.infoText.text;
        GameManager.instance.IsEnemySelectModeOn = true;

        while (GameManager.instance.IsEnemySelectModeOn)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                GameManager.instance.stopUsingSkill();
            }
            yield return null;
        }

        GameManager.instance.inGameEnemies[EnemyToolTip.selectedEnemyIdx].currHP -= GameManager.instance.playCharacters[CommandPanel.selectedCharacterIndex].currAtk;
        EnemyPanel.instance.transform.GetChild(EnemyToolTip.selectedEnemyIdx).GetChild(1).GetComponent<Slider>().value = GameManager.instance.inGameEnemies[EnemyToolTip.selectedEnemyIdx].currHP;
        PlayerPanel.instance.ActionFinished(CommandPanel.selectedCharacterIndex);
        EnemyPanel.instance.EnemyDeath();
        
        PlayerPanel.instance.PlayerActionUnLock();
    }
}


