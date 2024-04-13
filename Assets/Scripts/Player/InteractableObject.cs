using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController playerController)
    {
        InteractBehavior(playerController);
    }

    protected virtual void InteractBehavior(PlayerController playerController)
    {
        Debug.Log("Interact with " + gameObject.name);
    }
}
