using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : InteractableObject
{
    List<Item> items = new List<Item>();
    private void Start()
    {
        TimeManager.instance.onDayEnd += SellItem;
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
            //do something to set price
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
        probability += Random.Range(0.0f, 0.2f);
        probability += ReputationManager.instance.bonus.wagonBuyProbabilityIncrease;
        return probability;
    }
}
