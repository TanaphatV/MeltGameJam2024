using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseQTEManager : MonoBehaviour
{
    private bool isHolding = false;
    private float value = 0f;
    private Coroutine holdCoroutine;
    private bool isStartMinigame;
    [SerializeField] private Image fillWaterImg;
    [SerializeField] private Image goodScoreImage;
    [SerializeField] private Image failScoreImage;
    [SerializeField] private float minimumGoodScore;
    [SerializeField] private float maximumGoodScore;
    [SerializeField] private Button nextButton;

    private void Start()
    {
        Init(minimumGoodScore, maximumGoodScore);
        isStartMinigame = true;
        StartCoroutine(EnableMouseInput());
    }

    public void Init(float minimumGoodScore, float maximumGoodScore)
    {
        goodScoreImage.fillAmount = 1.0f - (minimumGoodScore / 100f);
        failScoreImage.fillAmount = 1.0f - (maximumGoodScore / 100f);
    }

    private IEnumerator EnableMouseInput()
    {
        while (isStartMinigame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartHolding();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopHolding();
            }
            yield return null;
        }
        nextButton.gameObject.SetActive(true);
    }

    void StartHolding()
    {
        if (holdCoroutine == null)
        {
            isHolding = true;
            holdCoroutine = StartCoroutine(HoldCoroutine());
        }
    }

    void StopHolding()
    {
        isHolding = false;
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
            if (value > minimumGoodScore && value < maximumGoodScore)
            {
                Debug.Log("Nice");
            }
            else
            {
                Debug.Log("You fail");
            }
            isStartMinigame = false;
        }
    }

    IEnumerator HoldCoroutine()
    {
        while (isHolding && value < 100f)
        {
            value += Time.deltaTime * 20f;
            //Debug.Log("Value: " + value);
            fillWaterImg.fillAmount = value / 100f;
            yield return null;
        }
        if (value >= 100f)
        {
            isStartMinigame = false;
        }
    }
}
