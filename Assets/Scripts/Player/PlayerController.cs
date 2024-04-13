using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 movement;

    void Update()
    {
        // Get input for movement
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        movement = new Vector2(moveInputX, moveInputY).normalized;

        // Rotate character to face movement direction (optional)
        //if (movement != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
