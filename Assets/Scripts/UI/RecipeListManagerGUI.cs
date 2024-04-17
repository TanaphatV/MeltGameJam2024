using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeListManagerGUI : MonoBehaviour
{
    [SerializeField] private RecipeSocketGUI socketTemplate;
    [SerializeField] private GameObject gfx;
    [SerializeField] private GameObject verticalLayout;
    [SerializeField] private BaseQTEManager qteManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    private ResourceSO resource;
    private List<RecipeSocketGUI> recipeSocketList = new List<RecipeSocketGUI>();
    private int selectingIndex;
    private bool isSelectNormal = true;
    private ProcessingStation station;

    public UnityAction<ItemSO> onSelectedItemToCreate;

    private void Start()
    {
        resource = RecipeSingletonManager.Instance.GetResource;
        closeButton.onClick.AddListener(() => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>().SetPlayerPause(false));
        InitPanel();
    }

    public void InitPanel()
    {
        int i = 0;
        if (resource.craftableItems.Count > 0)
        {
            foreach (ItemSO item in resource.craftableItems)
            {
                if(item != null)
                {
                    int curIndex = i;
                    RecipeSocketGUI newSocket = Instantiate(socketTemplate, verticalLayout.transform);
                    newSocket.InitSocket(item);
                    newSocket.AddButtonListenerNormal(() => OpenQTEPanel());
                    recipeSocketList.Add(newSocket);
                }
                i++;
            }
        }
        socketTemplate.gameObject.SetActive(false);

        DisplayScrollListButton();

        recipeSocketList[selectingIndex].UnselectAll();
        ChooseMode();
    }
    private void DisplayScrollListButton()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        if (recipeSocketList.Count > 4 && selectingIndex < recipeSocketList.Count - 1)
        {
            nextButton.gameObject.SetActive(true);
        }
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
        onSelectedItemToCreate(recipeSocketList[selectingIndex].GetItemSO);
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
