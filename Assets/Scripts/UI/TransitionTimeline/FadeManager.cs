using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeManager : MonoBehaviour
{
    #region Singleton
    public static FadeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] private Image fader;

    private void Start()
    {

    }
    public void StartFade(float fadeTime,float fadeInDelay, UnityAction onFadeInDone = null,UnityAction onFadeDone = null)
    {
        if(!fading)
            StartCoroutine(FadeIE(fadeTime, fadeInDelay, onFadeInDone, onFadeDone));
    }

    bool fading = false;

    IEnumerator FadeIE(float fadeTime,float fadeInDelay,UnityAction onFadeInDone,UnityAction onFadeDone)
    {
        fading = true;
        alpha = 0;
        float fadeSpeed = 2.0f / fadeTime;
        while(alpha < 1.0f)
        {
            ChangeFaderAlpha(fadeSpeed * Time.deltaTime);
            yield return null;
        }
        if (onFadeInDone != null)
            onFadeInDone();
        yield return new WaitForSeconds(fadeInDelay);
        while(alpha > 0)
        {
            ChangeFaderAlpha(-fadeSpeed * Time.deltaTime);
            yield return null;
        }
        if (onFadeDone != null)
            onFadeDone();
        fading = false;
    }
    float alpha;
    void ChangeFaderAlpha(float amount)
    {
        alpha += amount;
        alpha = Mathf.Clamp01(alpha);
        fader.color = new Color(0, 0, 0, alpha);
    }
}
