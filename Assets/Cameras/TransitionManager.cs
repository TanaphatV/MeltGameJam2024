using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
public class TransitionManager : MonoBehaviour
{
    #region singleton
    private static TransitionManager _instance;
    public static TransitionManager instance
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

    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Transform mineExit;
    [SerializeField] private Transform mineEntrace;
    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeDelay;
    PlayerController playerController;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Freezer")
        {
            //Debug.Log("Enter Freezer");
            _cameraAnimator.Play("InsideFreezer");
        }
        else if(collision.tag == "MineEntrance")
        {
            playerController.isInDungeon = true;
            StartCoroutine(TransitionToMineIE(mineExit.position + (Vector3.up), "InsideMine"));
        }
        else if(collision.tag == "MineExit")
        {
            playerController.isInDungeon = false;
            StartCoroutine(TransitionToMineIE(mineEntrace.position + (Vector3.down * 2), "InsideWorkshop"));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Freezer")
        {
            //Debug.Log("Enter Workshop");
            _cameraAnimator.Play("InsideWorkshop");
        }
    }

    IEnumerator TransitionToMineIE(Vector3 destination, string cameraState = null)
    {
        playerController.pause = true;
        UnityAction temp = () => { 
            transform.position = destination;
            if (cameraState != null)
                _cameraAnimator.Play(cameraState);
         };
        FadeManager.Instance.StartFade(fadeSpeed,fadeDelay, temp);
        yield return new WaitUntil(() => { return FadeManager.Instance.FadeDone(); });
        playerController.pause = false;
    }
}
