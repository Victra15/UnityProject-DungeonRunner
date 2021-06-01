using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


public class DataBaseManager : MonoBehaviour
{
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

    // Start is called before the first frame update
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
        for(int loop = 0; loop < Characters.Count; loop ++)
        {
            Characters[loop].EXPTable = EXPTable;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
