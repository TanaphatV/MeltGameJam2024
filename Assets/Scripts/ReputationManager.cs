using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReputationRank
{
    unranked,
    bronze,
    silver,
    gold
}

public class ReputationManager : MonoBehaviour
{
    #region singleton
    private static ReputationManager _instance;
    public static ReputationManager instance
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
    public ReputationRank currentRank { get; private set; }
    public float reputation;
    public float maxReputation;
    public float specialOrderMultiplier;

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
