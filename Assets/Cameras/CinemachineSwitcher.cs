using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("InsideWorkshop");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.Play("InsideFreezer");
        }
    }
}
