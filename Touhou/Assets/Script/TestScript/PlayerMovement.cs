using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movementDirection;
    private Vector2 lastMovementDirection;

    void Update()
    {
        // Input
        ProcessInputs();
        Animate();
    }

    
    private void FixedUpdate() 
    {
        // Movement
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementDirection.x * moveSpeed, movementDirection.y * moveSpeed);
    }
    
    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && movementDirection.x != 0 || movementDirection.y != 0)
        {
            lastMovementDirection = movementDirection;
        }

        movementDirection = new Vector2(moveX, moveY).normalized;
    }


    private void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        animator.SetFloat("LastMoveHorizontal", lastMovementDirection.x);
        animator.SetFloat("LastMoveVertical", lastMovementDirection.y);
    }

}
