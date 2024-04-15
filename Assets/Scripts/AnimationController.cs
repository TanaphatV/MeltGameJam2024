using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    string currentState;
    public UnityAction onAnimationEnd;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool uninterruptable = false;

    public void ChangeAnimState(string newState,bool isRight,bool uninterruptable = false)
    {
        if (this.uninterruptable)
            return;
        spriteRenderer.flipX = isRight;

        if (currentState == newState)
            return;
        if (uninterruptable)
            StartCoroutine(UninterruptableIE());

        StartCoroutine(AnimationEventIE());
        animator.Play(newState);
        currentState = newState;
    }

    IEnumerator UninterruptableIE()
    {
        this.uninterruptable = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => { return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1; });
        this.uninterruptable = false;
    }
    IEnumerator AnimationEventIE()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => { return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1; });
        if (onAnimationEnd != null)
            onAnimationEnd();
    }

}
