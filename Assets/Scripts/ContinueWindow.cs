using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueWindow : MonoBehaviour
{
    Button ContinueButton;
    Button ReturnButton;
    // Start is called before the first frame update

    void Start()
    {
        ContinueButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        ContinueButton.onClick.AddListener(GameManager.instance.GoToIngameScene);

        ReturnButton = transform.GetChild(0).GetChild(2).GetComponent<Button>();
        ReturnButton.onClick.AddListener(() => { gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
