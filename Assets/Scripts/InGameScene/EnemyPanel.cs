using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    public static EnemyPanel instance;

    GameObject EnemyPrefab;
    [SerializeField] PlayerPanel playerPanel;
    [SerializeField] BackGround backGround;
    Vector2 EnemyPanelPos;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnEnemy()
    {
        for (int loop = 0; loop < GameManager.instance.inGameEnemies.Count; loop++)
        {
            EnemyPrefab = Instantiate(GameManager.instance.inGameEnemies[loop].EnemyPrefab, transform);
            EnemyPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(100 + loop * 200, 0);
            EnemyPrefab.transform.GetChild(1).GetComponent<Slider>().maxValue = GameManager.instance.inGameEnemies[loop].maxHP;
            EnemyPrefab.transform.GetChild(1).GetComponent<Slider>().value = GameManager.instance.inGameEnemies[loop].currHP;
        }

        PanelInitialize();
        StartCoroutine(PanelMove());
    }

    public void EnemyDeath()
    {
        for(int loop = 0; loop < GameManager.instance.inGameEnemies.Count; loop++)
        {
            if(GameManager.instance.inGameEnemies[loop].currHP <= 0)
            {
                GameManager.instance.obtain_exp += GameManager.instance.inGameEnemies[loop].exp;
                GameManager.instance.obtain_gold += GameManager.instance.inGameEnemies[loop].gold;
                GameManager.instance.inGameEnemies.RemoveAt(loop);
                GameManager.instance.inGameEnemiesNo.RemoveAt(loop);
                Destroy(transform.GetChild(loop).gameObject);
            }
        }
    }

    IEnumerator PanelMove()
    {
        float speed = 1.0f;
        backGround.MoveAllLayers();

        while (EnemyPanelPos.x >= 440)
        {
            EnemyPanelPos.x -= speed;
            GetComponent<RectTransform>().anchoredPosition = EnemyPanelPos;
            yield return null;
        }

        backGround.StopAllLayers();
        yield return GameManager.instance.BattleStart();
    }

    public void PanelInitialize()
    {
        EnemyPanelPos = new Vector2(2000, 120);
        GetComponent<RectTransform>().anchoredPosition = EnemyPanelPos;
    }

    public bool IsAllEnemyActionFinished()
    {
        return true;
    }

    void Start()
    {
        Vector2 EnemyPanelPos = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
