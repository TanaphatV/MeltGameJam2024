using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHole : MonoBehaviour
{
    [SerializeField] Transform returnPoint;
    public Vector3 GetReturnPosition()
    {
        return returnPoint.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GotoNextCaveIE(collision));
        }
    }
    IEnumerator GotoNextCaveIE(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().pause = true;
        FadeManager.Instance.StartFade(1.0f,0.2f,() => { collision.gameObject.transform.position = CaveManager.instance.GoToNextCave(); });
        yield return new WaitUntil(() => { return FadeManager.Instance.FadeDone(); });
        collision.gameObject.GetComponent<PlayerController>().pause = false;
    }
}
