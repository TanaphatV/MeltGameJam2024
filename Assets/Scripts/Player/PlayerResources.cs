using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] ResourceSO resourceSO;
    private Dictionary<string, int> materialDictionary;

    private void Awake()
    {
        materialDictionary = new Dictionary<string, int>();
        foreach (var mat in resourceSO.pickableOres)
        {
            materialDictionary.Add(mat.materialName, 0);
        }
    }

    public bool HaveEnoughMaterial(string materialName, int amount)
    {
        if (materialDictionary[materialName] >= amount)
            return true;
        return false;
    }

    public void TakeMaterial(string materialName, int amount)
    {
        materialDictionary[materialName] -= amount;
        if(materialDictionary[materialName] < 0)
        {
            throw new Exception("ERROR: took more material than material amount"); 
        }
    }

    public void AddMaterial(string materialName, int amount)
    {
        materialDictionary[materialName] += amount;
    }
}
