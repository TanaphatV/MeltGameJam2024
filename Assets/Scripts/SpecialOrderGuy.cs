using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOrderGuy : InteractableObject
{

    public List<ItemSO> specialOrders = new List<ItemSO>();
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.instance.onDayEnd += RefreshOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshOrder()
    {
        specialOrders = new List<ItemSO>();
        var itemList = RecipeSingletonManager.Instance.GetResource.craftableItems;
        for (int i = 0; i < ReputationManager.instance.currentRank + 1; i++)
        {
            specialOrders.Add(itemList[Random.Range(0, itemList.Count)]);
        }
    }

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        Item temp = playerInteract.GetHoldingItem();
        if (temp != null)
        {
            if (specialOrders.Contains(temp.itemSo)) 
            {
                specialOrders.Remove(temp.itemSo);
                playerInteract.TakeItem();
                int payout = (int)((temp.marketPrice * 1.5f * ReputationManager.instance.bonus.specialOrderPayMultiplier));
                PlayerResources.instance.coin += payout;
                if(temp.highQuality)
                    PlayerResources.instance.coin += payout;
                ReputationManager.instance.IncreaseReputation(temp.reputationReward);
            }
        }
        else
        {
            //display UI
        }
    }
}
