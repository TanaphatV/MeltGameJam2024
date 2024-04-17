using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class RecipeSocketGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemImg;
    [SerializeField] private MaterialSocketGUI matSocketTemplate;
    [SerializeField] private GameObject normalGridLayout;
    [SerializeField] private GameObject highGridLayout;

    [SerializeField] private Button normalQualityButton;
    [SerializeField] private Button highQualityButton;

    private ItemSO itemSO;
    public ItemSO GetItemSO => itemSO;
    private List<MaterialSocketGUI> matSocketList = new List<MaterialSocketGUI>();
    public void InitSocket(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        itemName.text = itemSO.itemName;
        itemImg.sprite = itemSO.itemSprite;
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
        normalQualityButton.GetComponent<Image>().color = Color.cyan;
    }

    public void SelectHighQuality()
    {
        normalQualityButton.GetComponent<Image>().color = Color.white;
        highQualityButton.GetComponent<Image>().color = Color.cyan;
    }

    public void UnselectAll()
    {
        normalQualityButton.GetComponent<Image>().color = Color.white;
        highQualityButton.GetComponent<Image>().color = Color.white;
    }
    public void ButtonInvoke(bool isNormal)
    {
        if (isNormal)
        {
            normalQualityButton.onClick.Invoke();
        }
        else
        {
            highQualityButton.onClick.Invoke();
        }
    }

    public void AddButtonListenerNormal(UnityAction action)
    {
        normalQualityButton.onClick.RemoveAllListeners();
        normalQualityButton.onClick.AddListener(action);
        highQualityButton.onClick.RemoveAllListeners();
        highQualityButton.onClick.AddListener(action);
    }

    public void AddButtonListenerHigh(UnityAction action)
    {
        
    }
}
