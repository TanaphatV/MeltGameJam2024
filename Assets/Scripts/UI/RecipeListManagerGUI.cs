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
    [SerializeField] private ResourceUIController resourceUI;
    private ResourceSO resource;
    private List<RecipeSocketGUI> recipeSocketList = new List<RecipeSocketGUI>();
    private int selectingIndex;
    private bool isSelectNormal = true;
    private ProcessingStation station;
    private int minimumShow;
    private int maximumshow;

    public UnityAction<ItemSO, bool> onSelectedItemToCreate;

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
                    newSocket.AddButtonListenerNormal(() => CheckMaterialResource());
                    recipeSocketList.Add(newSocket);
                }
                i++;
            }
        }
        minimumShow = 0;
        maximumshow = 3;
        socketTemplate.gameObject.SetActive(false);

        DisplayScrollListButton();

        recipeSocketList[selectingIndex].UnselectAll();
        ChooseMode();
    }
    private void DisplayScrollListButton()
    {
        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        if(minimumShow != 0)
        {
            prevButton.gameObject.SetActive(true);
        }
        if(maximumshow != recipeSocketList.Count - 1)
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
                    if (selectingIndex < minimumShow)
                    {
                        minimumShow--;
                        maximumshow--;
                        StartCoroutine(MoveVerticalPanel(verticalLayout.GetComponent<RectTransform>().localPosition.y, verticalLayout.GetComponent<RectTransform>().localPosition.y - 200));
                    }
                    ChooseMode();
                    DisplayScrollListButton();
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selectingIndex < recipeSocketList.Count - 1)
                {
                    recipeSocketList[selectingIndex].UnselectAll();
                    selectingIndex++;
                    if (selectingIndex > maximumshow)
                    {
                        minimumShow++;
                        maximumshow++;
                        StartCoroutine(MoveVerticalPanel(verticalLayout.GetComponent<RectTransform>().localPosition.y, verticalLayout.GetComponent<RectTransform>().localPosition.y+200));
                    }
                    ChooseMode();
                    DisplayScrollListButton();
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
                //CheckMaterialResource();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanel();
            }
        }
    }

    public void CheckMaterialResource()
    {
        if (PlayerResources.instance.HaveEnoughMulitpleMaterial(recipeSocketList[selectingIndex].GetItemSO.normalQualityRecipe) && isSelectNormal)
        {
            ExecuteQTEFlow();
            PlayerResources.instance.TakeMulitpleMaterial(recipeSocketList[selectingIndex].GetItemSO.normalQualityRecipe);
            
        }
        else if(PlayerResources.instance.HaveEnoughMulitpleMaterial(recipeSocketList[selectingIndex].GetItemSO.highQualityRecipe) && !isSelectNormal)
        {
            ExecuteQTEFlow();
            PlayerResources.instance.TakeMulitpleMaterial(recipeSocketList[selectingIndex].GetItemSO.highQualityRecipe);
        }
        else
        {
            Debug.Log("Not enough");
        }
        
    }

    private IEnumerator MoveVerticalPanel(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.15f)
        {
            float t = elapsedTime / 0.15f;
            float posY = Mathf.Lerp(start, end, t);

            Vector3 newPosition = verticalLayout.GetComponent<RectTransform>().localPosition;
            newPosition.y = posY;
            verticalLayout.GetComponent<RectTransform>().localPosition = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        verticalLayout.GetComponent<RectTransform>().localPosition = new Vector3(verticalLayout.GetComponent<RectTransform>().localPosition.x, 
            end, verticalLayout.GetComponent<RectTransform>().localPosition.z);
    }


    private void ExecuteQTEFlow()
    {
        //ProcessingStation shortCut;
        onSelectedItemToCreate(recipeSocketList[selectingIndex].GetItemSO, isSelectNormal);
        qteManager.OpenPanel(station, this);
        ClosePanel();
        //resourceUI.UpdateMaterialList();
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
