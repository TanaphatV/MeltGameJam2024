using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Workshop")
        {
            //Debug.Log("Enter Workshop");
            _cameraAnimator.Play("InsideWorkshop");
        }
        if (collision.tag == "Freezer")
        {
            //Debug.Log("Enter Freezer");
            _cameraAnimator.Play("InsideFreezer");
        }
    }
}
