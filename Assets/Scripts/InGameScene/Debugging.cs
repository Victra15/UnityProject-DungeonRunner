using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
    Button LoadButton;
    Button FightButton;
    Button PortalButton;
    Button RandomEventButton;
    Button RestZoneButton;
    Button TreasureButton;
    

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
        FightButton.onClick.AddListener(GameManager.instance.SpawnEnemy);
        FightButton.onClick.AddListener(EnemyPanel.instance.SpawnEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
