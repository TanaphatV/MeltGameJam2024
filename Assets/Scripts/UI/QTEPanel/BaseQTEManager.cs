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

    private void Awake()
    {
        //qteMoldingPanel = FindAnyObjectByType<QTEMoldingPanel>();
        //qteTempPanel = FindAnyObjectByType<QTETemperaturePanel>();

        if (qteMoldingPanel == null || qteTempPanel == null)
        {
            Debug.LogError("Can't find object QTEPanel");
        }
    }

    public IEnumerator StartQTEFlow()
    {
        panel.SetActive(true);

        yield return StartQTEMoldingWeapon();

        station.currentItemCraftingStatus = CraftingStatus.Completed;
    }

    public void OpenPanel(ProcessingStation _ps)
    {
        station = _ps;
        panel.SetActive(true);
        StartCoroutine(StartQTEFlow());
    }

    public IEnumerator StartQTEMoldingWeapon()
    {
        int newMinimum = Random.Range(60, 85);
        int newMax = newMinimum + Random.Range(5, 10);
        qteMoldingPanel.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        yield return qteMoldingPanel.Init(newMinimum, newMax, this);
        qteMoldingPanel.gameObject.SetActive(false);

        yield return null;
    }

    public IEnumerator StartQTEControlTemperature()
    {
        Debug.Log("StartQTETEM");
        qteTempPanel.gameObject.SetActive(true);
        qteTempPanel.InitQTEProcess();
        //qteTempPanel.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }
}
