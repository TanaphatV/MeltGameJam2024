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
            //if(item.price > item.itemSo.marketPrice)
            //{

            //}
            revenue += item.price;
        }
        items = new List<Item>();
        PlayerResources.instance.coin += revenue;
    }
}
