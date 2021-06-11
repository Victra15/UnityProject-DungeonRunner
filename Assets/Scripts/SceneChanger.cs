using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    [SerializeField] Image Panel;
    float time;
    float F_time = 1f;
    private void Start()
    {
        instance = this;
        StartCoroutine(FadeIn());
    }
    public void goToScene(string sceneName)
    {
        StartCoroutine(nextScene(sceneName));
    }
    public IEnumerator nextScene(string sceneName)
    {
        yield return FadeOut();

        SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    IEnumerator FadeOut()
    {
        Panel.gameObject.SetActive(true);
        time = 0;
        Color alpha = Panel.color;
        alpha = new Color(0, 0, 0, 0);
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }


        yield return null;
    }

    IEnumerator FadeIn()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        time = 0;
        alpha = new Color(0, 0, 0, 1);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
