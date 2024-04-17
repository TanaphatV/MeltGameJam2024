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
    public bool showName;
    public void InitSocket(MaterialSO mat,int amount = 0,bool showName = false)
    {
        materialIcon.sprite = mat.icon;
        if (showName)
            materialName.text = mat.materialName;
        materialAmount.text = "x" + amount;
    }
    public void InitSocket(MaterialContainer mat, bool showName = false)
    {
        InitSocket(mat.material, mat.amount,showName);
    }
    public void UpdateSocket(int amount)
    {
        materialAmount.text = "x" + amount;
    }
}
