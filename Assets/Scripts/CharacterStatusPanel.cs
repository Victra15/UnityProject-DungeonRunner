using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatusPanel : MonoBehaviour
{
    CharacterStat character;
    public CharacterPositionPanel characterPositionPanel;
    TextMeshProUGUI nameVal; 
    TextMeshProUGUI maxHpVal; 
    TextMeshProUGUI maxMpVal; 
    TextMeshProUGUI atkVal; 
    TextMeshProUGUI defRateVal;

    Button[] skillInfoButtons;
    TextMeshProUGUI skillNameVal;
    TextMeshProUGUI skillInfoVal;

    Button selectButton;
    Button cancelButton;

    // Start is called before the first frame update
    public void characterSelectButtonClick(CharacterStat _character)
    {
        character = _character;

        nameVal.text = string.Format("{0}", character.name);
        maxHpVal.text = string.Format("{0}", character.maxHP);
        maxMpVal.text = string.Format("{0}", character.maxMP);
        atkVal.text = string.Format("{0}", character.atk);
        defRateVal.text = string.Format("{0}", character.defRate);
        skillNameVal.text = "";
        skillInfoVal.text = "설명:";
        if(character.isSelected)
        {
            selectButton.interactable = false;
            cancelButton.interactable = true;
        }
        else
        {
            selectButton.interactable = true;
            cancelButton.interactable = false;
        }
    }

    public void skillImageClick(int buttonIndex)
    {
        skillNameVal.text = string.Format("{0}", character.skills[buttonIndex].skillName);
        skillInfoVal.text = string.Format("설명: {0}", character.skills[buttonIndex].skillInfo);
    }

    void Start()
    {
        nameVal = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>(); // 캐릭터 이름 적는 칸
        maxHpVal = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>(); 
        maxMpVal = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        atkVal = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        defRateVal = transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfoButtons = transform.GetChild(5).GetChild(1).GetComponentsInChildren<Button>();
        skillNameVal= transform.GetChild(5).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfoVal = transform.GetChild(5).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();

        skillInfoButtons[0].onClick.AddListener(() => { skillImageClick(0); });
        skillInfoButtons[1].onClick.AddListener(() => { skillImageClick(1); });
        skillInfoButtons[2].onClick.AddListener(() => { skillImageClick(2); });
        skillInfoButtons[3].onClick.AddListener(() => { skillImageClick(3); });

        selectButton = transform.GetChild(6).GetComponent<Button>();
        cancelButton = transform.GetChild(7).GetComponent<Button>();
        
        selectButton.onClick.AddListener(() => { characterPositionPanel.characterSelectButtonClick(character); });
        selectButton.onClick.AddListener(() => {
            if (character.isSelected)
            {
                selectButton.interactable = false;
                cancelButton.interactable = true;
            }
            else
            {
                selectButton.interactable = true;
                cancelButton.interactable = false;
            }; });

        cancelButton.onClick.AddListener(() => { characterPositionPanel.characterCancelButtonClick(character); });
        cancelButton.onClick.AddListener(() => {
            if (character.isSelected)
            {
                selectButton.interactable = false;
                cancelButton.interactable = true;
            }
            else
            {
                selectButton.interactable = true;
                cancelButton.interactable = false;
            };
        });
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
