using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldPannel : MonoBehaviour
{
    public static GoldPannel instance;
    TextMeshProUGUI curr_gold;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        curr_gold = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void updateGold(int gold)
    {
        curr_gold.text = string.Format("{0}", gold);
    }
}
