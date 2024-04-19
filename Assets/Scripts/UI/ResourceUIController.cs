using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private MaterialSocketGUI materialSocketTemplate;
    private Dictionary<MaterialSO,MaterialSocketGUI> matUIList = new Dictionary<MaterialSO, MaterialSocketGUI>();

    void Start()
    {
        Init();
        PlayerResources.instance.onMaterialAmountChange += UpdateMaterial;
        materialSocketTemplate.gameObject.SetActive(false);
        //UpdateMaterialList();
    }

    private void Init()
    {
        foreach (var material in PlayerResources.instance.GetMaterialDictionary().Keys)
        {
            MaterialSocketGUI newMatGUI = Instantiate(materialSocketTemplate, verticalLayout.transform);
            newMatGUI.InitSocket(material);
            newMatGUI.gameObject.SetActive(true);
            matUIList.Add(material,newMatGUI);
        }
    }

    public Vector3 GetWorldPositionFromMaterialUI(MaterialSO materialSO)
    {
        RectTransform rect = matUIList[materialSO].gameObject.GetComponent<RectTransform>();

        return rect.position;
    }

    private void Update()
    {
        currentMoney.text = PlayerResources.instance.coin.ToString() + "$";
    }

    void UpdateMaterial(MaterialSO matName,int amount)
    {
        matUIList[matName].UpdateSocket(amount);
    }

}
