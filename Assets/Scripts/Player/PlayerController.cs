using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInteract playerInteract;

    private Vector2 movement;

    public bool isInDungeon = false;

    void Update()
    {
        // Get input for movement
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        movement = new Vector2(moveInputX, moveInputY).normalized;

        if (!isInDungeon)
            WorkShopBehavior();
        else
            DungeonBehavior();

    }

    private void WorkShopBehavior()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerInteract.InteractAction();
        }
    }
    private void DungeonBehavior()
    {

    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
