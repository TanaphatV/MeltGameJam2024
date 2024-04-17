using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{
    //#region Singleton
    //private static ResourceUIController _instance;
    //public static ResourceUIController instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            Debug.Log("Error, instance is null");
    //        }
    //        return _instance;
    //    }
    //}
    //#endregion
    private Dictionary<MaterialSO, int> materialDictionary;
    public bool IsDebugingMode = false;
    [SerializeField] private GameObject currentMaterialUITemplate;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private List<MaterialSO> testMatList = new List<MaterialSO>();
    private List<GameObject> matUIList = new List<GameObject>();

    void Start()
    {

        if (IsDebugingMode)
        {
            AddTester();
        }

        materialDictionary = PlayerResources.instance.GetMaterialDictionary();
        PlayerResources.instance.onMaterialAmountChange += UpdateMaterial;

        UpdateMaterialList();
    }

    public void UpdateMaterialList()
    {
        //Debug.Log(materialDictionary.Count);
        foreach (GameObject ui in matUIList)
        {
            Destroy(ui.gameObject);
        }
        matUIList.Clear();
        //Debug.Log(materialDictionary.Count);
        foreach (KeyValuePair<MaterialSO, int> pair in materialDictionary)
        {
            MaterialSO material = pair.Key;
            int materialQuantity = pair.Value;

            GameObject newMat = Instantiate(currentMaterialUITemplate, verticalLayout.transform);
            newMat.GetComponentInChildren<TextMeshProUGUI>().text = materialQuantity.ToString();
            newMat.GetComponentInChildren<Image>().sprite = material.icon;
            newMat.gameObject.SetActive(true);
            matUIList.Add(newMat);
        }
        currentMaterialUITemplate.SetActive(false);
    }

    private void Update()
    {
        currentMoney.text = PlayerResources.instance.coin.ToString() + "$";
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
