using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPathfinding : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementDirection = new();
    [SerializeField] private float moveSpeed = 2f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movementDirection * (moveSpeed * Time.fixedDeltaTime));
    }


    public void MoveTo(Vector2 movement)
    {
        _movementDirection = movement;
    }
}
