using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSingleton : MonoBehaviour
{
    #region singleton
    private static PlayerStatsSingleton _instance;
    public static PlayerStatsSingleton instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("Error, instance is null");
            }
            return _instance;
        }
    }
    #endregion

    public int wagonStorage;
    public int damage = 1;
    public int mining = 1;
    public float reputationGainModifier;
    public float freezerWaitTimeReduction;

    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
