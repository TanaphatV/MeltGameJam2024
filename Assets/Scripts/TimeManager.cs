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


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        StartCoroutine(TimerIE());
    }
    public string GetTimerText()
    {
        int intTime = Mathf.FloorToInt( timer);
        int minute =  intTime/ 60;
        int seconds = intTime - (minute*60);
        string text = minute.ToString() + ":" + seconds.ToString();
        return text;
    }
    // Update is called once per frame
    void Update()
    {

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
