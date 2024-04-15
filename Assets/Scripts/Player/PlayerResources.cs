using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class PlayerResources : MonoBehaviour
{
    #region singleton
    private static PlayerResources _instance;
    public static PlayerResources instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Error, instance is null");
            }
            return _instance;
        }
    }
    #endregion
    [SerializeField] ResourceSO resourceSO;
    private Dictionary<string, int> materialDictionary;

    public int coin;
    public int reputation;

    public UnityAction<string, int> onMaterialAmountChange;

    private void Awake()
    {
        _instance = this;
        materialDictionary = new Dictionary<string, int>();
        foreach (var mat in resourceSO.pickableOres)
        {
            materialDictionary.Add(mat.materialName, 0);
        }
    }

    public Dictionary<string, int> GetMaterialDictionary()
    {
        return new Dictionary<string, int>(materialDictionary);
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
        onMaterialAmountChange(materialName, materialDictionary[materialName]);
        if (materialDictionary[materialName] < 0)
        {
            throw new Exception("ERROR: took more material than material amount"); 
        }
    }

    public void AddMaterial(string materialName, int amount)
    {
        materialDictionary[materialName] += amount;
        onMaterialAmountChange(materialName, materialDictionary[materialName]);
    }
}
