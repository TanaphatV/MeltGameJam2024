using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    public Vector2 attackSize;
    public float invulnerabilityDuration;
    public float invulnFlashingInterval;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerController playerController;
    [SerializeField] AnimationController animationController;
    int hp;

    public void Init()
    {
        hp = PlayerStats.instance.maxHp;
    }

    bool mouseDirectionIsRight;
    public void Attack()
    {
        FindMouseDirection();
        playerController.pause = true; ;
        animationController.onAnimationEnd += OnAttackEnd;
        animationController.ChangeAnimState("attack_side", mouseDirectionIsRight,true);
        playerController.isFacingRight = mouseDirectionIsRight;
        Vector3 offset = new Vector3((attackSize.x / 2.0f), 0, 0);
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - offset + (offset * 2 * Convert.ToInt32(mouseDirectionIsRight)), attackSize, 0);//collider array to hit multiple enemies at once
        foreach(var col in cols)
        {
            if (col.TryGetComponent(out IHitable obj))
            {
                obj.Hit();
            }
        }
    }

    void OnAttackEnd()
    {
        playerController.pause = false;
        animationController.onAnimationEnd -= OnAttackEnd;
    }

    //IEnumerator AttackIE()
    //{

    //}

    bool invulnerable = false;
    public void Hit(int damage,Vector3 knockForce)
    {
        if (invulnerable)
            return;
        hp -= damage;
        StartCoroutine(HitStunIE());
        rb.AddForce(knockForce, ForceMode2D.Impulse);
        StartCoroutine(InvulnerabilityIE());
    }
    IEnumerator HitStunIE()
    {
        playerController.pause = true;
        yield return new WaitForSeconds(0.9f);
        playerController.pause = false;

    }
    IEnumerator InvulnerabilityIE()
    {
        invulnerable = true;
        float timer = 0;
        while(timer < invulnerabilityDuration)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(invulnFlashingInterval);
            timer += invulnFlashingInterval;
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invulnFlashingInterval);
            timer += invulnFlashingInterval;
        }
        invulnerable = false;
    }

    void FindMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 pos = transform.InverseTransformPoint(mousePos);
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        if (angle >= -90 && angle <= 90)
            mouseDirectionIsRight = true;
        else
            mouseDirectionIsRight = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3((attackSize.x / 2.0f), 0, 0);
        Gizmos.DrawWireCube(transform.position - offset + (offset * 2 * Convert.ToInt32(mouseDirectionIsRight)), attackSize);
    }
}

public interface IHitable
{ 
    public void Hit();
}