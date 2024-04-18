using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEMoldingPanel : MonoBehaviour
{
    private bool isHolding = false;
    private float value = 0f;
    private float value2 = 0f;
    private Coroutine holdCoroutine;
    private bool isStartMinigame;
    [SerializeField] private Image fillWaterImg;
    [SerializeField] private Image goodScoreImage;
    [SerializeField] private Image failScoreImage;
    [SerializeField] private Image waterSurfaceImage;
    private float minimumGoodScore;
    private float maximumGoodScore;
    [SerializeField] private Button nextButton;
    private BaseQTEManager qteManager;
    private float decreaseDifVal = 0;

    public IEnumerator Init(float minimumGoodScore, float maximumGoodScore, BaseQTEManager bm)
    {
        qteManager = bm;

        isStartMinigame = true;

        value = 0f;
        value2 = 0f;
        fillWaterImg.fillAmount = 0.0f;
        waterSurfaceImage.fillAmount = 0f;

        goodScoreImage.fillAmount = 1.0f - ((minimumGoodScore-decreaseDifVal) / 100f);
        failScoreImage.fillAmount = 1.0f - (maximumGoodScore / 100f);
        this.maximumGoodScore = maximumGoodScore;
        this.minimumGoodScore = (minimumGoodScore - decreaseDifVal);

        yield return EnableMouseInput();
    }

    public void DecreaseDifficulty()
    {
        decreaseDifVal = 10;
    }

    private IEnumerator EnableMouseInput()
    {
        while (isStartMinigame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartHolding();
            }
            if (Input.GetMouseButtonUp(0))
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
            //Debug.Log("Hold");
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
            isStartMinigame = false;
            if (value > minimumGoodScore && value < maximumGoodScore)
            {
                Debug.Log("Nice");
                StartCoroutine(qteManager.StartQTEControlTemperature());
            }
            else
            {
                qteManager.GetStation.currentItemCraftingStatus = CraftingStatus.Failed;
            }
            
        }
    }



    private IEnumerator HoldCoroutine()
    {
        float incrementRate1 = maximumGoodScore / 5.0f;
        float incrementRate2 = 100f / 4.7f;
        while (isHolding && value < 100f)
        {
            value = Mathf.Clamp(value + incrementRate1 * Time.deltaTime, 0f, 100 - (maximumGoodScore - minimumGoodScore));
            value2 = Mathf.Clamp(value2 + incrementRate2 * Time.deltaTime, 0f, 100f);

            fillWaterImg.fillAmount = value / 100f;
            waterSurfaceImage.fillAmount = value2 / 100f;
            
            yield return null;
        }
        if (value >= 100f)
        {
            isStartMinigame = false;
            //station.currentItemCraftingStatus = CraftingStatus.Failed;
        }
    }
}
