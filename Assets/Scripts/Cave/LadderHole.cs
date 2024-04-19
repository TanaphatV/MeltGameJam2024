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
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            AudioManager.Instance.PlaySFX("Ladder1");
            player.pause = true;
            FadeManager.Instance.StartFade(1.0f, 0.2f, () => { player.gameObject.transform.position = CaveManager.instance.GoToNextCave(); }, () => { player.pause = false; });
        }
    }
}
