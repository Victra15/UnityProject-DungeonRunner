using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutPanel : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;
    private void Start()
    {

    }

    public void PanelFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void PanelFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    // Start is called before the first frame update
    IEnumerator FadeOut()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
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
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time -= Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
