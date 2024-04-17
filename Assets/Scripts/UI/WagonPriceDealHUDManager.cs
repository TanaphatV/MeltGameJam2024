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

    private int priceSet = 0;

    private string savedInput = ""; // To store the valid input as a string

    void Start()
    {
        gfx.SetActive(false);
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber; // Set content type to accept only integer numbers
        inputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(); });
    }

    public void InitPanel(Item item)
    {
        itemName.text = item.itemSo.itemName;
        itemIcon.sprite = item.itemSo.itemSprite;
        //item.
        if (item.highQuality)
        {
            qualityText.text = "High Quality";
        }
        else
        {
            qualityText.text = "Low Quality";
        }
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
        }
        
    }
    public void DecreasePriceByOne()
    {
        if (priceSet > 0)
        {
            priceSet--;
            savedInput = priceSet.ToString();
            inputField.text = savedInput;
        }
    }
}
