using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OrderSocketGUI : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] TextMeshProUGUI orderName;
    [SerializeField] TextMeshProUGUI reputationReward;
    [SerializeField] TextMeshProUGUI payout;
    [SerializeField] GameObject orderCompleteGO;

    public void InitSocket(SpecialOrder specialOrder)
    {
        weaponImage.sprite = specialOrder.itemSO.itemSprite;
        orderName.text = specialOrder.itemSO.itemName;
        if (specialOrder.highQuality)
        {
            orderName.text = "High Quality " +specialOrder.itemSO.itemName;
        }
        reputationReward.text = specialOrder.itemSO.reputationReward.ToString();
        payout.text = specialOrder.payout.ToString();
        orderCompleteGO.SetActive(specialOrder.completed);

    }
}
