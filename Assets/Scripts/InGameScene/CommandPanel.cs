using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite BasicAttactSprite;

    public static TextMeshProUGUI infoText;
    public static string tempText;
    public static int selectedCharacterIndex;
    

    Transform BasicAttackButton;
    Transform FirstSkillButton;
    Transform SecondSkillButton;
    Transform ThirdSkillButton;
    Transform FourthSkillButton;

    void Start()
    {
        infoText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        BasicAttackButton = transform.GetChild(0).GetChild(0);
        FirstSkillButton = transform.GetChild(0).GetChild(1);
        SecondSkillButton = transform.GetChild(0).GetChild(2);
        ThirdSkillButton = transform.GetChild(0).GetChild(3);
        FourthSkillButton = transform.GetChild(0).GetChild(4);

        BasicAttackButton.GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => {
            GameManager.instance.BasicAttack();
        });

        
    }

    public void characterSelected(bool onclicked, ToggleGroup toggleGroup)
    {
        if (!onclicked)
        {
            BasicAttackButton.gameObject.SetActive(false);
            FirstSkillButton.gameObject.SetActive(false);
            SecondSkillButton.gameObject.SetActive(false);
            ThirdSkillButton.gameObject.SetActive(false);
            FourthSkillButton.gameObject.SetActive(false);
        }
        else
        {
            BasicAttackButton.gameObject.SetActive(true);
            FirstSkillButton.gameObject.SetActive(true);
            SecondSkillButton.gameObject.SetActive(true);
            ThirdSkillButton.gameObject.SetActive(true);
            FourthSkillButton.gameObject.SetActive(true);

            selectedCharacterIndex = toggleGroup.ActiveToggles().FirstOrDefault().transform.GetSiblingIndex();

            BasicAttackButton.GetChild(1).GetComponent<Image>().sprite = BasicAttactSprite;
            FirstSkillButton.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[selectedCharacterIndex].skills[0].skillImage;
            SecondSkillButton.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[selectedCharacterIndex].skills[1].skillImage;
            ThirdSkillButton.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[selectedCharacterIndex].skills[2].skillImage;
            FourthSkillButton.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[selectedCharacterIndex].skills[3].skillImage;

            BasicAttackButton.GetChild(2).GetComponent<TextMeshProUGUI>().text = "일반 공격";
            FirstSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.instance.playCharacters[selectedCharacterIndex].skills[0].skillName;
            SecondSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.instance.playCharacters[selectedCharacterIndex].skills[1].skillName;
            ThirdSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.instance.playCharacters[selectedCharacterIndex].skills[2].skillName;
            FourthSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.instance.playCharacters[selectedCharacterIndex].skills[3].skillName;

            FirstSkillButton.GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => {
                GameManager.instance.playCharacters[selectedCharacterIndex].skills[0].skillActivate.Invoke();
            });

            SecondSkillButton.GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => {
                GameManager.instance.playCharacters[selectedCharacterIndex].skills[1].skillActivate.Invoke();
            });

            ThirdSkillButton.GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => {
                GameManager.instance.playCharacters[selectedCharacterIndex].skills[2].skillActivate.Invoke();
            });

            FourthSkillButton.GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => {
                GameManager.instance.playCharacters[selectedCharacterIndex].skills[3].skillActivate.Invoke();
            });

            if (GameManager.instance.playCharacters[selectedCharacterIndex].level < 6)
            {
                ThirdSkillButton.GetChild(1).GetComponent<Image>().color = new Color(150f / 255f, 150f / 255f, 150f / 255f);
                ThirdSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(100f / 255f, 100f / 255f, 100f / 255f);
                ThirdSkillButton.GetComponent<Toggle>().interactable = false;
            }
            else
            {
                ThirdSkillButton.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1);
                ThirdSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0);
                ThirdSkillButton.GetComponent<Toggle>().interactable = true;
            }

            if (GameManager.instance.playCharacters[selectedCharacterIndex].level < 10)
            {
                FourthSkillButton.GetChild(1).GetComponent<Image>().color = new Color(150f / 255f, 150f / 255f, 150f / 255f);
                FourthSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(100f / 255f, 100f / 255f, 100f / 255f);
                FourthSkillButton.GetComponent<Toggle>().interactable = false;
            }
            else
            {
                FourthSkillButton.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1);
                FourthSkillButton.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0);
                FourthSkillButton.GetComponent<Toggle>().interactable = true;
            }
        }
    }

    public static void ShowCharactersInfo(int characterIdx)
    {
        tempText = infoText.text;

        string text = string.Format("{0}\nHP: {1}/{2}  MP: {3}/{4}\n공격력: {5}  방어력: {6}%\n크리티컬: {7}%  상태이상저항: {8}%"
            , GameManager.instance.playCharacters[characterIdx].name
            , GameManager.instance.playCharacters[characterIdx].currHP
            , GameManager.instance.playCharacters[characterIdx].maxHP
            , GameManager.instance.playCharacters[characterIdx].currMP
            , GameManager.instance.playCharacters[characterIdx].maxMP
            , GameManager.instance.playCharacters[characterIdx].currAtk
            , GameManager.instance.playCharacters[characterIdx].currDefRate * 100
            , GameManager.instance.playCharacters[characterIdx].currCriticalRate * 100
            , GameManager.instance.playCharacters[characterIdx].currStatusEffectRate * 100
            );

        infoText.text = text;
    }

    public static void ShowSkillsInfo(int characterIdx, int skillIdx)
    {
        tempText = infoText.text;

        string text;
        if (skillIdx != -1)
        {
            text = string.Format("{0}: {1}"
                , GameManager.instance.playCharacters[characterIdx].skills[skillIdx].skillName
                , GameManager.instance.playCharacters[characterIdx].skills[skillIdx].skillInfo
                );
        }
        else
        {
            text = "일반 공격";
        }

        infoText.text = text;
    }

    public static void ShowEnemyInfo(int enemyIdx)
    {
        tempText = infoText.text;

        string text = string.Format("{0}\nHP: {1}/{2}\n공격력: {3}  방어력: {4}%\n크리티컬: {5}%  상태이상저항: {6}%"
            , GameManager.instance.inGameEnemies[enemyIdx].name
            , GameManager.instance.inGameEnemies[enemyIdx].currHP
            , GameManager.instance.inGameEnemies[enemyIdx].maxHP
            , GameManager.instance.inGameEnemies[enemyIdx].currAtk
            , GameManager.instance.inGameEnemies[enemyIdx].currDefRate * 100
            , GameManager.instance.inGameEnemies[enemyIdx].currCriticalRate * 100
            , GameManager.instance.inGameEnemies[enemyIdx].currStatusEffectRate * 100
            );

        infoText.text = text;
    }

    public static void ClearText()
    {
        tempText = "";
        infoText.text = tempText;
    }

    public static void ReturnText()
    {
        infoText.text = tempText;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
