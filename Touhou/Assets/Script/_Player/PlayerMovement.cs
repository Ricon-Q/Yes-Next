using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

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
        HandleMove();
        DrawScanRay();
    }

    private void HandleMove()
    {
        if(PlayerInputManager.Instance.GetInputMode())
        { 
            movementDirection = InputManager.Instance.GetMoveDirection();
            // Debug.Log(movementDirection);
            rb.velocity = movementDirection * moveSpeed;
        }
    }
    
    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveX == 0) && movementDirection.x != 0 || movementDirection.y != 0)
        {
            lastMovementDirection = movementDirection;
        }

        // movementDirection = new Vector2(moveX, moveY).normalized;
    }


    private void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        animator.SetFloat("LastMoveHorizontal", lastMovementDirection.x);
        animator.SetFloat("LastMoveVertical", lastMovementDirection.y);
    }

    private void DrawScanRay()
    {
        //Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set the z value to the camera's near clip plane
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 rayDirection = (worldMousePosition - transform.position).normalized;
        Debug.DrawRay(transform.position, rayDirection * 1f, new Color(0, 1, 0));
        // RaycastHit2D rayHit = Physics2D.Raycast(rb.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));

        // if(rayHit.collider != null)
        // {
        //     ScanObject = rayHit.collider.gameObject;
        // }
        // else
        // {
        //     ScanObject = null;
        // }
    }

}
