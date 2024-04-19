using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MaterialAmountDropChance
{
    public int amount;
    public float chance;
}

[System.Serializable]
public struct MaterialTypeDropChance
{
    public MaterialSO material;
    public float chance;
}

[CreateAssetMenu(fileName = "OreDropChance", menuName = "ScriptableObjects/OreDropChance")]
public class CaveInfoSpread : ScriptableObject
{
    [Header("Entity Spawn Count")]
    public int enemyCount;
    public int minOreCount;
    public int maxOreCount;
    [Header("Ore Drop")]
    public List<MaterialAmountDropChance> amountSpread;
    public List<MaterialTypeDropChance> typeSpread;

    public int GetRandomizeOreCount()
    {
        return Random.Range(minOreCount, maxOreCount + 1);
    }
    public MaterialContainer GetRandomizedMaterialDrop()
    {
        int dropAmount = 0;
        MaterialSO material = null;
        float random = Random.Range(0.0f, 1.0f);
        foreach (var drop in amountSpread)
        {
            if (random <= drop.chance)
            {
                dropAmount = drop.amount;
                break; // Exit the loop after dropping one material
            }
            else
            {
                random -= drop.chance;
            }
        }
        float random2 = Random.Range(0.0f, 1.0f);
        foreach (var drop in typeSpread)
        {
            if (random2 <= drop.chance)
            {
                material = drop.material;
                break; // Exit the loop after dropping one material
            }
            else
            {
                random2 -= drop.chance;
            }
        }
        return new MaterialContainer(material,dropAmount);
    }
}
