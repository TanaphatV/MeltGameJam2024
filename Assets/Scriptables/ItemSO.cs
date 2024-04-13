using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptable")]
public class ItemSO : ScriptableObject
{
    public Sprite wipItem;
    public Sprite finishedItem;
    public float timeNeededtoFreeze;
    [Header("Recipe")]
    public List<MaterialContainer> requiredMaterial;
}


