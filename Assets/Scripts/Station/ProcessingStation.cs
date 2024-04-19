using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftingStatus
{
    Nothing,
    Completed,
    Failed
}
public enum MinigameResult
{
    Nothing,
    Fail,
    Good,
    Perfect
}

public class ProcessingStation : InteractableObject
{
    public Item itemBasePrefab;
    protected ItemSO itemToCreate;
    bool isHigh = false;
    [SerializeField] Transform output;
    private RecipeListManagerGUI recipePanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject qtePanel;

    //private BaseQTEManager moldUI;
    public CraftingStatus currentItemCraftingStatus = CraftingStatus.Nothing;
    public MinigameResult currentMinigameResult = MinigameResult.Nothing;

    private void Start()
    {
        itemToCreate = null;
        recipePanel = FindObjectOfType<RecipeListManagerGUI>();
        recipePanel.onSelectedItemToCreate += SetItemToCreate;
        //recipePanel.onSelectedQuality+=
    }
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        AudioManager.Instance.PlaySFX("Pickaxe");
        StartCoroutine(InteractRoutine(playerInteract));
    }

    public void CreateItem(ItemSO itemSO , bool highQuality, bool perfectMinigame)
    {
        Item temp = Instantiate(itemBasePrefab);
        temp.perfectMinigame = perfectMinigame;
        temp.highQuality = highQuality;
        temp.reputationReward = ReputationRewardCalculation(itemSO, highQuality, perfectMinigame);
        temp.transform.position = output.position;
        temp.Init(itemSO);
    }

    int ReputationRewardCalculation(ItemSO itemSO,bool highQuality,bool perfectMinigame)
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
        if (perfectMinigame)
            reward = (int)((float)reward * 1.2f);

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
        ResetQTEStatus();

        yield return new WaitForEndOfFrame();
        recipePanel.OpenPanel(this);

        while (currentItemCraftingStatus == CraftingStatus.Nothing && currentMinigameResult == MinigameResult.Nothing)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Result: " + currentItemCraftingStatus + " with " + currentMinigameResult + " grade");
        if (currentItemCraftingStatus == CraftingStatus.Completed)
        {
            Debug.Log(itemToCreate);
            switch (currentMinigameResult)
            {
                case MinigameResult.Fail:
                    break;
                case MinigameResult.Good:
                    CreateItem(itemToCreate, isHigh, false);
                    break;
                case MinigameResult.Perfect:
                    CreateItem(itemToCreate, isHigh, true);
                    break;
            }
        }
        else
        {
            Debug.Log("Fail to create an item");
            failPanel.SetActive(true);
            qtePanel.SetActive(false);
            //Do some ui popup;
        }
        AudioManager.Instance.StartBlendBGM(AudioManager.Instance.currentAudioPlay, AudioManager.Instance.currentReputationBGM);
        playerInteract.SetPlayerPause(false);
        ResetQTEStatus();
    }

    private void ResetQTEStatus()
    {
        currentItemCraftingStatus = CraftingStatus.Nothing;
        currentMinigameResult = MinigameResult.Nothing;
    }
}
