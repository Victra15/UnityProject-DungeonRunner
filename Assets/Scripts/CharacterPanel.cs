using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public DataBaseManager dataBaseManager;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<CharacterStat> characters = dataBaseManager.GetComponent<DataBaseManager>().Characters;
        Debug.Log(characters.Count);
        for (int loop = 0; loop < characters.Count; loop++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab);
            buttonObj.transform.SetParent(this.transform);
            buttonObj.transform.localScale = Vector3.one;
            if(characters[loop].characterPortrait != null)
            {
                buttonObj.GetComponent<Image>().sprite = characters[loop].characterPortrait;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
