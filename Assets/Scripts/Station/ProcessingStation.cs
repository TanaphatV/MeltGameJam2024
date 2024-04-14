using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftingStatus
{
    Nothing,
    Completed,
    Failed
}

public class ProcessingStation : InteractableObject
{
    public Item itemBasePrefab;
    [SerializeField] protected ItemSO testSO;
    public CraftingStatus currentItemCraftingStatus = CraftingStatus.Nothing;
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        CreateItem(testSO);
        //StartCoroutine(InteractRoutine(playerInteract));
    }

    void CreateItem(ItemSO itemSO)
    {
        Item temp = Instantiate(itemBasePrefab);
        temp.transform.position = transform.position;
        temp.Init(itemSO);
    }

    IEnumerator InteractRoutine(PlayerInteract playerInteract)
    {
        playerInteract.pause = true;
        yield return null;
        //if minigame completed properly, CreateItem
        playerInteract.pause = false;
        currentItemCraftingStatus = CraftingStatus.Nothing;
    }
}
