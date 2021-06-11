using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel instance; // To debug

    [SerializeField] CommandPanel commandPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[0].characterSprite;
        transform.GetChild(0).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[0].characterAnimator;
        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[1].characterSprite;
        transform.GetChild(1).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[1].characterAnimator;
        transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[2].characterSprite;
        transform.GetChild(2).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[2].characterAnimator;

        
        transform.GetChild(0).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
        transform.GetChild(1).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
        transform.GetChild(2).GetComponent<Toggle>().onValueChanged.AddListener((bool onclicked) => { commandPanel.characterSelected(onclicked, GetComponent<ToggleGroup>()); });
    }

    public void load() // To Debug
    {
        transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[0].characterSprite;
        transform.GetChild(0).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[0].characterAnimator;
        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[1].characterSprite;
        transform.GetChild(1).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[1].characterAnimator;
        transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = GameManager.instance.playCharacters[2].characterSprite;
        transform.GetChild(2).GetChild(1).GetComponent<Animator>().runtimeAnimatorController = GameManager.instance.playCharacters[2].characterAnimator;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
