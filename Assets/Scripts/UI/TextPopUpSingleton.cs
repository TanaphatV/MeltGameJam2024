using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUpSingleton : MonoBehaviour
{
    private static TextPopUpSingleton _instance;
    public static TextPopUpSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TextPopUpSingleton>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("TextPopUpSingleton");
                    _instance = singletonObject.AddComponent<TextPopUpSingleton>();
                }
            }
            return _instance;
        }
    }
    public GameObject uiPanelPrefab;
    private GameObject uiPanelInstance;

    void Start()
    {
        uiPanelInstance = Instantiate(uiPanelPrefab);
        uiPanelInstance.SetActive(false);
    }
    public void ShowUIPanel(Vector3 mousePosition)
    {
        uiPanelInstance.SetActive(true);
        uiPanelInstance.transform.position = mousePosition;
    }

    public void HideUIPanel()
    {
        uiPanelInstance.SetActive(false);
    }
    public void OnMouseEnter()
    {
        Vector3 mousePosition = Input.mousePosition;
        ShowUIPanel(mousePosition);
    }

    // Method to handle mouse exit
    public void OnMouseExit()
    {
        HideUIPanel();
    }
}
