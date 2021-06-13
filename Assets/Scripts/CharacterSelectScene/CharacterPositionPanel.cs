using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterPositionPanel : MonoBehaviour
{
    List<Image> characterImages;
    List<Animator> characterAnimators;
    public GameObject ContinueWindow;
    public int[] playCharactersNo;

    public void characterSelectButtonClick(CharacterStat character)
    {
        if(playCharactersNo[Constants.PartyMemberCount - 1] == 0) // selected character is not Full
        {
            character.isSelected = true;

            for (int loop = 0; loop < playCharactersNo.Length; loop++)
            {
                if (playCharactersNo[loop] == 0)
                {
                    playCharactersNo[loop] = character.CharacterNo;
                    characterImages[loop].sprite = character.characterSprite;
                    characterImages[loop].color = new Color(1, 1, 1, 1);
                    characterAnimators[loop].runtimeAnimatorController = character.characterAnimator;
                    break;
                }
            }

            if (playCharactersNo[Constants.PartyMemberCount - 1] != 0) // selected character is Full
            {
                ContinueWindow.SetActive(true);
            }
        }
    }

    public void characterCancelButtonClick(CharacterStat character)
    {
        character.isSelected = false;
        for (int loop1 = playCharactersNo.Length - 1; loop1 >= 0; loop1--)
        {
            if (playCharactersNo[loop1] == character.CharacterNo)
            {
                for (int loop2 = loop1; loop2 < playCharactersNo.Length - 1; loop2++)
                {
                    playCharactersNo[loop2] = playCharactersNo[loop2 + 1];
                    characterImages[loop2].sprite = characterImages[loop2 + 1].sprite;
                    characterImages[loop2].color = characterImages[loop2 + 1].color;
                    characterAnimators[loop2].runtimeAnimatorController = characterAnimators[loop2 + 1].runtimeAnimatorController;
                }
                playCharactersNo[Constants.PartyMemberCount - 1] = 0;
                characterImages[Constants.PartyMemberCount - 1].sprite = null;
                characterImages[Constants.PartyMemberCount - 1].color = new Color(1, 1, 1, 0);
                characterAnimators[Constants.PartyMemberCount - 1].runtimeAnimatorController = null;
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

        playCharactersNo = GameManager.instance.GetComponent<GameManager>().playCharactersNo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
