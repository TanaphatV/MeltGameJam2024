using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
{
    [Header("Upgrades")]
    public List<ShopUpgrade> wagon;
    public List<ShopUpgrade> pickAxe;
    public List<ShopUpgrade> freezer;
    public List<ShopUpgrade> reputation;
    public List<ShopUpgrade> minigame;

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        StartCoroutine(InteractIE(playerInteract));
    }

    IEnumerator InteractIE(PlayerInteract playerInteract)
    {
        playerInteract.SetPlayerPause(true);
        yield return null;
    }
}