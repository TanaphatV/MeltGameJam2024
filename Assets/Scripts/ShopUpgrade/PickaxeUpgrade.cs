using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeUpgrade : ShopUpgrade
{
    public int damageIncrease;
    public int miningIncrease;

    public override void UpgradeEffect()
    {
        PlayerStats.instance.damage += damageIncrease;
        PlayerStats.instance.mining += miningIncrease;
        used = true;
    }
}
