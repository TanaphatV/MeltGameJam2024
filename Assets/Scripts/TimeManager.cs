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
    [SerializeField] Transform bed;
    public UnityAction onDayEnd;
    float timer;
    PlayerController playerController;
    private int dayCount = 1;
    public bool pause = false;
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
        playerController = FindObjectOfType<PlayerController>();
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

    public int GetMinuteTimer => Mathf.FloorToInt(timer) / 60;
    public bool pauseTimer;
    // Update is called once per frame
    void Update()
    {
        if (timer > 0 & !pauseTimer)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            DayEnd();
        }
    }
    public void DayEnd()
    {
        pauseTimer = true;
        playerController.pause = true;
        if (onDayEnd != null)
            onDayEnd(); DayCount++;
        timer = dayLength;
        FadeManager.Instance.StartFade(1.5f, 0.5f, null, () =>
          {
              pauseTimer = false;
              playerController.pause = false;
          });
    }
}
