using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private AnimationController animationController;

    private Vector2 movement;

    public bool isInDungeon = false;

    bool isFacingRight;

    void Update()
    {
        // Get input for movement
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        if (moveInputX > Mathf.Epsilon)
            isFacingRight = true;
        else if (moveInputX < -Mathf.Epsilon)
            isFacingRight = false;

        movement = new Vector2(moveInputX, moveInputY).normalized;
        if(movement.magnitude > Mathf.Epsilon)
        {
            animationController.ChangeAnimState("walkSide", isFacingRight);
        }

        if (!isInDungeon)
            WorkShopBehavior();
        else
            DungeonBehavior();

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerCombat.Attack();
        }
    }
}
