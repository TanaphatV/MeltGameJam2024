using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WagonPriceDealHUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI qualityText;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI customerPreferText;
    [SerializeField] private GameObject gfx;

    private string savedInput = ""; // To store the valid input as a string

    void Start()
    {
        gfx.SetActive(false);
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber; // Set content type to accept only integer numbers
        inputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(); });
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
            savedInput = newText;
        }
        else
        {
            inputField.text = savedInput;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
