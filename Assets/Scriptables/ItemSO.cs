using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptable")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public float timeNeededtoFreeze;
    public int marketPrice;
    public int highQualityPrice;
    public int reputationReward;
    public Sprite moldSprite;
    [Header("Recipe")]
    public List<MaterialContainer> normalQualityRecipe;
    public List<MaterialContainer> highQualityRecipe;
}


