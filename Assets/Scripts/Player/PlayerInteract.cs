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
        if (pickedObject is Item)
        {
            Item temp = (Item)pickedObject;
            pickedObject = null;
            return temp;
        }
        else
            return null;
    }

    public Item GetHoldingItem()
    {
        if (pickedObject == null)
            return null;
        if (pickedObject is not Item)
            return null; 
        
        Item temp = (Item)pickedObject;
        pickedObject = null;
        return temp;
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

        if (hit.collider.TryGetComponent(out IInteractable interactable))
        {
            if (Vector2.Distance(hit.collider.transform.position, transform.position) > interactionRange)
            {
                //tooltip???
                return false;
            }
            interactable.Interact(this);
            return true;
        }
        else if(hit.collider.transform.parent != null)
        {
            if(hit.collider.transform.gameObject.TryGetComponent(out IInteractable interact))
            {
                if (Vector2.Distance(hit.collider.transform.position, transform.position) > interactionRange)
                {
                    //tooltip???
                    return false;
                }
                interact.Interact(this);
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
