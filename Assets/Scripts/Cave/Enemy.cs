using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
    enum State
    {
        patrol,
        chase
    }

    State currentState;
    [SerializeField] int hp;
    [SerializeField] int damage;
    [SerializeField] float knockForce;
    [SerializeField] float detectRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] MaterialDrop materialDropPrefab;
    [SerializeField] float dropRadius;
    public MaterialContainer materialDrop;

    bool stun = false;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (stun)
            return;
        if(currentState == State.patrol)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
    }

    void Patrol()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position,detectRange);
        foreach(var col in cols)
        {
            if(col.gameObject.tag == "Player")
            {
                target = col.gameObject.transform;
                currentState = State.chase;
            }
        }

    }

    void Chase()
    {
        Vector2 direction = target.position - transform.position;
        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }

    public void Hit()
    {
        Debug.Log("Hit");
        if(hp <= 0)
            return;
        
        hp -= PlayerStats.instance.damage;
        Vector3 direction = transform.position - target.position;
        StartCoroutine(HitStunIE());
        rb.AddForce(direction.normalized * PlayerStats.instance.knockForce,ForceMode2D.Impulse);
        if (hp <= 0)
        {
            Death();
        }

    }

    IEnumerator HitStunIE()
    {
        stun = true;
        yield return new WaitForSeconds(0.9f);
        stun = false;
    }

    void Death()
    {
        for (int i = 0; i < materialDrop.amount; i++)
        {
            MaterialDrop temp = Instantiate(materialDropPrefab);
            temp.gameObject.transform.position = transform.position;
            temp.Init(materialDrop.material, Random.insideUnitCircle * dropRadius);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCollision(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerCollision(collision);
    }
    void PlayerCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.TryGetComponent(out PlayerCombat playerCombat))
            {
                Vector2 direction = playerCombat.transform.position - transform.position;
                playerCombat.Hit(damage,direction.normalized * knockForce);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
