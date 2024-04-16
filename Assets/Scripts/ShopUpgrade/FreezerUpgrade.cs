using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerUpgrade : ShopUpgrade
{
    public float waitTimeReduction;
    public override void UpgradeEffect()
    {
        PlayerStats.instance.freezerWaitTimeReduction += waitTimeReduction;
    }
}
