using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : MonoBehaviour, IHitable
{
    public MaterialSO materialSO;
    public int hitsNeeded;

    public void Hit()
    {
        hitsNeeded -= PlayerStatsSingleton.instance.mining;
        if (hitsNeeded <= 0)
            Break();
    }

    void Break()
    {
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
