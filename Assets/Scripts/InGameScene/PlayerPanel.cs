using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel instance; // To debug

    [SerializeField] CommandPanel commandPanel;
    List<Transform> PanelCharacters;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        PanelCharacters = new List<Transform>();

        PanelCharacters.Add(transform.GetChild(0).GetChild(1));
        PanelCharacters.Add(transform.GetChild(1).GetChild(1));
        PanelCharacters.Add(transform.GetChild(2).GetChild(1));

        PanelCharacters[0].GetComponent<Image>().sprite = GameManager.instance.playCharacters[0].characterSprite;
        PanelCharacters[1].GetComponent<Image>().sprite = GameManager.instance.playCharacters[1].characterSprite;
        PanelCharacters[2].GetComponent<Image>().sprite = GameManager.instance.playCharacters[2].characterSprite;

        PanelCharacters[0].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[0].characterAnimator;
        PanelCharacters[1].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[1].characterAnimator; 
        PanelCharacters[2].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[2].characterAnimator;

        transform.GetChild(0).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[0].maxHP;
        transform.GetChild(0).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[0].currHP;
        transform.GetChild(1).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[1].maxHP;
        transform.GetChild(1).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[1].currHP;
        transform.GetChild(2).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[2].maxHP;
        transform.GetChild(2).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[2].currHP;

        transform.GetChild(0).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[0].maxMP;
        transform.GetChild(0).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[0].currMP;
        transform.GetChild(1).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[1].maxMP;
        transform.GetChild(1).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[1].currMP;
        transform.GetChild(2).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[2].maxMP;
        transform.GetChild(2).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[2].currMP;

        transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
        transform.GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
        transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
    }
    public void PlayerDeath()
    {
        for (int loop = 0; loop < GameManager.instance.playCharacters.Length; loop++)
        {
            if (GameManager.instance.playCharacters[loop].currHP < 0)
            {
                
            }
        }
    }
    public void ReadyToBattle()
    {
        for(int loop = 0; loop < Constants.PartyMemberCount; loop++)
        {
            GameManager.instance.playCharacters[loop].isActionFinished = false;
            transform.GetChild(loop).GetComponent<Toggle>().interactable = true;
        }
    }

    public void ActionFinished(int FinishedCharacterIdx)
    {
        GameManager.instance.playCharacters[FinishedCharacterIdx].isActionFinished = true;
        transform.GetChild(FinishedCharacterIdx).GetComponent<Toggle>().isOn = false;
        transform.GetChild(FinishedCharacterIdx).GetComponent<Toggle>().interactable = false;
    }

    public void PlayerActionLock()
    {
        for (int loop = 0; loop < Constants.PartyMemberCount; loop++)
        {
            transform.GetChild(loop).GetComponent<Toggle>().interactable = false;
        }

    }

    public void PlayerActionUnLock()
    {
        for (int loop = 0; loop < Constants.PartyMemberCount; loop++)
        {
            transform.GetChild(loop).GetComponent<Toggle>().interactable = !GameManager.instance.playCharacters[loop].isActionFinished;
        }

        
    }

    public void load() // To Debug
    {
        PanelCharacters[0].GetComponent<Image>().sprite = GameManager.instance.playCharacters[0].characterSprite;
        PanelCharacters[1].GetComponent<Image>().sprite = GameManager.instance.playCharacters[1].characterSprite;
        PanelCharacters[2].GetComponent<Image>().sprite = GameManager.instance.playCharacters[2].characterSprite;

        PanelCharacters[0].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[0].characterAnimator;
        PanelCharacters[1].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[1].characterAnimator;
        PanelCharacters[2].GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[2].characterAnimator;

        transform.GetChild(0).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[0].maxHP;
        transform.GetChild(0).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[0].currHP;
        transform.GetChild(1).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[1].maxHP;
        transform.GetChild(1).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[1].currHP;
        transform.GetChild(2).GetChild(3).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[2].maxHP;
        transform.GetChild(2).GetChild(3).GetComponent<Slider>().value = GameManager.instance.playCharacters[2].currHP;

        transform.GetChild(0).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[0].maxMP;
        transform.GetChild(0).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[0].currMP;
        transform.GetChild(1).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[1].maxMP;
        transform.GetChild(1).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[1].currMP;
        transform.GetChild(2).GetChild(4).GetComponent<Slider>().maxValue = GameManager.instance.playCharacters[2].maxMP;
        transform.GetChild(2).GetChild(4).GetComponent<Slider>().value = GameManager.instance.playCharacters[2].currMP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
