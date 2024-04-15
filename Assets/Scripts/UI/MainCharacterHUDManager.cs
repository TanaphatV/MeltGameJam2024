using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject recipeList;

    public void ToggleRecipeListHUD(bool isOpen)
    {
        recipeList.SetActive(isOpen);
        moneyHUD.SetActive(!isOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (recipeList.activeSelf)
            {
                ToggleRecipeListHUD(false);
            }
            else
            {
                ToggleRecipeListHUD(true);
            }
            
        }
    }
}