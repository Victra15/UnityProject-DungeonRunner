using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public GameManager gameManager;
    Button ReturnToTitleButton;
    // Start is called before the first frame update
    void Start()
    {
        ReturnToTitleButton = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        ReturnToTitleButton.onClick.AddListener(gameManager.ReturnToTitleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
