using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MainHUD
{
    Material,
    Recipe,
    Money
}

public class MainCharacterHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject moneyHUD;
    [SerializeField] private GameObject materialList;
    [SerializeField] private GameObject hpHorizontal;
    [SerializeField] private Image hpIcon;

    private List<Image> hpIconList = new List<Image>();

    private void Start()
    {
        hpIcon.gameObject.SetActive(false);

        for (int i = 0; i < PlayerStats.instance.maxHp; i++)
        {
            Image newHpIcon = Instantiate(hpIcon, hpHorizontal.transform);
            newHpIcon.gameObject.SetActive(true);
            hpIconList.Add(newHpIcon);
        }
    }

    public void ToggleRecipeListHUD(bool isOpen)
    {
        moneyHUD.SetActive(!isOpen);
    }

    private void Update()
    {
        //if (PlayerResources.instance.hp > 0 && PlayerStats.instance.maxHp != 0 && hpIconList.Count < PlayerStats.instance.maxHp)
        //{
        //    Image newHpIcon = Instantiate(hpIcon, hpHorizontal.transform);
        //    newHpIcon.gameObject.SetActive(true);
        //    hpIconList.Add(newHpIcon);
        //}else if (PlayerResources.instance.hp < hpIconList.Count)
        //{
        //    Destroy(hpIconList[0].gameObject);
        //    hpIconList.RemoveAt(0);
        //}
    }
}