using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopWindowManager : MonoBehaviour
{
    [SerializeField] private GameObject gfx;
    [SerializeField] private Button closeButton;
    #region JUST_VARIABLE
    [SerializeField] private Button wagonSpaceButton;
    [SerializeField] private Image wagonSpaceBar;
    [SerializeField] private TextMeshProUGUI wagonSpaceCostText;
    [SerializeField] private GameObject wagonSpaceMaxLv;

    [SerializeField] private Button pickaxeButton;
    [SerializeField] private Image pickaxeBar;
    [SerializeField] private TextMeshProUGUI pickaxeCostText;
    [SerializeField] private GameObject pickaxeMaxLv;

    [SerializeField] private Button freezeReduceButton;
    [SerializeField] private Image freezeBar;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject maxLv;

    [SerializeField] private Button reputaButton;
    [SerializeField] private Image reputaBar;
    [SerializeField] private TextMeshProUGUI reputaCostText;
    [SerializeField] private GameObject reputaMaxLv;

    [SerializeField] private Button minigameButton;
    [SerializeField] private Image minigameBar;
    [SerializeField] private TextMeshProUGUI minigameCostText;
    [SerializeField] private GameObject minigameMaxLv;
    #endregion
    private Shop shop;
    private int wagonLv = 1;
    private int pickaxeLv = 1;
    private int freezeLv = 1;
    private int reputaLv = 1;
    private int minigameLv = 1;

    private void Start()
    {
        wagonSpaceButton.onClick.AddListener(() => UpgradeWagonSpace(shop.wagon[wagonLv - 1] as StorageUpgrade));
        pickaxeButton.onClick.AddListener(() => UpgradePickaxe(shop.pickAxe[pickaxeLv - 1] as PickaxeUpgrade));
        freezeReduceButton.onClick.AddListener(() => UpgradeFreezeReduce(shop.freezer[freezeLv - 1] as FreezerUpgrade));
        reputaButton.onClick.AddListener(() => UpgradeReputationGain(shop.reputation[reputaLv - 1] as ReputationUpgrade));
        minigameButton.onClick.AddListener(() => UpgradeMinigameDifficult());
        closeButton.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>().SetPlayerPause(false));

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    #region Upgrade
    public void UpgradeWagonSpace(StorageUpgrade upgrade)
    {
        if (IsPlayerHasEnoughCoin(upgrade))
        {
            PlayerResources.instance.coin -= upgrade.cost;
            upgrade.UpgradeEffect();
            StartCoroutine(ProgressBar(wagonSpaceBar, (25f * (wagonLv - 1)) / 100f, (25f * wagonLv / 100f)));
            wagonLv++;
            wagonSpaceButton.onClick.RemoveAllListeners();

            if (wagonLv <= shop.wagon.Count)
            {
                wagonSpaceButton.onClick.AddListener(() => UpgradeWagonSpace(shop.wagon[wagonLv - 1] as StorageUpgrade));
                wagonSpaceCostText.text = shop.wagon[wagonLv - 1].cost.ToString();
            }
            else
            {
                wagonSpaceCostText.gameObject.SetActive(false);
                wagonSpaceMaxLv.SetActive(true);
                wagonSpaceButton.interactable = false;
            }
        }
    }
    public void UpgradePickaxe(PickaxeUpgrade upgrade)
    {
        if (IsPlayerHasEnoughCoin(upgrade))
        {
            PlayerResources.instance.coin -= upgrade.cost;
            upgrade.UpgradeEffect();
            StartCoroutine(ProgressBar(pickaxeBar, (33.5f * (pickaxeLv - 1)) / 100f, (33.5f * pickaxeLv / 100f)));
            pickaxeLv++;
            pickaxeButton.onClick.RemoveAllListeners();

            if (pickaxeLv <= shop.pickAxe.Count)
            {
                pickaxeButton.onClick.AddListener(() => UpgradePickaxe(shop.pickAxe[pickaxeLv - 1] as PickaxeUpgrade));
                pickaxeCostText.text = shop.pickAxe[pickaxeLv - 1].cost.ToString();
            }
            else
            {
                pickaxeCostText.gameObject.SetActive(false);
                pickaxeMaxLv.SetActive(true);
                pickaxeButton.interactable = false;
            }
        }
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
    public void UpgradeReputationGain(ReputationUpgrade upgrade)
    {
        if (IsPlayerHasEnoughCoin(upgrade))
        {
            PlayerResources.instance.coin -= upgrade.cost;
            upgrade.UpgradeEffect();
            StartCoroutine(ProgressBar(reputaBar, (50f * (reputaLv - 1)) / 100f, (50f * reputaLv / 100f)));
            reputaLv++;
            reputaButton.onClick.RemoveAllListeners();

            if (reputaLv <= shop.reputation.Count)
            {
                reputaButton.onClick.AddListener(() => UpgradeReputationGain(shop.reputation[reputaLv - 1] as ReputationUpgrade));
                reputaCostText.text = shop.reputation[reputaLv - 1].cost.ToString();
            }
            else
            {
                reputaCostText.gameObject.SetActive(false);
                reputaMaxLv.SetActive(true);
                reputaButton.interactable = false;
            }
        }
    }
    public void UpgradeMinigameDifficult()
    {
        if (PlayerResources.instance.coin >= 500)
        {
            PlayerResources.instance.coin -= 500;
            //upgrade.UpgradeEffect();
            StartCoroutine(ProgressBar(minigameBar, 0.0f, 1.0f));
            minigameLv++;
            minigameButton.onClick.RemoveAllListeners();

            //minigameCostText.text = shop.minigame[minigameLv - 1].cost.ToString();
            FindAnyObjectByType<BaseQTEManager>().DecreseDifficulty();

            //MaxLv
            minigameCostText.gameObject.SetActive(false);
            minigameMaxLv.SetActive(true);
            minigameButton.interactable = false;

            if (minigameLv <= shop.minigame.Count)
            {
                
            }
            else
            {
                
            }
        }
    }

    #endregion

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
