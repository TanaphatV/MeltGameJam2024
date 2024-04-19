using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wagon : InteractableObject
{
    [SerializeField] private WagonPriceDealHUDManager hud;
    [SerializeField] private List<GameObject> wagonLevels;
    List<ItemInfo> items = new List<ItemInfo>();
    [SerializeField] private TextMeshProUGUI demandItemText;
    public ItemSO itemInDemand { get; private set; }
    private void Start()
    {
        TimeManager.instance.onDayEnd += SellItem;
        TimeManager.instance.onDayEnd += RefreshWagon;
        RefreshWagon();
        UpdateWagonFillLevelSprite();
    }

    void RefreshWagon()
    {
        List<ItemSO> itemList = RecipeSingletonManager.Instance.GetResource.craftableItems;
        itemInDemand = itemList[Random.Range(0, itemList.Count)];
        demandItemText.text = "Today <b>" + itemInDemand.itemName + "</b> are in high demand!";
        UpdateWagonFillLevelSprite();
    }

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        if(playerInteract.pickedObject != null)
        {
            if (playerInteract.pickedObject.GetType() != typeof(Item))
            {
                Debug.Log("Not item");
                return;
            }
            else if (playerInteract.GetHoldingItem().status == ItemStatus.WIP)
            {
                Debug.Log("Item Not Frozen yet!");
                return;
            }
            else if(items.Count < PlayerStats.instance.wagonStorage)
            {

                StartCoroutine(MoveItemToWagon(playerInteract));
            }
        }
        
    }

    public IEnumerator MoveItemToWagon(PlayerInteract playerInteract)
    {
        Item item = playerInteract.TakeItem();
        ItemInfo itemInfo = new ItemInfo(item.itemSo, item.price, item.reputationReward);
        hud.OpenPanel();
        yield return hud.InitPanel(item, this, itemInfo);

        Debug.Log(itemInfo.price);
        items.Add(itemInfo);
        UpdateWagonFillLevelSprite();
        item.Destroy();
    }

    int currentWagonFillLevel = 0;
    void UpdateWagonFillLevelSprite()
    {
        float fill = (float)items.Count / (float)PlayerStats.instance.wagonStorage;
        Debug.Log(fill);
        int previousFillLevel = currentWagonFillLevel;
        currentWagonFillLevel = 0;
        for (int i = 1; i < 3; i++)
        {
            if (fill > (i) * 0.33f)
                currentWagonFillLevel = i;
            else
                break;
        }
        if(previousFillLevel != currentWagonFillLevel)
        {
            wagonLevels[previousFillLevel].SetActive(false);
            wagonLevels[currentWagonFillLevel].SetActive(true);
        }
    }

    public string GetSellChanceMessage(ItemInfo item)
    {
        float chance = GetSellProbability(item);

        if (chance < 0.1f)
            return "PEOPLE WILL NOT BUY THIS";
        else if (chance < 0.3f)
            return "I REALLY don't like the chance...";
        else if (chance < 0.6f)
            return "This might not sell...";
        else if (chance < 1.0f)
            return "This will likely sell!";
        else 
            return "This WILL sell!";

    }

    public class ItemInfo
    {
        public ItemSO itemSo;
        public int price;
        public int reputationReward;
        public ItemInfo(ItemSO itemSo,int price,int reputationReward)
        {
            this.itemSo = itemSo;
            this.price = price;
            this.reputationReward = reputationReward;
        }
    }

    void SellItem()
    {
        int revenue = 0;
        foreach(var item in items)
        {
            if(Random.Range(0.0f,1.0f) <= GetSellProbability(item))
            {
                revenue += item.price;
                ReputationManager.instance.IncreaseReputation(item.reputationReward);
            }
        }
        items = new List<ItemInfo>();
        PlayerResources.instance.coin += revenue;
    }

    float GetSellProbability(ItemInfo item)
    {
        float priceRatio = (float)item.price / (float)item.itemSo.marketPrice;
        float probability = 1;
        for(int i = 0; i < 9; i++)
        {
            if (priceRatio > 0.6f + ((float)i * 0.2f))
            {
                probability -= 0.2f;
            }
            else 
                break;
        }
        if (item.itemSo == itemInDemand)
            probability += 0.2f; 
        probability += ReputationManager.instance.bonus.wagonBuyProbabilityIncrease;
        return probability;
        
    }
}
