using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingStation : InteractableObject
{
    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        StartCoroutine(InteractRoutine(playerInteract));
    }

    IEnumerator InteractRoutine(PlayerInteract playerInteract)
    {
        playerInteract.pause = true;
        yield return null;
        playerInteract.pause = false;
    }
}
