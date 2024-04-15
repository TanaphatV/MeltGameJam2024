using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseQTEManager : MonoBehaviour
{
    private QTEMoldingPanel qteMoldingPanel;
    private QTETemperaturePanel qteTempPanel;
    private ProcessingStation station;

    private void Awake()
    {
        qteMoldingPanel = FindAnyObjectByType<QTEMoldingPanel>();
        qteTempPanel = FindAnyObjectByType<QTETemperaturePanel>();

        if (qteMoldingPanel == null || qteTempPanel == null)
        {
            Debug.LogError("Can't find object QTEPanel");
        }
    }

    public IEnumerator StartQTEFlow(ProcessingStation _ps)
    {
        station = _ps;
        yield return StartQTEMoldingWeapon();

        station.currentItemCraftingStatus = CraftingStatus.Completed;

        //station.CreateItem(item, isHighQuality);
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
        qteTempPanel.InitQTEProcess();
        qteTempPanel.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();
    }
}
