using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUpgrade : ShopUpgrade
{
    public int storageIncrease;
    public override void UpgradeEffect()
    {
        PlayerStats.instance.wagonStorage += storageIncrease;
        used = true;
    }
}
