using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WagonPriceDealHUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI qualityText;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI customerPreferText;
    [SerializeField] private GameObject gfx;

    private Wagon wagon;
    private Wagon.ItemInfo iteminfo;

    private int priceSet = 0;
    public int GetCurrentPriceSet => priceSet;

    private string savedInput = "";

    void Start()
    {
        gfx.SetActive(false);
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(); });
    }

    public void InitPanel(Item item, Wagon wagon, Wagon.ItemInfo iteminfo)
    {
        this.wagon = wagon;
        this.iteminfo = iteminfo;
        itemName.text = item.itemSo.itemName;
        itemIcon.sprite = item.itemSo.itemSprite;
        if (item.highQuality)
        {
            qualityText.text = "High Quality";
        }
        else
        {
            qualityText.text = "Low Quality";
        }
    }

    public string GetSellChanceMessageForThisPrice()
    {
        iteminfo.price = priceSet;
        return wagon.GetSellChanceMessage(iteminfo);
    }

    public void OpenPanel()
    {
        gfx.SetActive(true);
    }
    public void ClosePanel()
    {
        gfx.SetActive(false);
    }

    void OnInputFieldValueChanged()
    {
        string newText = inputField.text;

        if (int.TryParse(newText, out int result))
        {
            if (result > 9999)
            {
                result = 9999;
            }
            savedInput = result.ToString();
            priceSet = result;
            inputField.text = priceSet.ToString();
            customerPreferText.text = GetSellChanceMessageForThisPrice();
        }
        else
        {
            inputField.text = savedInput;
        }
    }

    public void IncreasePriceByOne()
    {
        if (priceSet < 9999)
        {
            priceSet++;
            savedInput = priceSet.ToString();
            inputField.text = savedInput;
            customerPreferText.text = GetSellChanceMessageForThisPrice();
        }
        
    }
    public void DecreasePriceByOne()
    {
        if (priceSet > 0)
        {
            priceSet--;
            savedInput = priceSet.ToString();
            inputField.text = savedInput;
            customerPreferText.text = GetSellChanceMessageForThisPrice();
        }
    }
}
