using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static int selectedEnemyIdx;

    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        CommandPanel.ShowEnemyInfo(transform.GetSiblingIndex());
        if(GameManager.instance.IsEnemySelectModeOn)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CommandPanel.ReturnText();
        if (GameManager.instance.IsEnemySelectModeOn)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.IsEnemySelectModeOn)
        {
            GameManager.instance.IsEnemySelectModeOn = false;
            transform.GetChild(0).gameObject.SetActive(false);
            selectedEnemyIdx = transform.GetSiblingIndex();
        }
    }
}
