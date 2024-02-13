using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    [Header("Direction")]
    [SerializeField] private Vector2 _movementDirection;
    private Vector2 _lastMovementDirection;

    void Update()
    {
        // Input
        ProcessInputs();
        Animate();
    }

    private void FixedUpdate() 
    {
        HandleMove();
    }

    private void HandleMove()
    {
        // if(PlayerInputManager.Instance.GetInputMode())
        // { 
        //     _movementDirection = InputManager.Instance.GetMoveDirection();
        //     // Debug.Log(_movementDirection);
        //     _rb.velocity = _movementDirection * _moveSpeed;
        // }
        _rb.velocity = _movementDirection * _moveSpeed;
    }
    
    private void ProcessInputs()
    {
        // float moveX = Input.GetAxisRaw("Horizontal");
        // float moveY = Input.GetAxisRaw("Vertical");

        // if((moveX == 0 && moveX == 0) && _movementDirection.x != 0 || _movementDirection.y != 0)
        // {
        //     _lastMovementDirection = _movementDirection;
        // }

        // _movementDirection = new Vector2(moveX, moveY).normalized;
    }


    private void Animate()
    {
        _animator.SetFloat("Horizontal", _movementDirection.x);
        _animator.SetFloat("Vertical", _movementDirection.y);
        _animator.SetFloat("Speed", _movementDirection.sqrMagnitude);
        _animator.SetFloat("LastMoveHorizontal", _lastMovementDirection.x);
        _animator.SetFloat("LastMoveVertical", _lastMovementDirection.y);
    }
}
