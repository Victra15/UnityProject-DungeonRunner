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

    Toggle BasicAttackButton;
    Toggle FirstSkillButton;
    Toggle SecondSkillButton;
    Toggle ThirdSkillButton;
    Toggle FourthSkillButton;

    Image BasicAttackButtonImage;
    Image FirstSkillButtonImage;
    Image SecondSkillButtonImage;
    Image ThirdSkillButtonImage;
    Image FourthSkillButtonImage;

    TextMeshProUGUI BasicAttactButtonText;
    TextMeshProUGUI FirstSkillButtonText;
    TextMeshProUGUI SecondSkillButtonText;
    TextMeshProUGUI ThirdSkillButtonText;
    TextMeshProUGUI FourthSkillButtonText;

    void Start()
    {
        BasicAttackButton = transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
        FirstSkillButton = transform.GetChild(0).GetChild(1).GetComponent<Toggle>();
        SecondSkillButton = transform.GetChild(0).GetChild(2).GetComponent<Toggle>();
        ThirdSkillButton = transform.GetChild(0).GetChild(3).GetComponent<Toggle>();
        FourthSkillButton = transform.GetChild(0).GetChild(4).GetComponent<Toggle>();

        BasicAttackButtonImage = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
        FirstSkillButtonImage = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();
        SecondSkillButtonImage = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>();
        ThirdSkillButtonImage = transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Image>();
        FourthSkillButtonImage = transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Image>();

        BasicAttactButtonText = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        FirstSkillButtonText = transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        SecondSkillButtonText = transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
        ThirdSkillButtonText = transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();
        FourthSkillButtonText = transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();

        transform.GetChild(0).GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { });
        transform.GetChild(0).GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { });
        transform.GetChild(0).GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { });
        transform.GetChild(0).GetChild(3).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { });
        transform.GetChild(0).GetChild(4).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { });
    }

    public void characterSelected(bool onclicked, ToggleGroup toggleGroup)
    {
        if(!onclicked)
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

            int index = toggleGroup.ActiveToggles().FirstOrDefault().transform.GetSiblingIndex();

            BasicAttackButtonImage.sprite = BasicAttactSprite;
            FirstSkillButtonImage.sprite = GameManager.instance.playCharacters[index].skills[0].skillImage;
            SecondSkillButtonImage.sprite = GameManager.instance.playCharacters[index].skills[1].skillImage;
            ThirdSkillButtonImage.sprite = GameManager.instance.playCharacters[index].skills[2].skillImage;
            FourthSkillButtonImage.sprite = GameManager.instance.playCharacters[index].skills[3].skillImage;

            BasicAttactButtonText.text = "일반 공격";
            FirstSkillButtonText.text = GameManager.instance.playCharacters[index].skills[0].skillName;
            SecondSkillButtonText.text = GameManager.instance.playCharacters[index].skills[1].skillName;
            ThirdSkillButtonText.text = GameManager.instance.playCharacters[index].skills[2].skillName;
            FourthSkillButtonText.text = GameManager.instance.playCharacters[index].skills[3].skillName;

            if (GameManager.instance.playCharacters[index].level < 6)
            {
                ThirdSkillButtonImage.color = new Color(150f/255f, 150f/255f, 150f/255f);
                ThirdSkillButtonText.color = new Color(100f/255f, 100f/255f, 100f/255f);
                ThirdSkillButton.interactable = false;
            }
            else
            {
                ThirdSkillButtonImage.color = new Color(1, 1, 1);
                ThirdSkillButtonText.color = new Color(0, 0, 0);
                ThirdSkillButton.interactable = true;
            }
            
            if (GameManager.instance.playCharacters[index].level < 10)
            {
                FourthSkillButtonImage.color = new Color(150f/255f, 150f/255f, 150f/255f);
                FourthSkillButtonText.color = new Color(100f/255f, 100f/255f, 100f/255f);
                FourthSkillButton.interactable = false;
            }
            else
            {
                FourthSkillButtonImage.color = new Color(1, 1, 1);
                FourthSkillButtonText.color = new Color(0, 0, 0);
                FourthSkillButton.interactable = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
