using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Image TitleLog;
    public SE_List UISFX;
    public Button StartButton;
    public Button OptionsButton;
    public Button ExitButton;
    public Button OptionExitButton;
    public GameObject OptionMenu;
    public Image BlinkImage;
    public Image FadeImage;
    public bool isFadeEnded;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFadeEnded = false;

        TitleLog.enabled = false;
        StartButton.interactable = false;
        OptionsButton.interactable = false;
        ExitButton.interactable = false;

        StartCoroutine(BlinkScreen(1f, true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPress()
    {
        UISFX.SoundPlay(0);
        StartCoroutine(FadeIn(2f, true));
    }

    public void OptionButtonPress()
    {
        UISFX.SoundPlay(0);
        OptionMenu.SetActive(true);
        StartButton.interactable = false;
        OptionsButton.interactable = false;
        ExitButton.interactable = false;
    }

    public void OptionExitButtonPress()
    {
        UISFX.SoundPlay(1);
        OptionMenu.SetActive(false);
        StartButton.interactable = true;
        OptionsButton.interactable = true;
        ExitButton.interactable = true;
    }

    public void ExitButtonPress()
    {
        UISFX.SoundPlay(1);
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
        #else
        Application.Quit();
        
        #endif
    }

    IEnumerator BlinkScreen(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        yield return new WaitForSeconds(1f);

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float percent = t / fadeTime;

            if (isFadeEnded)

                BlinkImage.color = new Color(BlinkImage.color.r,
                                                   BlinkImage.color.g,
                                                   BlinkImage.color.b,
                                                   Mathf.Lerp(0, 1f, percent));

            TitleLog.enabled = true;
            StartButton.interactable = true;
            OptionsButton.interactable = true;
            ExitButton.interactable = true;


            BlinkImage.color = new Color(BlinkImage.color.r,
                                        BlinkImage.color.g,
                                        BlinkImage.color.b,
                                        Mathf.Lerp(1f, 0, percent));


            yield return null;

        }

        isFadeEnded = false;
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
                                            Mathf.Lerp(0, 1f, percent));
            yield return null;
        }

        isFadeEnded = false;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
