using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    MoveBackground[] BackGroundLayers;

    // Start is called before the first frame update
    public void MoveAllLayers()
    {
        for(int loop = 0; loop < BackGroundLayers.Length; loop++)
        {
            BackGroundLayers[loop].move();
            PlayerPanel.instance.PanelCharacters[0].GetComponent<Animator>().SetBool("isMoving", true);
            PlayerPanel.instance.PanelCharacters[1].GetComponent<Animator>().SetBool("isMoving", true);
            PlayerPanel.instance.PanelCharacters[2].GetComponent<Animator>().SetBool("isMoving", true);
        }
    }

    public void StopAllLayers()
    {
        for (int loop = 0; loop < BackGroundLayers.Length; loop++)
        {
            BackGroundLayers[loop].stop();
            PlayerPanel.instance.PanelCharacters[0].GetComponent<Animator>().SetBool("isMoving", false);
            PlayerPanel.instance.PanelCharacters[1].GetComponent<Animator>().SetBool("isMoving", false);
            PlayerPanel.instance.PanelCharacters[2].GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    void Start()
    {
        BackGroundLayers = GetComponentsInChildren<MoveBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
