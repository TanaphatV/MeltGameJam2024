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
    [SerializeField] private Image waterSurfaceImage;
    private float minimumGoodScore;
    private float maximumGoodScore;
    [SerializeField] private Button nextButton;
    private ProcessingStation station;

    public IEnumerator Init(float minimumGoodScore, float maximumGoodScore, ProcessingStation s)
    {
        station = s;
        isStartMinigame = true;
        goodScoreImage.fillAmount = 1.0f - (minimumGoodScore / 100f);
        failScoreImage.fillAmount = 1.0f - (maximumGoodScore / 100f);
        this.maximumGoodScore = maximumGoodScore;
        this.minimumGoodScore = minimumGoodScore;
        yield return EnableMouseInput();
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
        yield return null;
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
                station.currentItemCraftingStatus = CraftingStatus.Completed;
            }
            else
            {
                Debug.Log("You fail");
                station.currentItemCraftingStatus = CraftingStatus.Failed;
            }
            isStartMinigame = false;
        }
    }

    IEnumerator HoldCoroutine()
    {
        while (isHolding && value < 100f)
        {
            value += Time.deltaTime * 20f;
            fillWaterImg.fillAmount = value / 100f;
            waterSurfaceImage.fillAmount = value / (100 - (maximumGoodScore - minimumGoodScore));
            yield return null;
        }
        if (value >= 100f)
        {
            isStartMinigame = false;
            station.currentItemCraftingStatus = CraftingStatus.Failed;
        }
    }
}
