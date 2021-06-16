using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    [SerializeField] Button ReturnToTitleButton;
    // Start is called before the first frame update
    void Start()
    {
        ReturnToTitleButton.onClick.AddListener(GameManager.instance.ReturnToTitleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
