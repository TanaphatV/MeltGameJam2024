using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteract playerInteract);
}

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float putdownRange = 1f;
    [SerializeField] Transform objectLiftingTransform;
    [SerializeField] PlayerController playerController;
    public PickableObject pickedObject { get; private set; }


    public void InteractAction()
    {
        if (InteractWithObject())
        {

        }
        else if (pickedObject)
            PutDownObject();
    }

    private void Update()
    {
        if (!playerController.isFacingRight)
            objectLiftingTransform.localScale = new Vector3(1, 1, 1);
        else
            objectLiftingTransform.localScale = new Vector3(-1, 1, 1);
    }


    public void PickUpObject(PickableObject po)
    {
        if (pickedObject)
            return;
        pickedObject = po;
        pickedObject.StartHolding(objectLiftingTransform);
        
    }
    void PutDownObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = new Vector3(mousePos.x,mousePos.y,transform.position.z) - transform.position;
        Vector3 direction = difference.normalized;
        Vector3 position = transform.position + (direction * putdownRange);

        pickedObject.StopHolding(position);

        pickedObject = null;
    }

    public Item TakeItem()
    {
        Item item = GetHoldingItem();
        if (item != null)
        {
            pickedObject = null;
            return item;
        }
        else
            return null;
    }

    public Item GetHoldingItem()
    {
        if (pickedObject is Item)
        {
            Item temp = (Item)pickedObject;
            return temp;
        }
        else
            return null;
    }

    public void SetPlayerPause(bool pause)
    {
        playerController.pause = pause;
    }

    bool InteractWithObject()
    {
        Physics2D.queriesHitTriggers = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        Physics2D.queriesHitTriggers = false;
        if (hit.collider == null)
            return false;

        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        if(interactable == null)
        {
            if (hit.collider.transform.parent != null)
            {
                interactable = hit.collider.transform.GetComponentInParent<IInteractable>();
            }
        }

        if(interactable != null)
        {
            if (Vector2.Distance(hit.collider.ClosestPoint(transform.position), transform.position) > interactionRange)
            {
                //tooltip???
                return false;
            }
            interactable.Interact(this);
            return true;
        }
       

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
