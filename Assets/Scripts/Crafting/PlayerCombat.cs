using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class PlayerCombat : MonoBehaviour
{
    public Vector2 attackSize;
    public float invulnerabilityDuration;
    public float invulnFlashingInterval;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerController playerController;
    [SerializeField] AnimationController animationController;
    DeathScreenUI deathScreen;
    int currentHp;

    public UnityAction<int> onHpChange;

    private void Start()
    {
        deathScreen = FindObjectOfType<DeathScreenUI>();
        TimeManager.instance.onDayEnd += Init;
        TimeManager.instance.onDayEnd += DayEndDeath;
    }

    void DayEndDeath()
    {
        Death("from lack of sleep");
    }

    public void SetHp(int hp)
    {
        currentHp = hp;
        if (onHpChange != null)
            onHpChange(hp);
    }

    public void Init()
    {
        SetHp(PlayerStats.instance.maxHp);
        
    }

    bool mouseDirectionIsRight;
    public void Attack()
    {
        FindMouseDirection();
        playerController.pause = true; 
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
        if (invulnerable || dead)
            return;
        SetHp(currentHp - damage);
        AudioManager.Instance.PlaySFX("Hurt" + UnityEngine.Random.Range(1,5).ToString());

        if (currentHp <= 0)
        {
            TimeManager.instance.onDayEnd -= DayEndDeath;
            Death("Trying to fend off goobers");
            return;
        }

        StartCoroutine(HitStunIE());
        rb.AddForce(knockForce, ForceMode2D.Impulse);
        animationController.ChangeAnimState("hurt_side", mouseDirectionIsRight, true);
        hurtAnim = true;
        StartCoroutine(InvulnerabilityIE());
    }
    bool dead = false;
    void Death(string reason)
    {
        dead = true;
        playerController.pause = true;
        animationController.ChangeAnimState("dead",mouseDirectionIsRight);
        StartCoroutine(DeathIE(reason));
    }

    IEnumerator DeathIE(string reason)
    {
        UnityAction temp = () =>
        {
            Respawn();
        };

        yield return new WaitForSeconds(0.5f);
        deathScreen.StartFade(reason,temp);

    }

    void Respawn()
    {
        TransitionManager.instance.TransitionBetweenZone(new Vector3(8, 0, 0),"InsideWorkshop",false);
        CaveManager.instance.GoBackToTop();
        TimeManager.instance.DayEnd();
        TimeManager.instance.onDayEnd += DayEndDeath;
    }

    IEnumerator HitStunIE()
    {
        playerController.pause = true;
        yield return new WaitForSeconds(0.9f);
        playerController.pause = false;
        hurtAnim = false;

    }
    IEnumerator InvulnerabilityIE()
    {
        invulnerable = true;
        float timer = 0;
        while(hurtAnim)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        while (timer < invulnerabilityDuration)
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
    bool hurtAnim = true;
    void OnHurtEnd()
    {
        hurtAnim = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bed")
        {
            TimeManager.instance.onDayEnd -=  DayEndDeath;
            AudioManager.Instance.PlaySFX("Sleep");
            TimeManager.instance.DayEnd();
            TimeManager.instance.onDayEnd += DayEndDeath;
        }
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