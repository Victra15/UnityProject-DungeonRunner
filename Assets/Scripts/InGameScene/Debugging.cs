using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
    public static Debugging instance;

    Button LoadButton;
    Button FightButton;
    Button PortalButton;
    Button RandomEventButton;
    Button RestZoneButton;
    Button TreasureButton;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadButton = transform.GetChild(0).GetComponent<Button>();
        FightButton = transform.GetChild(1).GetComponent<Button>();
        PortalButton = transform.GetChild(2).GetComponent<Button>();
        RandomEventButton = transform.GetChild(3).GetComponent<Button>();
        RestZoneButton = transform.GetChild(4).GetComponent<Button>();
        TreasureButton = transform.GetChild(5).GetComponent<Button>();

        LoadButton.onClick.AddListener(GameManager.instance.loadCharacters);

        FightButton.onClick.AddListener(UninteractableAll);
        FightButton.onClick.AddListener(GameManager.instance.SpawnEnemy);
        FightButton.onClick.AddListener(EnemyPanel.instance.SpawnEnemy);
    }

    public void UninteractableAll()
    {
        FightButton.interactable = false;
        PortalButton.interactable = false;
        RandomEventButton.interactable = false;
        RestZoneButton.interactable = false;
        TreasureButton.interactable = false;
    }

    public void interactableAll()
    {
        FightButton.interactable = true;
        PortalButton.interactable = true;
        RandomEventButton.interactable = true;
        RestZoneButton.interactable = true;
        TreasureButton.interactable = true;
    }
}
