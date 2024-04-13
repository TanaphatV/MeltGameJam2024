using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    private Camera mainCamera;
    private float interactionRange = 2f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.transform.position, mainCamera.transform.forward, interactionRange);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 e = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        //Gizmos.DrawLine(e, (Camera.main.transform.forward * 2) + e);

        Gizmos.DrawSphere(e, 2.0f);
    }
}
