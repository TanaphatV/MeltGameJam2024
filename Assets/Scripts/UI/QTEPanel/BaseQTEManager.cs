using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseQTEManager : MonoBehaviour
{
    [SerializeField] private QTEMoldingPanel qteMoldingPanel;
    [SerializeField] private QTETemperaturePanel qteTempPanel;
    [SerializeField] private GameObject panel;
    private ProcessingStation station;
    private RecipeListManagerGUI recipePanel;

    #region GETTER
    public ProcessingStation GetStation => station;
    public RecipeListManagerGUI GetRecipePanel => recipePanel;
    public QTEMoldingPanel GetQTEMoldPanel => qteMoldingPanel;
    #endregion

    private void Awake()
    {
        panel.SetActive(false);
        if (qteMoldingPanel == null || qteTempPanel == null)
        {
            Debug.LogError("Can't find object QTEPanel");
        }
    }

    public void DecreseDifficulty()
    {
        qteMoldingPanel.DecreaseDifficulty();
        qteTempPanel.DecreaseDifficulty();
        Debug.Log("Decrease Dif");
    }

    public IEnumerator StartQTEFlow()
    {
        panel.SetActive(true);

        yield return StartQTEMoldingWeapon();
    }

    public void BackToRecipeListPanel()
    {
        recipePanel.OpenPanel(station);
        panel.SetActive(false);
    }

    public void OpenPanel(ProcessingStation _ps, RecipeListManagerGUI gui)
    {
        recipePanel = gui;
        station = _ps;
        panel.SetActive(true);
        qteTempPanel.gameObject.SetActive(false);
        StartCoroutine(StartQTEFlow());
    }

    public IEnumerator StartQTEMoldingWeapon()
    {
        int newMinimum = Random.Range(60, 85);
        int newMax = newMinimum + Random.Range(5, 10);
        qteMoldingPanel.gameObject.SetActive(true);
        yield return qteMoldingPanel.Init(newMinimum, newMax, this);
        qteMoldingPanel.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator StartQTEControlTemperature()
    {
        qteTempPanel.gameObject.SetActive(true);
        yield return qteTempPanel.InitQTEProcess(station);

        station.currentItemCraftingStatus = CraftingStatus.Completed;

        yield return new WaitForEndOfFrame();
    }
}
