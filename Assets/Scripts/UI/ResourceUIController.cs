using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUIController : MonoBehaviour
{
    PlayerResources playerResources;
    private Dictionary<string, int> materialDictionary;
    [SerializeField] private GameObject currentMaterialUITemplate;
    [SerializeField] private GameObject verticalLayout;

    void Awake()
    {
        materialDictionary = playerResources.GetMaterialDictionary();
        playerResources.onMaterialAmountChange += UpdateMaterial;

        foreach (KeyValuePair<string, int> pair in materialDictionary)
        {
            string materialName = pair.Key;
            int materialQuantity = pair.Value;

            GameObject newMat = Instantiate(currentMaterialUITemplate, verticalLayout.transform);
            newMat.GetComponentInChildren<TextMeshProUGUI>().text = materialQuantity.ToString();
        }
        currentMaterialUITemplate.SetActive(false);
    }

    void UpdateMaterial(string matName,int amount)
    {
        materialDictionary[matName] = amount;
    }

}
