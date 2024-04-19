using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimeManager : MonoBehaviour
{
    #region singleton
    private static TimeManager _instance;
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Error, instance is null");
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] float dayLength;
    public UnityAction onDayEnd;
    float timer;
    public bool pause = false;
    private int dayCount = 1;
    public int DayCount
    {
        get { return dayCount; }
        set { dayCount = value; }
    }


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        timer = dayLength;
    }
    public string GetTimerText()
    {
        int intTime = Mathf.FloorToInt(timer);

        // Calculate minutes and seconds
        int minutes = intTime / 60;
        int seconds = intTime % 60;

        // Format the timer text
        string text = minutes.ToString("D2") + ":" + seconds.ToString("D2");

        return text;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (onDayEnd != null)
                onDayEnd(); DayCount++;
            timer = dayLength;
        }
    }
    IEnumerator TimerIE()
    {
        while(true)
        {
            yield return null;
            float timer = dayLength;
            while (timer > 0)
            {
                yield return new WaitUntil(() => { return !pause; });
                timer -= Time.deltaTime;
                yield return null;
            }
            if (onDayEnd != null)
                onDayEnd();
            timer = dayLength;
        }
    }
}
