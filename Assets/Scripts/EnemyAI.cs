using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPathfinding))]
public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;


    private void Awake()
    {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            var roamPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}
