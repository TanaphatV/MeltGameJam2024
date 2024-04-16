using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTETemperaturePanel : MonoBehaviour
{
    [SerializeField] private Button successButton;
    [SerializeField] private Button failButton;
    void Start()
    {
        //successButton.onClick.AddListener(()=>);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator InitQTEProcess()
    {
        Debug.Log("QTETempProcessing");
        yield return new WaitForEndOfFrame();
    }
}
