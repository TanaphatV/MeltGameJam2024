using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : MonoBehaviour, IHitable
{
    [SerializeField] MaterialContainer materialContainer;
    public int hitsNeeded;
    [SerializeField] MaterialDrop materialDropPrefab;
    public void Hit()
    {
        hitsNeeded -= PlayerStats.instance.mining;
        if (hitsNeeded <= 0)
            Break();
    }

    void Break()
    {
        for(int i = 0; i < materialContainer.amount; i++)
        {
            MaterialDrop temp = Instantiate(materialDropPrefab);
            temp.Init(materialContainer.material);
        }
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
