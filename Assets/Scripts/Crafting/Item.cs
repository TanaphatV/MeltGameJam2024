using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemStatus
{
    WIP,
    Completed
}

public class Item : PickableObject
{
    ItemStatus status;
    float timeRequired;
    float timePassedInFreezer;
    Sprite completedSprite;

    public void Init(ItemSO itemSO)
    {
        spriteRenderer.sprite = itemSO.wipItem;
        timeRequired = itemSO.timeNeededtoFreeze;
        status = ItemStatus.WIP;
        completedSprite = itemSO.finishedItem;
    }

    public void IncrementProgress()
    {
        timePassedInFreezer += Time.deltaTime;
        if(timePassedInFreezer >= timeRequired)
        {
            spriteRenderer.sprite = completedSprite;
            status = ItemStatus.Completed;
        }
    }
}
