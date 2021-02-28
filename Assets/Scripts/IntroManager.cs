using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject TitleImage;
    [SerializeField] GameObject Space;
    [SerializeField] GameObject PlayScreen;
    [SerializeField] GameObject Tutorial;
    [SerializeField] Image fader;
    [SerializeField] AudioSource cameraAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DOTween.Play(Space);
            DOTween.Play(TitleImage);
            DOTween.Play(PlayScreen);
        }
    }

    public void TutorialScreen()
    {
        DOTween.Restart(Tutorial);
        DOTween.SmoothRewind(PlayScreen);
        DOTween.Play(Tutorial);
    }

    public void TutorialBack()
    {
        DOTween.SmoothRewind(Tutorial);
        DOTween.Restart(PlayScreen);
        DOTween.Play(PlayScreen);
    }

    public void PlayGame()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float targetAlpha = 1.0f;
        Color currentColor = fader.color;
        while(Mathf.Abs(currentColor.a - targetAlpha) >= 0.001f)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, 10f * Time.deltaTime);
            fader.color = currentColor;
            yield return null;
        }
        StartCoroutine(BeginGame());
    }
    IEnumerator BeginGame()
    {
        while (cameraAudio.volume >= 0.001f)
        {
            cameraAudio.volume -= Time.deltaTime * 2;
            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}
