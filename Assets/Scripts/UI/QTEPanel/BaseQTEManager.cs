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

    private void Awake()
    {
        if (qteMoldingPanel == null || qteTempPanel == null)
        {
            Debug.LogError("Can't find object QTEPanel");
        }
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
        Debug.Log("StartQTETEM");
        qteTempPanel.gameObject.SetActive(true);
        yield return qteTempPanel.InitQTEProcess();

        Debug.Log("fff");
        station.currentItemCraftingStatus = CraftingStatus.Completed;

       //Debug.Log(station.currentItemCraftingStatus);

        yield return new WaitForEndOfFrame();
    }
}
