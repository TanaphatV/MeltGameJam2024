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
        PlayerResources.instance.onMaterialAmountChange += UpdateMaterial;
        materialSocketTemplate.gameObject.SetActive(false);
        Init();
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

    public RectTransform GetMaterialUIRectTransform(MaterialSO materialSO)
    {
        return matUIList[materialSO].gameObject.GetComponent<RectTransform>();
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
