using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DataBaseManager dataBaseManager;
    [SerializeField] CharacterStatusPanel characterStatusPanel;
    Button button;
    CharacterStat character;
    void Start()
    {
        character = dataBaseManager.GetComponent<DataBaseManager>().Characters[transform.GetSiblingIndex()];
        GetComponent<Image>().sprite = character.characterPortrait;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { characterStatusPanel.characterSelectButtonClick(character); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
