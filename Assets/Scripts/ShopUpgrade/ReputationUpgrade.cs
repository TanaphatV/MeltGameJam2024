using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationUpgrade : ShopUpgrade
{
    public float reputationGainModifier;
    public override void UpgradeEffect()
    {
        PlayerStats.instance.reputationGainModifier = reputationGainModifier;
    }

}
