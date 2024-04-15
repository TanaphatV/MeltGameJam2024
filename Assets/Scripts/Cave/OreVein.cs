using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : MonoBehaviour, IHitable
{
    public MaterialContainer materialContainer;
    public int hitsNeeded;

    public void Hit()
    {
        hitsNeeded -= PlayerStats.instance.mining;
        if (hitsNeeded <= 0)
            Break();
    }

    void Break()
    {
        PlayerResources.instance.AddMaterial(materialContainer.material, materialContainer.amount);
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
