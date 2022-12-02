using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public BGM_List BGMPlayer;
    public SE_List PlayerSFX;
    public SE_List EnemySFX;
    public SE_List ObjectSFX;

    public GameObject Respawn;

    public Text ScoreText;

    public Transform Player_Tr;
    public Transform Hit_Tr;
    public Transform Boss_Tr;

    public GameObject Player_Hit;
    public GameObject Player_Explosion;

    public GameObject Boss_Explosion;

    public Image FadeImage;
    public GameObject GameClearImg;
    public GameObject PauseImg;
    public GameObject GameOverImg;

    int score;

    public bool isFadeEnded;

    void Awake()
    {
        GM = this;

        score = 0;

        ScoreText.text = score.ToString();
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        
    }

    public void BossAppearEvent()
    {
        Respawn.SetActive(false);
        BGMPlayer.PartSoundPlay(1);
    }

    public void Enemy_Score_Up()
    {
        score = score + 100;
        ScoreText.text = score.ToString();
    }

    public void Power_Score_Up()
    {
        score = score + 1000;
        ScoreText.text = score.ToString();
        ObjectSFX.SoundPlay(1);
    }

    public void Boss_Score_Up()
    {
        score = score + 30000;
        ScoreText.text = score.ToString();
    }

    public void PlayerHitEffect()
    {
        Instantiate(Player_Hit, Hit_Tr.position , Quaternion.identity);
        PlayerSFX.SoundPlay(1);
    }

    public void PlayerExplosionEffect()
    {
        Instantiate(Player_Explosion, Player_Tr.position, Quaternion.identity);
        PlayerSFX.SoundPlay(2);
    }

    public void BossExplosionEffect()
    {
        Instantiate(Boss_Explosion, Boss_Tr.position, Quaternion.identity);
        EnemySFX.SoundPlay(1);
    }

    public void PowerUpSound()
    {
        ObjectSFX.SoundPlay(0);
    }

    public void EnemyDefeatSound()
    {
        EnemySFX.SoundPlay(0);
    }

    public void GamePauseOn()
    {
        PauseImg.SetActive(true);
        FadeImage.color = new Color(FadeImage.color.r,
                                    FadeImage.color.g,
                                    FadeImage.color.b,
                                    0.5f);
        Time.timeScale = 0f;
    }

    public void GamePauseOff()
    {
        PauseImg.SetActive(false);
        FadeImage.color = new Color(FadeImage.color.r,
                                    FadeImage.color.g,
                                    FadeImage.color.b,
                                    0f);
        Time.timeScale = 1f;
    }

    public void PlayerGameOver()
    {
        BGMPlayer.UISoundPlay(1);
        GameOverImg.SetActive(true);
        StartCoroutine(GameOver_Fade(3f, true));
    }

    public void PlayerGameClear()
    {
        Boss_Score_Up();
        BGMPlayer.UISoundPlay(0);
        PlayerCtrl.PC.ClearInvincible();
        GameClearImg.SetActive(true);
        StartCoroutine(GameClear_Fade(3f, true));
    }

    IEnumerator GameOver_Fade(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        yield return new WaitForSeconds(4f);

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
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    IEnumerator GameClear_Fade(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        yield return new WaitForSeconds(8f);

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
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
