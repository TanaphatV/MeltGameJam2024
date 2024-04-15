using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRate : MonoBehaviour
{
    public float value1 = 0f;
    public float value2 = 0f;
    public float maxValue1 = 100f;
    public float maxValue2 = 70f;
    public float duration = 10f; // Duration in seconds for both values to reach their max

    private float incrementRate1;
    private float incrementRate2;

    void Start()
    {
        // Calculate the increment rate for each value based on the desired duration
        incrementRate1 = maxValue1 / duration;
        incrementRate2 = maxValue2 / duration;
    }

    void Update()
    {
        // Increase value1 and value2 over time
        value1 = Mathf.Clamp(value1 + incrementRate1 * Time.deltaTime, 0f, maxValue1);
        value2 = Mathf.Clamp(value2 + incrementRate2 * Time.deltaTime, 0f, maxValue2);

        // Example: Logging the values
        Debug.Log("Value 1: " + value1 + ", Value 2: " + value2);
    }
}
