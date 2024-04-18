using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    #region singleton
    private static CaveManager _instance;
    public static CaveManager instance
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
    [SerializeField] Transform CavePosition;
    [SerializeField] GameObject caveEntrance;
    [SerializeField] List<Cave> CaveLayoutPrefabs = new List<Cave>();
    [SerializeField] List<OreDropChanceSO> floorDropChances = new List<OreDropChanceSO>();

    List<Cave> caveList = new List<Cave>();

    int currentFloor = -1;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        //TimeManager.instance.onDayEnd += RefreshCaveLayout;
        RefreshCaveLayout();
    }

    void RefreshCaveLayout()
    {
        for(int i = 0; i < caveList.Count;i++)
        {
            Destroy(caveList[i].gameObject);
        }
        caveList = new List<Cave>();

        for(int i = 0; i < 4; i++)
        {
            Cave temp = Instantiate(CaveLayoutPrefabs[Random.Range(0, CaveLayoutPrefabs.Count)]);
            temp.transform.position = CavePosition.position;
            temp.SetOreDropChance(floorDropChances[i]);
            temp.InitCave();
            temp.SetCaveActive(false);
            caveList.Add(temp);
            Debug.Log("NEW CAVE");
        }
    }

    public  Vector3 GoToNextCave()
    {
        if (currentFloor == -1)
        {
            caveEntrance.SetActive(false);
        }
        else
            caveList[currentFloor].SetCaveActive(false);
        currentFloor++;
        caveList[currentFloor].SetCaveActive(true);
        return caveList[currentFloor].GetSpawnPoint();
    }

    public Vector3 GoToPreviousCave()
    {
        if (currentFloor == 0)
        {
            caveEntrance.SetActive(true);
        }

        caveList[currentFloor].SetCaveActive(false);
        currentFloor--;
        
        
        if(currentFloor != -1)
        {
            caveList[currentFloor].SetCaveActive(true);
            return caveList[currentFloor].GetReturnPosition();
        }
        else
            return caveEntrance.GetComponentInChildren<LadderHole>().GetReturnPosition();
    }
}
