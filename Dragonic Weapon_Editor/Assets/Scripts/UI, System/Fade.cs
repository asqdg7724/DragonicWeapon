using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool isFadeEnded;
    public Image FadeImage;

    // Start is called before the first frame update
    void Start()
    {
        isFadeEnded = false;

        StartCoroutine(FadeIn(1f, true));
    }

    IEnumerator FadeIn(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float percent = t / fadeTime;

            if (isFadeEnded)

                FadeImage.color = new Color(FadeImage.color.r,
                                            FadeImage.color.g,
                                            FadeImage.color.b,
                                            Mathf.Lerp(1f, 0, percent));


            yield return null;

        }

        isFadeEnded = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
