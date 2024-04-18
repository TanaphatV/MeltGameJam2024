using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : InteractableObject
{
    [SerializeField] private WagonPriceDealHUDManager hud;
    List<ItemInfo> items = new List<ItemInfo>();
    public ItemSO itemInDemand { get; private set; }
    private void Start()
    {
        TimeManager.instance.onDayEnd += SellItem;
        TimeManager.instance.onDayEnd += RefreshWagon;
        RefreshWagon();
    }

    void RefreshWagon()
    {
        List<ItemSO> itemList = RecipeSingletonManager.Instance.GetResource.craftableItems;
        itemInDemand = itemList[Random.Range(0, itemList.Count)];
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
            else if(items.Count < PlayerStats.instance.wagonStorage)
            {
                Item item = playerInteract.TakeItem();
                ItemInfo itemInfo = new ItemInfo(item.itemSo, item.price, item.reputationReward);
                hud.OpenPanel();
                hud.InitPanel(item, this, itemInfo);
                items.Add(itemInfo);
                Destroy(item.gameObject);
            }
        }
        
    }

    public string GetSellChanceMessage(ItemInfo item)
    {
        float chance = GetSellProbability(item);

        if (chance < 1)
            return "PEOPLE WILL NOT BUY THIS";
        else if (chance < 30)
            return "I REALLY don't like the chance...";
        else if (chance < 60)
            return "This might not sell...";
        else if (chance < 1)
            return "This will probably sell!";
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
