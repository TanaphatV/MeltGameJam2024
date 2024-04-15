using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    string currentState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimState(string newState,bool isRight)
    {

            spriteRenderer.flipX = isRight;

        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }

}
