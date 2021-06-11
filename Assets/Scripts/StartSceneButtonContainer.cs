using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartSceneButtonContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button StartButton;
    [SerializeField] Button ContinueButton;
    [SerializeField] Button ExitButton;

    void Start()
    {
        StartButton = transform.GetChild(0).GetComponent<Button>();
        ContinueButton = transform.GetChild(1).GetComponent<Button>();
        ExitButton = transform.GetChild(2).GetComponent<Button>();

        StartButton.onClick.AddListener(GameManager.instance.GetComponent<GameManager>().GotoCharacterSelectScene);
        /*ContinueButton.onClick.AddListener(GameManager)*/
        ExitButton.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
