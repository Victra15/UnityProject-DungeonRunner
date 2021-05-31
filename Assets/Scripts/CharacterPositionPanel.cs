using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterPositionPanel : MonoBehaviour
{
    List<Image> characterImages;
    List<Animator> characterAnimators;

    public void characterSelectButtonClick(CharacterStat character)
    {
        for (int loop = 0; loop < characterImages.Count; loop++)
        {
            if (characterImages[loop].sprite == null)
            {
                characterImages[loop].sprite = character.characterSprite;
                characterImages[loop].color = new Color(1, 1, 1, 1);
                characterAnimators[loop].runtimeAnimatorController = character.characterAnimator;
                break;
            }
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        characterImages = new List<Image>();
        characterAnimators = new List<Animator>();

        characterImages.Add(transform.GetChild(0).GetComponent<Image>());
        characterImages.Add(transform.GetChild(1).GetComponent<Image>());
        characterImages.Add(transform.GetChild(2).GetComponent<Image>());

        characterAnimators.Add(transform.GetChild(0).GetComponent<Animator>());
        characterAnimators.Add(transform.GetChild(1).GetComponent<Animator>());
        characterAnimators.Add(transform.GetChild(2).GetComponent<Animator>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
