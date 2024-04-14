using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public interface IBarSubject
{
    public UnityAction<float, float> onTargetValueChanged { get; set; }
}

[System.Serializable]
public enum BarMode
{
    AlwaysVisble,
    DisableAtZero,
    DisableAtMax,
    DisableAtBothEnds
}
public class BarObserver : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject subjectGO;
    [SerializeField] BarMode barDisplayMode;
    private IBarSubject subject;

    private void Start()
    {
        subject = subjectGO.GetComponent<IBarSubject>();
        if (barDisplayMode != BarMode.AlwaysVisble)
            slider.gameObject.SetActive(false);
        subject.onTargetValueChanged += UpdateBar;
    }
    public void UpdateBar(float currentValue,float maxValue)
    {
        slider.value = currentValue / maxValue;

        if(barDisplayMode == BarMode.DisableAtZero || barDisplayMode == BarMode.DisableAtBothEnds)
        {
            if (slider.value <= Mathf.Epsilon)
            {
                slider.gameObject.SetActive(false);
            }
            else
                slider.gameObject.SetActive(true);
        }

        if (barDisplayMode == BarMode.DisableAtMax || barDisplayMode == BarMode.DisableAtBothEnds)
        {
            if (slider.value >= 1.0f)
            {
                slider.gameObject.SetActive(false);
            }
            else
                slider.gameObject.SetActive(true);
        }


    }

}
