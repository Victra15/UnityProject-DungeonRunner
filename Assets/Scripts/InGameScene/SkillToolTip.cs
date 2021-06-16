using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        int ActiveCharacterIndex = PlayerPanel.instance.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault().transform.GetSiblingIndex();
        CommandPanel.ShowSkillsInfo(ActiveCharacterIndex, transform.GetSiblingIndex() - 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CommandPanel.ReturnText();
    }
}
