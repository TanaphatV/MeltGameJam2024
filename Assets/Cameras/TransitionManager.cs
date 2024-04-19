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

            TransitionBetweenZone(mineExit.position + (Vector3.up), "InsideMine", true);
        }
        else if(collision.tag == "MineExit")
        {
            
            TransitionBetweenZone(mineEntrace.position + (Vector3.down * 2), "InsideWorkshop",false);
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

    public void TransitionBetweenZone(Vector3 destination, string cameraState = null,bool isInDungeon = false)
    {
        playerController.isInDungeon = isInDungeon;
        playerController.pause = true;
        UnityAction temp = () => {
            transform.position = destination;
            if (cameraState != null)
                _cameraAnimator.Play(cameraState);
        };

        playerController.pause = true;
        FadeManager.Instance.StartFade(1.0f, 0.2f, temp, () => { playerController.pause = false; });
    }
}
