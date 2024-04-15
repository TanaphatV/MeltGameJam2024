using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseQTEManager : MonoBehaviour
{
    private QTEMoldingPanel qteMoldingPanel;
    private QTETemperaturePanel qteTempPanel;

    private void Awake()
    {
        qteMoldingPanel = FindAnyObjectByType<QTEMoldingPanel>();
        qteTempPanel = FindAnyObjectByType<QTETemperaturePanel>();
    }

    public void 
}
