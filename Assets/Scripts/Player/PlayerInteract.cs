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
    public PickableObject pickedObject { get; private set; }

    public bool pause;

    //PlayerController playerController;
    void Start()
    {
        //playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (pause)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(InteractWithObject())
            {

            }
            else if(pickedObject)
                PutDownObject();
        }
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

    bool InteractWithObject()
    {
        Physics2D.queriesHitTriggers = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

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

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

}
