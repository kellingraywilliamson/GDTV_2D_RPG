using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int MoveX = Animator.StringToHash("moveX");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerDirection();
        Move();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat(MoveX, _movement.x);
        _animator.SetFloat(MoveY, _movement.y);
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerDirection()
    {
        _spriteRenderer.flipX = Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x;
    }
}
