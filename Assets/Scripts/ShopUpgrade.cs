using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopUpgrade : MonoBehaviour
{
    public int level;
    public string description;
    public int cost;
    public bool used = false;
    public abstract void UpgradeEffect();
}
