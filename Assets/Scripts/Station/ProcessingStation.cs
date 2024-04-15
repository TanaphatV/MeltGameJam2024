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
        
        yield return new WaitForEndOfFrame();
        recipePanel.OpenPanel(this);
        //yield return 
        //if minigame completed properly, CreateItem

        yield return new WaitForSeconds(1.0f);
        if (currentItemCraftingStatus == CraftingStatus.Completed)
        {
            CreateItem(testSO, true);
        }
        //playerInteract.pause = false;
        currentItemCraftingStatus = CraftingStatus.Nothing;
    }
}
