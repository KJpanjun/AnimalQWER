using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Damage,
        Die
    }

    public EnemyState state;

    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (state == EnemyState.Idle)
            Idle();
        else if (state == EnemyState.Chase)
            Chase();
        else if (state == EnemyState.Attack)
            Attack();
        else if (state == EnemyState.Damage)
            Damage();
        else if (state == EnemyState.Die)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "QSkillObject")
        {
            gameObject.SetActive(false);
        }
    }

    private const float idlePatrolTime = 3f;
    private float _idlePatrolTimer;

    void Idle()
    {
        _idlePatrolTimer += Time.deltaTime;

        if (_idlePatrolTimer > idlePatrolTime)
        {
            _agent.destination = transform.position + Random.insideUnitSphere * 3f;
            _idlePatrolTimer = 0f;
        }
    }

    private Player _targetPlayer;

    void Chase()
    {
        if (_targetPlayer == null) _targetPlayer = FindFirstObjectByType<Player>();

        _agent.destination = _targetPlayer.transform.position;
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > 8)
        {
            state = EnemyState.Idle;
        }
        else if (Vector3.Distance(transform.position, _targetPlayer.transform.position) < 2)
        {
            state = EnemyState.Attack;
        }
    }

    void Attack()
    {
        // 여기에 공격로직!
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > 2)
        {
            state = EnemyState.Chase;
        }
    }

    void Damage()
    {
    }

    void Die()
    {
    }
}

