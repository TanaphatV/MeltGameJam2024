using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOrderGuy : InteractableObject
{

    public List<ItemSO> specialOrders = new List<ItemSO>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                PlayerResources.instance.coin += (int)((temp.itemSo.marketPrice * 1.5f * ReputationManager.instance.specialOrderMultiplier));
            }
        }
        else
        {
            //display UI
        }
    }
}
