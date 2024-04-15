using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Vector2 attackSize;
    public float invulnerabilityDuration;
    public float invulnFlashingInterval;
    [SerializeField] SpriteRenderer spriteRenderer;
    int hp;
    enum Direction
    {
        Left,
        Right
    }

    public void Init()
    {
        hp = PlayerStats.instance.maxHp;
    }

    Direction mouseDirection;
    public void Attack()
    {
        FindMouseDirection();
        Debug.Log(mouseDirection);

        Vector3 offset = new Vector3((attackSize.x / 2.0f), 0, 0);
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - offset + (offset * 2 * (int)mouseDirection), attackSize, 0);//collider array to hit multiple enemies at once
        foreach(var col in cols)
        {
            if (col.TryGetComponent(out IHitable obj))
            {
                obj.Hit();
            }
        }

    }
    bool invulnerable = false;
    public void Hit(int damage)
    {
        if (invulnerable)
            return;
        hp -= damage;
        StartCoroutine(InvulnerabilityIE());
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
            mouseDirection = Direction.Right;
        else
            mouseDirection = Direction.Left;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3((attackSize.x / 2.0f), 0, 0);
        Gizmos.DrawWireCube(transform.position - offset + (offset * 2 * (int)mouseDirection), attackSize);
    }
}

public interface IHitable
{ 
    public void Hit();
}