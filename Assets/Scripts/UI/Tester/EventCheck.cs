using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventCheck : MonoBehaviour, IPointerClickHandler
{
    public bool isDebugingMode = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (true)
        {
            Debug.Log("Clicked UI Element: " + gameObject.name);
        }
    }
}
