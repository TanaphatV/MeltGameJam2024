using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{
    private Dictionary<MaterialSO, int> materialDictionary;
    [SerializeField] private GameObject currentMaterialUITemplate;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private MaterialSO testMat;

    void Start()
    {
        currentMoney.text = PlayerResources.instance.coin.ToString() + "$";

        //PlayerResources.instance.GetMaterialDictionary().Add(testMat, 10);
        PlayerResources.instance.AddMaterial(testMat, 10);

        materialDictionary = PlayerResources.instance.GetMaterialDictionary();
        PlayerResources.instance.onMaterialAmountChange += UpdateMaterial;

        Debug.Log(materialDictionary.Count);
        foreach (KeyValuePair<MaterialSO, int> pair in materialDictionary)
        {
            MaterialSO material = pair.Key;
            int materialQuantity = pair.Value;

            GameObject newMat = Instantiate(currentMaterialUITemplate, verticalLayout.transform);
            newMat.GetComponentInChildren<TextMeshProUGUI>().text = materialQuantity.ToString();
            newMat.GetComponentInChildren<Image>().sprite = material.icon;
        }
        currentMaterialUITemplate.SetActive(false);
    }

    void UpdateMaterial(MaterialSO matName,int amount)
    {
        materialDictionary[matName] = amount;
    }

}
