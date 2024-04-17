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
    protected ItemSO itemToCreate;
    bool isHigh = false;
    [SerializeField] private RecipeListManagerGUI recipePanel;

    //private BaseQTEManager moldUI;
    public CraftingStatus currentItemCraftingStatus = CraftingStatus.Nothing;

    private void Start()
    {
        itemToCreate = null;
        recipePanel.onSelectedItemToCreate += SetItemToCreate;
        //recipePanel.onSelectedQuality+=
    }
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        StartCoroutine(InteractRoutine(playerInteract));
    }

    public void CreateItem(ItemSO itemSO , bool highQuality)
    {
        Item temp = Instantiate(itemBasePrefab);
        temp.highQuality = highQuality;
        temp.reputationReward = ReputationRewardCalculation(itemSO, highQuality);
        temp.transform.position = transform.position;
        temp.Init(itemSO);
    }

    int ReputationRewardCalculation(ItemSO itemSO,bool highQuality)
    {
        int reward = 0;
        if(highQuality)
        {
            foreach (var mat in itemSO.highQualityRecipe)
                reward += mat.material.reputaionReward * mat.amount;
        }
        else
        {
            foreach (var mat in itemSO.normalQualityRecipe)
                reward += mat.material.reputaionReward * mat.amount;
        }

        return reward;
        
    }

    private void SetItemToCreate(ItemSO itemSo, bool isNormal)
    {
        //Debug.Log("SetItemToCreate");
        itemToCreate = itemSo;
        isHigh = !isNormal;
    }

    IEnumerator InteractRoutine(PlayerInteract playerInteract)
    {
        if(playerInteract == null)
        {
            Debug.LogError("No PlayerInteract has been assigned!");
        }
        playerInteract.SetPlayerPause(true);
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
            Debug.Log(itemToCreate);
            CreateItem(itemToCreate, isHigh);
        }
        playerInteract.SetPlayerPause(false);
        currentItemCraftingStatus = CraftingStatus.Nothing;
    }
}
