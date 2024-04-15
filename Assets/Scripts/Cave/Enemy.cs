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
    [SerializeField] float detectRange;
    [SerializeField] float moveSpeed;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
    }

    public void Hit()
    {
        hp -= PlayerStats.instance.damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.TryGetComponent(out PlayerCombat playerCombat))
            {
                playerCombat.Hit(damage);
            }
        }
    }
}
