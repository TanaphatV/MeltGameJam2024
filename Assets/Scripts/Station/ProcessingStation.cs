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
    [SerializeField] private RecipeListManagerGUI recipePanel;
    //private BaseQTEManager moldUI;
    public CraftingStatus currentItemCraftingStatus = CraftingStatus.Nothing;
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        StartCoroutine(InteractRoutine(playerInteract));
    }

    public void CreateItem(ItemSO itemSO , bool highQuality)
    {
        Item temp = Instantiate(itemBasePrefab);
        temp.highQuality = highQuality;
        temp.transform.position = transform.position;
        temp.Init(itemSO);
    }

    IEnumerator InteractRoutine(PlayerInteract playerInteract)
    {
        playerInteract.pause = true;
        currentItemCraftingStatus = CraftingStatus.Nothing;

        yield return new WaitForEndOfFrame();
        recipePanel.OpenPanel(this);

        //if minigame completed properly, CreateItem
        while (currentItemCraftingStatus == CraftingStatus.Nothing)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(currentItemCraftingStatus);
        if (currentItemCraftingStatus == CraftingStatus.Completed)
        {
            CreateItem(testSO, true);
        }
        //playerInteract.pause = false;
        currentItemCraftingStatus = CraftingStatus.Nothing;
    }
}
