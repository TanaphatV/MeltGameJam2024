using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RawMaterial", menuName = "ScriptableObjects/RawMaterial")]
public class MaterialSO : ScriptableObject
{
    public string materialName;
    public Sprite icon;
}

[System.Serializable]
public class MaterialContainer
{
    public MaterialSO material;
    public int amount;
    public MaterialContainer(MaterialSO material, int amount = 0)
    {
        this.material = material;
        this.amount = amount;
    }
}