using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListManagerGUI : MonoBehaviour
{
    [SerializeField] private RecipeSocketGUI socketTemplate;
    [SerializeField] private GameObject gfx;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private MainCharacterHUDManager mainHud;
    [SerializeField] private BaseQTEManager qteManager;
    private ResourceSO resource;
    private List<RecipeSocketGUI> recipeSocketList = new List<RecipeSocketGUI>();
    private int selectingIndex;
    private bool isSelectNormal = true;
    private ProcessingStation station;

    private void Start()
    {
        resource = RecipeSingletonManager.Instance.GetResource;
        InitPanel();
    }

    public void InitPanel()
    {
        if (resource.craftableItems.Count > 0)
        {
            //Debug.Log(resource.craftableItems.Count);
            foreach (ItemSO item in resource.craftableItems)
            {
                if(item != null)
                {
                    RecipeSocketGUI newSocket = Instantiate(socketTemplate, verticalLayout.transform);
                    newSocket.InitSocket(item);
                    newSocket.AddButtonListener(OpenQTEPanel);
                    recipeSocketList.Add(newSocket);
                }
            }
        }
        socketTemplate.gameObject.SetActive(false);

        recipeSocketList[selectingIndex].UnselectAll();
        ChooseMode();
    }

    public void OpenPanel(ProcessingStation s)
    {
        station = s;
        gfx.SetActive(true);
    }
    public void ClosePanel()
    {
        gfx.SetActive(false);
    }

    private void Update()
    {
        if (gfx.activeSelf)
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
                if (selectingIndex < recipeSocketList.Count - 1)
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
                OpenQTEPanel();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanel();
            }
        }
        
    }

    public void OpenQTEPanel()
    {
        qteManager.OpenPanel(station, this);
        ClosePanel();
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
