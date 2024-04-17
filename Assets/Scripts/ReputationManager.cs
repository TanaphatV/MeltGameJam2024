using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ReputationMilestoneBonus
{
    public float requiredReputation;
    public Sprite icon;
    public float specialOrderPayMultiplier;
    public float wagonBuyProbabilityIncrease;

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
    public int currentRank { get; private set; }
    private float reputation;
    public float maxReputation;
    private ReputationMilestoneBonus currentBonus;
    public ReputationMilestoneBonus bonus => currentBonus;
    public List<ReputationMilestoneBonus> mileStoneBonus;

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentRank = 0;
    }

    public void IncreaseReputation(float amount)
    {
        reputation += amount;
        if(reputation >= mileStoneBonus[currentRank + 1].requiredReputation)
        {
            currentRank++;
            currentBonus = mileStoneBonus[currentRank];
        }

        if (reputation >= maxReputation)
        {
            Win();
        }
    }

    void Win()
    {

    }
}
