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
        }
    }

    public void StopAllLayers()
    {
        for (int loop = 0; loop < BackGroundLayers.Length; loop++)
        {
            BackGroundLayers[loop].stop();
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
