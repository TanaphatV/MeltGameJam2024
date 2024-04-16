using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{
    private Dictionary<MaterialSO, int> materialDictionary;
    public bool IsDebugingMode = false;
    [SerializeField] private GameObject currentMaterialUITemplate;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private List<MaterialSO> testMatList = new List<MaterialSO>();

    void Start()
    {
        currentMoney.text = PlayerResources.instance.coin.ToString() + "$";

        if (IsDebugingMode)
        {
            AddTester();
        }

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

    void AddTester()
    {
        foreach (MaterialSO mat in testMatList)
        {
            PlayerResources.instance.AddMaterial(mat, 10);
        }
    }

    void UpdateMaterial(MaterialSO matName,int amount)
    {
        materialDictionary[matName] = amount;
    }

}
