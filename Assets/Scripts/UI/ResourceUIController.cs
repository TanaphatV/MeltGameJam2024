using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIController : MonoBehaviour
{
    PlayerResources playerResources;
    private Dictionary<string, int> materialDictionary;
    // Start is called before the first frame update
    void Awake()
    {
        materialDictionary = playerResources.GetMaterialDictionary();
        playerResources.onMaterialAmountChange += UpdateMaterial;
    }

    void UpdateMaterial(string matName,int amount)
    {
        materialDictionary[matName] = amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
