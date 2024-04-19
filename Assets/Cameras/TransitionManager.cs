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
    [SerializeField] SpriteRenderer freezerWall;
    [SerializeField] GameObject blackWorkShop;
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
            //blackWorkShop.SetActive(false);
            //StartCoroutine(FadeOutWall());
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
            //StartCoroutine(FadeInWall());
            //blackWorkShop.SetActive(true);
            _cameraAnimator.Play("InsideWorkshop");
        }
    }
    float alpha;
    IEnumerator FadeOutWall()
    {
        alpha = 1.0f;
        while (alpha > 0)
        {
            alpha -= 2 * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            freezerWall.color = new Color(freezerWall.color.r, freezerWall.color.g, freezerWall.color.b, alpha);
            yield return null;
        }
    }
    IEnumerator FadeInWall()
    {
        alpha = 0;
        while (alpha < 1.0f)
        {
            alpha += 2 * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            freezerWall.color = new Color(freezerWall.color.r, freezerWall.color.g, freezerWall.color.b, alpha);
            yield return null;
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
