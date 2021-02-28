using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    CandyManager candyManager;

    public bool gameActive = false;
    public float score;
    public float chocolateScore;
    public float sweetScore;
    public float sourScore;
    public float timer;
    [SerializeField] float fadeOutTime;
    [SerializeField] float hurryTimer = 60;
    [SerializeField] float fadeOutTargetAlpha;
    [SerializeField] Image fader;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject countdown;

    //Gameover DOTweens
    [SerializeField] GameObject GOScreen;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject chocolateGOText;
    [SerializeField] GameObject sweetsGOText;
    [SerializeField] GameObject sourGOText;
    [SerializeField] GameObject totalGOText;
    [SerializeField] GameObject mainMenuBtn;
    [SerializeField] GameObject playAgainBtn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        candyManager = FindObjectOfType<CandyManager>();
        Begin();
    }

    private void Begin()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color currentColor = fader.color;
        while(Math.Abs(currentColor.a + fadeOutTargetAlpha) >= 0.001f)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, fadeOutTargetAlpha, fadeOutTime * Time.deltaTime);
            fader.color = currentColor;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Countdown());
    } 

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    IEnumerator Countdown()
    {
        countdown.GetComponent<DOTweenAnimation>().DOPlay();
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO!";
        gameActive = true;
        StartCoroutine(GameTimer());
        candyManager.WaveBegin();
        yield return new WaitForSeconds(1);
        DOTween.SmoothRewind(countdown);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        timer -= Time.deltaTime;
    }

    IEnumerator GameTimer()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(0);
            if (timer <= hurryTimer)
            {
                candyManager.timeRunningOut = true;
            }
            if(timer <= 0)
            {
                print("Time's up. Game over.");
                gameActive = false;
                StartCoroutine(GameOverSequence());
            }
            if (gameActive)
            {
                //print("Time: " + timer);
            }

        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator GameOverSequence()
    {
        StartCoroutine(FadeIn(GOScreen));
        Cursor.lockState = CursorLockMode.None;
        DOTween.Play(gameOverText);
        DOTween.Play(chocolateGOText);
        DOTween.Play(sweetsGOText);
        DOTween.Play(sourGOText);
        DOTween.Play(totalGOText);
        DOTween.Play(mainMenuBtn);
        DOTween.Play(playAgainBtn);
        yield return new WaitForSeconds(2);
        chocolateGOText.gameObject.GetComponent<TextMeshProUGUI>().text = "Chocolates Cherished: " + chocolateScore.ToString();
        yield return new WaitForSeconds(1);
        sweetsGOText.gameObject.GetComponent<TextMeshProUGUI>().text = "Sweets Saved: " + sweetScore.ToString();
        yield return new WaitForSeconds(1);
        sourGOText.gameObject.GetComponent<TextMeshProUGUI>().text = "Sours Savored: " + sourScore.ToString();
        yield return new WaitForSeconds(1);
        totalGOText.gameObject.GetComponent<TextMeshProUGUI>().text = "Total: " + score.ToString();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeIn(GameObject targetFade)
    {
        float targetAlpha = 1.0f;
        float currentAlpha = targetFade.GetComponent<CanvasGroup>().alpha;
        while (Math.Abs(currentAlpha - targetAlpha) >= .001f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, 5f * Time.deltaTime);
            targetFade.GetComponent<CanvasGroup>().alpha = currentAlpha;
            yield return null;
        }
    }
}
