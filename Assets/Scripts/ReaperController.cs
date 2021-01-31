using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class ReaperController : MonoBehaviour, IAttackEvent
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    
    [Serializable]
    internal enum STATE
    {
        NONE,
        IDLE,
        MOVE,
        CHASE,
        ATTACK
    }

    [SerializeField]
    private STATE _currentState;
    
    //[SerializeField]
    private PlayerController _playerController;
    private Animator _animator;

    [SerializeField, Header("States")] 
    private float idleWaitTime;
    private float _idleTimer;

    [SerializeField]
    private float detectDistance;
    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private float attackHoldTime = 1f;

    private float _attackTimer;
    
    [SerializeField, Header("Movement")]
    private float patrolSpeed;
    [SerializeField]
    private float chaseSpeed;
    
    [SerializeField, Header("Patrol Area")]
    private Vector3 minPosition;
    [SerializeField]
    private Vector3 maxPosition;

    private bool _isChasing;
    private NavMeshAgent _navMeshAgent;

    private new Transform transform;

    //====================================================================================================================//
    
    // Start is called before the first frame update
    private void Start()
    {
        transform = gameObject.transform;

        _playerController = FindObjectOfType<PlayerController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        //TODO Need to wait to start
        SetState(STATE.IDLE);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
    }

    //====================================================================================================================//

    private void SetState(in STATE newState)
    {
        _currentState = newState;
        
        switch (newState)
        {
            case STATE.NONE:
                break;
            case STATE.IDLE:
                _idleTimer = idleWaitTime;
                break;
            case STATE.MOVE:
                SetNewTargetPosition(FindNewRandomPosition());
                _navMeshAgent.speed = patrolSpeed;
                break;
            case STATE.CHASE:
                _navMeshAgent.speed = chaseSpeed;
                break;
            case STATE.ATTACK:
                _animator.SetTrigger(ATTACK);
                _attackTimer = attackHoldTime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void UpdateState()
    {
        if (_isChasing != _playerController.FlashLightOn)
        {
            _isChasing = _playerController.FlashLightOn;

            if (_isChasing && (_currentState == STATE.MOVE || _currentState == STATE.IDLE))
            {
                SetState(STATE.CHASE);
                return;
            }
        }

        switch (_currentState)
        {
            case STATE.NONE:
                return;
            case STATE.IDLE:
                IdleState();
                break;
            case STATE.MOVE:
                MoveState();
                break;
            case STATE.CHASE:
                ChaseState();
                break;
            case STATE.ATTACK:
                AttackState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void IdleState()
    {
        if (IsPlayerInView())
        {
            SetState(STATE.CHASE);
            return;
        }
        
        if (_idleTimer > 0)
        {
            _idleTimer -= Time.deltaTime;

            return;
        }

        _idleTimer = 0f;
        SetState(STATE.MOVE);
    }

    private void MoveState()
    {
        Debug.DrawLine(transform.position, _navMeshAgent.destination, Color.cyan);

        if (IsPlayerInView())
        {
            SetState(STATE.CHASE);
            return;
        }
        
        if (_navMeshAgent.remainingDistance > 0.1f)
            return;

        //Stops Moving the reaper
        SetNewTargetPosition(transform.position);
        
        SetState(STATE.IDLE);
    }

    private void ChaseState()
    {
        if(_isChasing)
            SetNewTargetPosition(_playerController.transform.position);
        
        //TODO Check to see if the player is in range
        if (Vector3.Distance(_playerController.transform.position, transform.position) <= attackDistance)
        {
            SetState(STATE.ATTACK);
            SetNewTargetPosition(transform.position);
            return;
        }
        
        if (_navMeshAgent.remainingDistance > 0.1f)
            return;
        
        //Stops Moving the reaper
        SetNewTargetPosition(transform.position);
        SetState(STATE.IDLE);
    }

    private void AttackState()
    {
        if (_attackTimer > 0f)
        {
            _attackTimer -= Time.deltaTime;
            return;
        }
        
        SetState(_isChasing ? STATE.CHASE : STATE.IDLE);
    }

    //====================================================================================================================//

    private void SetNewTargetPosition(in Vector3 targetPosition)
    {
        _navMeshAgent.SetDestination(targetPosition);
    }

    private bool IsPlayerInView()
    {
        var playerPosition = _playerController.transform.position;
        var currentPosition = transform.position;
        
        var currentForward = transform.forward.normalized;
        var direction = (playerPosition - currentPosition).normalized;
        
        var check = Vector3.Distance(currentPosition, playerPosition) <= detectDistance;

        if (!check) return false;
        
        check = Vector3.Dot(currentForward, direction) >= 0.5f;
        
        Debug.DrawLine(currentPosition, playerPosition, check ? Color.green : Color.red);

        return check;
    }

    //====================================================================================================================//
    

    private Vector3 FindNewRandomPosition()
    {
        var x = Random.Range(minPosition.x, maxPosition.x);
        var z = Random.Range(minPosition.z, maxPosition.z);
        var newPosition = new Vector3(x, 2f, z);

        return !NavMesh.SamplePosition(newPosition, out var navMeshHit, 5.0f, NavMesh.AllAreas)
            ? FindNewRandomPosition()
            : navMeshHit.position;
    }

    //====================================================================================================================//
    
#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var TL = new Vector3(minPosition.x, 2f, maxPosition.z);
        var TR = new Vector3(maxPosition.x, 2f, maxPosition.z);
        var BR = new Vector3(maxPosition.x, 2f, minPosition.z);
        var BL = new Vector3(minPosition.x, 2f, minPosition.z);

        
        Gizmos.DrawLine(TL, TR);
        Gizmos.DrawLine(TR, BR);
        Gizmos.DrawLine(BR, BL);
        Gizmos.DrawLine(BL, TL);
    }

#endif
    public void AttackEvent()
    {
        if (IsPlayerInView())
        {
            //TODO Kill the player here
            Debug.LogError("Hit the Player");
        }
    }
}
