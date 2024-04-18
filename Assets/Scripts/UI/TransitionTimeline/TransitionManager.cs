using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransitionManager : MonoBehaviour
{
    #region Singleton
    public static TransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] private GameObject transitionDay;
    [ContextMenu("PlayTransition")]
    public void OnDayPassTransition()
    {
        transitionDay.gameObject.SetActive(true);
        transitionDay.GetComponent<PlayableDirector>().Play();
    }
}
