using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterTooltip : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    public static int selectedCharacterIdx;
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        CommandPanel.ShowCharactersInfo(transform.GetSiblingIndex());
        if(GameManager.instance.IsPlayerSelectModeOn)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CommandPanel.ReturnText();
        if (GameManager.instance.IsPlayerSelectModeOn)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.IsPlayerSelectModeOn)
        {
            GameManager.instance.IsPlayerSelectModeOn = false;
            transform.GetChild(2).gameObject.SetActive(false);
            selectedCharacterIdx = transform.GetSiblingIndex();
        }
    }
}
