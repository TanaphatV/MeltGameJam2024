using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
{
    public List<ShopUpgrade> wagon;
    public List<ShopUpgrade> pickAxe;
    public List<ShopUpgrade> freezer;
    public List<ShopUpgrade> reputation;
    public List<ShopUpgrade> minigame;;

    protected override void InteractBehavior(PlayerInteract playerInteract)
    {
        
    }
}

public class Upgrade
{
    public int level;
    public string description;
    public int cost;

}
