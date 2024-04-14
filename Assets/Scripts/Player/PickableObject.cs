using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider2D col;

    private void Start()
    {

    }

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        Debug.Log("Interact with " + gameObject.name);
        playerInteract.PickUpObject(this);
    }

    public virtual void StartHolding(Transform parent)
    {
        col.enabled = false;
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0, 0, transform.position.z);
        SetSortingLayorHolding();
    }
    public virtual void StopHolding(Vector3 putDownPos)
    {
        col.enabled = true;
        transform.SetParent(null);
        transform.position = putDownPos;
        SetSortingLayorNormal();
    }

    public void SetSortingLayorHolding()
    {
        spriteRenderer.sortingLayerName = "PickedObject";
    }

    public void SetSortingLayorNormal()
    {
        spriteRenderer.sortingLayerName = "Midground";
    }
}
