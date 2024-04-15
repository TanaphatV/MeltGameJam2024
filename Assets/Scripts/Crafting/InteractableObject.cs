using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteract playerInteract)
    {
        InteractBehavior(playerInteract);
    }

    protected virtual void InteractBehavior(PlayerInteract playerInteract)
    {
        Debug.Log("Interact with " + gameObject.name);
    }
}
