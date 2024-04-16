using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopWindowManager : MonoBehaviour
{
    [SerializeField] private GameObject gfx;
    [SerializeField] private Button freezeReduceButton;
    [SerializeField] private Image freezeBar;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject maxLv;
    private Shop shop;
    private int freezeLv = 1;

    private void Start()
    {
        freezeReduceButton.onClick.AddListener(() => UpgradeFreezeReduce(shop.freezer[freezeLv - 1] as FreezerUpgrade));
    }
    public void Init(Shop shop)
    {
        this.shop = shop;
        shop.freezer[0].UpgradeEffect();
    }

    public void OpenPanel()
    {
        gfx.SetActive(true);
    }
    public void ClosePanel()
    {
        gfx.SetActive(false);
    }

    public void UpgradeFreezeReduce(FreezerUpgrade upgrade)
    {
        if (IsPlayerHasEnoughCoin(upgrade))
        {
            PlayerResources.instance.coin -= upgrade.cost;
            upgrade.UpgradeEffect();
            StartCoroutine(ProgressBar(freezeBar, (33.5f*(freezeLv-1))/100f, (33.5f * freezeLv / 100f)));
            freezeLv++;
            freezeReduceButton.onClick.RemoveAllListeners();
            
            if (freezeLv <= shop.freezer.Count)
            {
                freezeReduceButton.onClick.AddListener(() => UpgradeFreezeReduce(shop.freezer[freezeLv - 1] as FreezerUpgrade));
                costText.text = shop.freezer[freezeLv - 1].cost.ToString();
            }
            else
            {
                costText.gameObject.SetActive(false);
                maxLv.SetActive(true);
                freezeReduceButton.interactable = false;
            }
        }
    }

    private IEnumerator ProgressBar(Image img, float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.5f) // Remove the semicolon here
        {
            float t = elapsedTime / 0.5f; // Normalize elapsedTime to be between 0 and 1
            float fillAmount = Mathf.Lerp(start, end, t);
            img.fillAmount = fillAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        img.fillAmount = end; // Update the image fill amount to the final value
    }

    public bool IsPlayerHasEnoughCoin(ShopUpgrade upgrade)
    {
        return PlayerResources.instance.coin > upgrade.cost && PlayerResources.instance.coin - upgrade.cost >= 0;
    }


}
