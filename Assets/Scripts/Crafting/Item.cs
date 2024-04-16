using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum ItemStatus
{
    WIP,
    Completed
}

public class Item : PickableObject, IBarSubject
{
    public ItemSO itemSo { get; private set; }
    ItemStatus status;
    float timeRequired;
    float timePassedInFreezer;
    bool isInFreezer = false;
    public bool highQuality = false;
    public int price;
    public int reputationReward;
    public int marketPrice { get; private set; }

    public UnityAction<float, float> onTargetValueChanged { get; set; }

    private void Update()
    {
        if (isInFreezer)
            IncrementProgress();
    }
    public void Init(ItemSO itemSO)
    {
        this.itemSo = itemSO;
        spriteRenderer.sprite = itemSO.wipItem;
        timeRequired = itemSO.timeNeededtoFreeze - PlayerStats.instance.freezerWaitTimeReduction;
        if (timeRequired <= 0)
            timeRequired = 3;
        marketPrice = itemSO.marketPrice;
        if (highQuality)
            marketPrice = itemSO.highQualityPrice;
        status = ItemStatus.WIP;
    }

    public void IncrementProgress()
    {
        if (status == ItemStatus.Completed)
            return;
        if(!TimeManager.instance.pause)
            timePassedInFreezer += Time.deltaTime;
        if (timePassedInFreezer >= timeRequired)
        {
            spriteRenderer.sprite = itemSo.finishedItem;
            status = ItemStatus.Completed;
            timePassedInFreezer = timeRequired;
        }
        onTargetValueChanged(timePassedInFreezer, timeRequired);
    }

    public override void StartHolding(Transform parent)
    {
        isInFreezer = false;
        base.StartHolding(parent);
    }

    public override void StopHolding(Vector3 position)
    {
        Physics2D.queriesHitTriggers = true;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        Physics2D.queriesHitTriggers = false; 
        if (hit && hit.collider.CompareTag("Freezer"))
        {
            isInFreezer = true;
        }
        base.StopHolding(position);
    }
}
