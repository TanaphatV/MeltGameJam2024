using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GotoPrevCaveIE(collision));
        }
    }
    IEnumerator GotoPrevCaveIE(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().pause = true;
        FadeManager.Instance.StartFade(1.0f, 0.2f, () => { collision.gameObject.transform.position = CaveManager.instance.GoToPreviousCave(); });
        yield return new WaitUntil(() => { return FadeManager.Instance.FadeDone(); });
        collision.gameObject.GetComponent<PlayerController>().pause = false;
    }
}
