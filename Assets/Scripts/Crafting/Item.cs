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
    ItemStatus status;
    float timeRequired;
    float timePassedInFreezer;
    Sprite completedSprite;
    bool isInFreezer = false;
    public bool highQuality = false;

    public UnityAction<float, float> onTargetValueChanged { get; set; }

    private void Update()
    {
        if (isInFreezer)
            IncrementProgress();
    }
    public void Init(ItemSO itemSO)
    {
        spriteRenderer.sprite = itemSO.wipItem;
        timeRequired = itemSO.timeNeededtoFreeze - PlayerStatsSingleton.instance.freezerWaitTimeReduction;
        status = ItemStatus.WIP;
        completedSprite = itemSO.finishedItem;
    }

    public void IncrementProgress()
    {
        if (status == ItemStatus.Completed)
            return;
        timePassedInFreezer += Time.deltaTime;
        if (timePassedInFreezer >= timeRequired)
        {
            spriteRenderer.sprite = completedSprite;
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
