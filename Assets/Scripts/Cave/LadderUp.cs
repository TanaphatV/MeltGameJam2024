using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.pause = true;
            FadeManager.Instance.StartFade(1.0f, 0.2f, () => { collision.gameObject.transform.position = CaveManager.instance.GoToPreviousCave(); }, () => { player.pause = false; });
        }
    }
}
