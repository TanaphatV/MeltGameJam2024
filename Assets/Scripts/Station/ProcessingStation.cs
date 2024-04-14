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
    [SerializeField] private BaseQTEManager uiPrefabs;
    private BaseQTEManager moldUI;
    public CraftingStatus currentItemCraftingStatus = CraftingStatus.Nothing;
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        StartCoroutine(InteractRoutine(playerInteract));
    }

    void CreateItem(ItemSO itemSO , bool highQuality)
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
        if (moldUI == null)
        {
            moldUI = Instantiate(uiPrefabs, uiPrefabs.transform);
            int newMinimum = Random.Range(60, 85);
            int newMax = newMinimum + Random.Range(5, 10);
            yield return moldUI.Init(newMinimum, newMax, this);
        }
        //if minigame completed properly, CreateItem
        yield return new WaitForSeconds(1.0f);
        if (currentItemCraftingStatus == CraftingStatus.Completed)
        {
            CreateItem(testSO);
            Destroy(moldUI.gameObject);
        }
        else
        {
            Destroy(moldUI.gameObject);
        }
        playerInteract.pause = false;
        currentItemCraftingStatus = CraftingStatus.Nothing;
    }
}
