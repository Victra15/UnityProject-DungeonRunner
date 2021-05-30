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
}


public class DataBaseManager : MonoBehaviour
{
    public List<CharacterStat> Characters;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
