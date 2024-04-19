using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class DeathScreenUI : MonoBehaviour
{
    float alpha;
    public float fadeTime;
    public float fadeInDelay;
    [SerializeField] Image fader;
    [SerializeField] TextMeshProUGUI youDiedText;
    [SerializeField] TextMeshProUGUI reasonText;
    public void StartFade(string reason, UnityAction onCompletelyBlack)
    {
        reasonText.text = reason;
        if (!fading)
            StartCoroutine(FadeIE(onCompletelyBlack));
    }

    bool fading = false;

    IEnumerator FadeIE(UnityAction onCompletelyBlack)
    {
        fading = true;
        alpha = 0;
        float fadeSpeed = 2.0f / fadeTime;
        while (alpha < 1.0f)
        {
            ChangeFaderAlpha(fadeSpeed * Time.deltaTime);
            yield return null;
        }
        if (onCompletelyBlack != null)
            onCompletelyBlack();

        yield return new WaitForSeconds(fadeInDelay);
        while (alpha > 0)
        {
            ChangeFaderAlpha(-fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fading = false;
    }

    void ChangeFaderAlpha(float amount)
    {
        alpha += amount;
        alpha = Mathf.Clamp01(alpha);
        fader.color = new Color(0, 0, 0, alpha);
        reasonText.color = new Color(1, 1, 1, alpha);
        youDiedText.color = new Color(1, 1,1, alpha);
    }
}
