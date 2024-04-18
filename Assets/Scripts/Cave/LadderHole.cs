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
            collision.gameObject.transform.position = CaveManager.instance.GoToNextCave();
        }
    }
}
