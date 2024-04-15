using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListManagerGUI : MonoBehaviour
{
    [SerializeField] private RecipeSocketGUI socketTemplate;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private MainCharacterHUDManager mainHud;
    private ResourceSO resource;
    private List<RecipeSocketGUI> recipeSocketList = new List<RecipeSocketGUI>();
    private int selectingIndex;
    private bool isSelectNormal = true;

    private void Start()
    {
        resource = RecipeSingletonManager.Instance.GetResource;
        InitPanel();
    }

    public void InitPanel()
    {
        if(resource.craftableItems.Count > 0)
        {
            //Debug.Log(resource.craftableItems.Count);
            foreach (ItemSO item in resource.craftableItems)
            {
                if(item != null)
                {
                    RecipeSocketGUI newSocket = Instantiate(socketTemplate, verticalLayout.transform);
                    newSocket.InitSocket(item);
                    recipeSocketList.Add(newSocket);
                }
            }
        }
        socketTemplate.gameObject.SetActive(false);

        recipeSocketList[selectingIndex].UnselectAll();
        ChooseMode();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectingIndex > 0)
            {
                recipeSocketList[selectingIndex].UnselectAll();
                selectingIndex--;
                ChooseMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(selectingIndex < recipeSocketList.Count - 1)
            {
                recipeSocketList[selectingIndex].UnselectAll();
                selectingIndex++;
                ChooseMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isSelectNormal)
            {
                isSelectNormal = !isSelectNormal;
                ChooseMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isSelectNormal)
            {
                isSelectNormal = !isSelectNormal;
                ChooseMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recipeSocketList[selectingIndex].ButtonInvoke(isSelectNormal);
            mainHud.ToggleRecipeListHUD(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainHud.ToggleRecipeListHUD(false);
        }
    }

    private void ChooseMode()
    {
        if (isSelectNormal)
        {
            recipeSocketList[selectingIndex].SelectNormalQuality();
        }
        else
        {
            recipeSocketList[selectingIndex].SelectHighQuality();
        }
    }
}
