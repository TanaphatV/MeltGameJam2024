using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUIController : MonoBehaviour
{
    PlayerResources playerResources;
    private Dictionary<MaterialSO, int> materialDictionary;
    [SerializeField] private GameObject currentMaterialUITemplate;
    [SerializeField] private GameObject verticalLayout;

    void Awake()
    {
        materialDictionary = playerResources.GetMaterialDictionary();
        playerResources.onMaterialAmountChange += UpdateMaterial;

        foreach (KeyValuePair<MaterialSO, int> pair in materialDictionary)
        {
            MaterialSO material = pair.Key;
            int materialQuantity = pair.Value;

            GameObject newMat = Instantiate(currentMaterialUITemplate, verticalLayout.transform);
            newMat.GetComponentInChildren<TextMeshProUGUI>().text = materialQuantity.ToString();
        }
        currentMaterialUITemplate.SetActive(false);
    }

    void UpdateMaterial(MaterialSO matName,int amount)
    {
        materialDictionary[matName] = amount;
    }

}
