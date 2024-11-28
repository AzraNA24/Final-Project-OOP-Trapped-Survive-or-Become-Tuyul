using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    private float idleTimer = 0f; // Tracks time the player is stationary
    private float idleThreshold = 5f; // Time in seconds before switching to idle animation

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        bool isMoving = movement.sqrMagnitude > 0;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        if (idleTimer > idleThreshold)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
