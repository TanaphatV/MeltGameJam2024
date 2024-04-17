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
    private Dictionary<MaterialSO, int> materialDictionary;

    public int hp;
    public int coin;

    public UnityAction<MaterialSO, int> onMaterialAmountChange;

    private void Awake()
    {
        _instance = this;
        materialDictionary = new Dictionary<MaterialSO, int>();
        foreach (var mat in resourceSO.pickableOres)
        {
            materialDictionary.Add(mat, 0);
        }
    }
    private void Start()
    {
        hp = PlayerStats.instance.maxHp;
        TimeManager.instance.onDayEnd += ClearMaterial;
    }

    void ClearMaterial()
    {
        foreach(var mat in materialDictionary.Keys)
        {
            materialDictionary[mat] = 0;
        }
    }

    public Dictionary<MaterialSO, int> GetMaterialDictionary()
    {
        return new Dictionary<MaterialSO, int>(materialDictionary);
    }

    public bool HaveEnoughMaterial(MaterialSO material, int amount)
    {
        if (materialDictionary[material] >= amount)
            return true;
        return false;
    }
    public bool HaveEnoughMaterial(MaterialContainer materialContainer)
    {
        if (materialDictionary[materialContainer.material] >= materialContainer.amount)
            return true;
        return false;
    }

    public bool HaveEnoughMulitpleMaterial(List<MaterialContainer> materialContainers)
    {
        foreach (var materialContainer in materialContainers)
        {
            if (!HaveEnoughMaterial(materialContainer))
                return false;
        }
        return true;
    }



    public void TakeMaterial(MaterialSO material, int amount)
    {
        materialDictionary[material] -= amount;
        OnMaterialAmountChange(material, amount);
        if (materialDictionary[material] < 0)
        {
            throw new Exception("ERROR: took more material than material amount"); 
        }
    }

    public void TakeMaterial(MaterialContainer materialContainer)
    {
        materialDictionary[materialContainer.material] -= materialContainer.amount;
        OnMaterialAmountChange(materialContainer.material, materialContainer.amount);
        if (materialDictionary[materialContainer.material] < 0)
        {
            throw new Exception("ERROR: took more material than material amount");
        }
    }

    public bool TakeMulitpleMaterial(List<MaterialContainer> materialContainers)
    {
        foreach (var materialContainer in materialContainers)
        {
            TakeMaterial(materialContainer);
        }
        return true;
    }

    public void AddMaterial(MaterialSO material, int amount)
    {
        materialDictionary[material] += amount;
        OnMaterialAmountChange(material, amount);
    }

    public void AddMaterial(MaterialContainer materialContainer)
    {
        materialDictionary[materialContainer.material] += materialContainer.amount;
        OnMaterialAmountChange(materialContainer.material, materialContainer.amount);
    }


    void OnMaterialAmountChange(MaterialSO material, int amount)
    {
        if (onMaterialAmountChange != null)
            onMaterialAmountChange(material, materialDictionary[material]);
    }
}
