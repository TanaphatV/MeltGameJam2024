using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[System.Serializable]
public class SpecialOrder
{
    public ItemSO itemSO = null;
    public bool highQuality = false;
    public bool completed = false;
    public int payout = 0;
}

public class SpecialOrderGuy : InteractableObject
{
    private List<SpecialOrder> specialOrders = new List<SpecialOrder>();

    OrderListManagerGUI orderListManagerGUI;
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.instance.onDayEnd += RefreshOrder;
        orderListManagerGUI = FindObjectOfType<OrderListManagerGUI>();
        RefreshOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshOrder()
    {
        specialOrders = new List<SpecialOrder>();
        var itemList = RecipeSingletonManager.Instance.GetResource.craftableItems;
        for (int i = 0; i < ReputationManager.instance.currentRank + 1; i++)
        {
            if (i > 2)
                break;
            SpecialOrder temp = new SpecialOrder();
            ItemSO itemSO = itemList[UnityEngine.Random.Range(0, itemList.Count)];
            temp.completed = false;
            temp.itemSO = itemSO;
            temp.highQuality = Convert.ToBoolean( UnityEngine.Random.Range(0, 2));
            temp.payout = (int)((itemSO.marketPrice * 1.5f * ReputationManager.instance.bonus.specialOrderPayMultiplier));

            specialOrders.Add(temp);
        }
        orderListManagerGUI.InitPanel(specialOrders);
    }

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        Item temp = playerInteract.GetHoldingItem();
        if (temp != null)
        {
            SpecialOrder order = specialOrders.Find((specialOrder) => {
                return ((specialOrder.itemSO == temp.itemSo) && (temp.highQuality == specialOrder.highQuality) && !specialOrder.completed);
            });
            if (order != null) 
            {
                order.completed = true;
                playerInteract.TakeItem();
                PlayerResources.instance.coin += order.payout;
                ReputationManager.instance.IncreaseReputation(temp.reputationReward);
            }
        }
        StartCoroutine(InteractIE(playerInteract));
    }

    IEnumerator InteractIE(PlayerInteract playerInteract)
    {
        playerInteract.SetPlayerPause(true);
        orderListManagerGUI.InitPanel(specialOrders);
        orderListManagerGUI.OpenPanel();
        yield return new WaitUntil(() => { return Input.GetKeyDown(KeyCode.Escape); });
        playerInteract.SetPlayerPause(false);
        orderListManagerGUI.ClosePanel();
    }
}
