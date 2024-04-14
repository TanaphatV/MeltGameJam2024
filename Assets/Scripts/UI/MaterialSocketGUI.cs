using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaterialSocketGUI : MonoBehaviour
{
    [SerializeField] private Image materialIcon;
    [SerializeField] private TextMeshProUGUI materialName;
    [SerializeField] private TextMeshProUGUI materialAmount;
    public void InitSocket(MaterialContainer mat)
    {
        materialIcon.sprite = mat.material.icon;
        materialName.text = mat.material.materialName;
        materialAmount.text = "x" + mat.amount.ToString();
    }
}
