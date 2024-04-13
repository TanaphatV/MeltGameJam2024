using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    private float interactionRange = 2f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouse2D = new Vector3(mousePos.x, mousePos.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(mouse2D, Vector2.zero);
        Debug.Log("MOuse pos " + mousePos);
        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(1,0), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Hit");
            Debug.Log(hit.collider.gameObject.name);
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 e = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    //    Gizmos.DrawLine(e, (Camera.main.transform.right * 2) + e);

    //    Gizmos.DrawSphere(e, 2.0f);
    //}
}
