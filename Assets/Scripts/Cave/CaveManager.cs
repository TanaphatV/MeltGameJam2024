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
    
    List<Cave> CaveLayoutPrefabs = new List<Cave>();


    public void GoToNextCave()
    {

    }

    public void GoToPreviousCave()
    {

    }
}
