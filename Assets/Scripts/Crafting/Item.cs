using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
enum ItemStatus
{
    WIP,
    Completed
}

public class Item : PickableObject, IBarSubject
{
    public ItemSO itemSo { get; private set; }
    [SerializeField] Transform spriteParent;
    [SerializeField] Collider2D physicalCollider;
    [SerializeField] ParticleSystem sparkle;
    ItemStatus status;
    float timeRequired;
    float timePassedInFreezer;
    bool isInFreezer = false;
    public bool highQuality = false;
    public bool perfectMinigame = false;
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
        timeRequired = itemSO.timeNeededtoFreeze - PlayerStats.instance.freezerWaitTimeReduction;
        if (timeRequired <= 0)
            timeRequired = 3;
        marketPrice = itemSO.marketPrice;
        if (highQuality)
        {
            marketPrice = itemSO.highQualityPrice;
        }
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
            spriteRenderer.sprite = itemSo.itemSprite;
            if (highQuality)
                sparkle.Play();
            status = ItemStatus.Completed;
            timePassedInFreezer = timeRequired;
        }
        onTargetValueChanged(timePassedInFreezer, timeRequired);
    }

    public override void StartHolding(Transform parent)
    {
        isInFreezer = false;
        spriteParent.transform.Rotate(new Vector3(0, 0, 90));
        physicalCollider.enabled = false;
        base.StartHolding(parent);
    }

    public override void StopHolding(Vector3 position)
    {
        spriteParent.transform.Rotate(new Vector3(0, 0, -90));
        Physics2D.queriesHitTriggers = true;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        Physics2D.queriesHitTriggers = false;
        physicalCollider.enabled = true;
        if (hit && hit.collider.CompareTag("Freezer"))
        {
            isInFreezer = true;
        }
        base.StopHolding(position);
    }
}
