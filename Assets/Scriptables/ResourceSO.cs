using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObjects/ItemList")]
public class ResourceSO : ScriptableObject
{
    public List<ItemSO> craftableItems;
    public List<MaterialSO> pickableOres;
}
