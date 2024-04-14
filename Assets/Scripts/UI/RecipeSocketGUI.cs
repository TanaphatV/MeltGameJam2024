using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeSocketGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemImg;
    [SerializeField] private MaterialSocketGUI matSocketTemplate;
    [SerializeField] private GameObject normalGridLayout;
    [SerializeField] private GameObject highGridLayout;

    [SerializeField] private Button normalQualityButton;
    [SerializeField] private Button highQualityButton;

    private List<MaterialSocketGUI> matSocketList = new List<MaterialSocketGUI>();
    public void InitSocket(ItemSO itemSO)
    {
        itemName.text = itemSO.itemName;
        itemImg.sprite = itemSO.finishedItem;
        foreach (MaterialContainer mat in itemSO.normalQualityRecipe)
        {
            MaterialSocketGUI newSocket = Instantiate(matSocketTemplate, normalGridLayout.transform);
            newSocket.InitSocket(mat);
            matSocketList.Add(newSocket);
        }
        foreach (MaterialContainer mat in itemSO.highQualityRecipe)
        {
            MaterialSocketGUI newSocket = Instantiate(matSocketTemplate, highGridLayout.transform);
            newSocket.InitSocket(mat);
            matSocketList.Add(newSocket);
        }
        matSocketTemplate.gameObject.SetActive(false);

        normalQualityButton.onClick.AddListener(() => Debug.Log("normal"));
        highQualityButton.onClick.AddListener(() => Debug.Log("high"));
    }

    public void SelectNormalQuality()
    {
        highQualityButton.GetComponent<Image>().color = Color.white;
        normalQualityButton.GetComponent<Image>().color = Color.blue;
    }

    public void SelectHighQuality()
    {
        normalQualityButton.GetComponent<Image>().color = Color.white;
        highQualityButton.GetComponent<Image>().color = Color.blue;
    }

    public void UnselectAll()
    {
        normalQualityButton.GetComponent<Image>().color = Color.white;
        highQualityButton.GetComponent<Image>().color = Color.white;
    }
}
