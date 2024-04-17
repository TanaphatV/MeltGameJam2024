using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : InteractableObject
{
    [SerializeField] private WagonPriceDealHUDManager hud;
    List<Item> items = new List<Item>();
    public ItemSO itemInDemand { get; private set; }
    private void Start()
    {
        TimeManager.instance.onDayEnd += SellItem;
    }

    void RefreshWagon()
    {
        List<ItemSO> itemList = RecipeSingletonManager.Instance.GetResource.craftableItems;
        itemInDemand = itemList[Random.Range(0, itemList.Count)];
    }

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        if(playerInteract.pickedObject is not Item)
        {
            Debug.Log("Not item");
            return;
        }
        else
        {
            Item item = playerInteract.TakeItem();
            hud.OpenPanel();
            //do something to set price
        }
    }

    public string GetSellChanceMessage(Item item)
    {
        float chance = GetSellProbability(item);
        if (chance >= 1.0f)
        {
            return "This WILL sell!";
        }
        else if (chance >= 60)
        {
            return "This will probably sell!";
        }
        else if (chance >= 30)
        {
            return "This might not sell...";
        }
        else if (chance >= 1)
        {
            return "I REALLY don't like the chance...";
        }
        else
            return "PEOPLE WILL NOT BUY THIS";

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
        items = new List<Item>();
        PlayerResources.instance.coin += revenue;
    }

    float GetSellProbability(Item item)
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
