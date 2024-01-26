using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject playerDestination;
    public GameObject cursorOnSerface;
    public GameObject cursorOnDefault;

    public GameObject qSkillObject;
    public GameObject wSkillObject;
    public GameObject eSkillObject;
    public GameObject rSkillObject;

    public float cantMoveTime;

    public float qSkillCoolTime;
    public float wSkillCoolTime;
    public float eSkillCoolTime;
    public float rSkillCoolTime;

    private enum PlayerState
    {
        idle,
        move,
        skillQ,
        skillW,
        skillE,
        skillR,
        dance1,
        dance2,
        hit,
        death
    }
    private PlayerState playerState;

    private NavMeshAgent _agent;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_agent.remainingDistance < 0.1f && playerState == PlayerState.move)
            playerState = PlayerState.idle;
        if (_agent.remainingDistance > 0.1f && (playerState == PlayerState.idle || playerState == PlayerState.move))
            playerState = PlayerState.move;

        if (_playerInput.actions["RightButtonMouse"].triggered)
            RightClick();
        if (cantMoveTime < 0)
        {
            _agent.destination = playerDestination.transform.position;

            qSkillObject.SetActive(false);
            if (qSkillCoolTime <= 0 && (playerState == PlayerState.idle || playerState == PlayerState.move)) 
                if (_playerInput.actions["SkillQ"].triggered)
                    SkillQ();
            _rigidbody.velocity = Vector3.zero;
            if (eSkillCoolTime <= 0 && (playerState == PlayerState.idle || playerState == PlayerState.move))
                if (_playerInput.actions["SkillE"].triggered)
                    SkillE();
            if (playerState == PlayerState.idle || playerState == PlayerState.move)
                if (_playerInput.actions["Dance1"].triggered)
                    Dance1();
            if (playerState == PlayerState.idle || playerState == PlayerState.move)
                if (_playerInput.actions["Dance2"].triggered)
                    Dance2();
        }

        if (cantMoveTime >= 0) cantMoveTime -= Time.deltaTime;
        if (cantMoveTime < 0 && playerState != PlayerState.idle && playerState != PlayerState.move)
            playerState = PlayerState.idle;

        CoolTime();
    }
    void RightClick()
    {
        playerDestination.transform.position = cursorOnDefault.transform.position;
    }
    void SkillQ()
    {
        playerState = PlayerState.skillQ;

        _agent.destination = transform.position;

        qSkillObject.SetActive(true);
        var rigidbody = qSkillObject.GetComponent<Rigidbody>();
        var forwardDirection = cursorOnSerface.transform.position - transform.position;
        rigidbody.velocity = Vector3.zero;
        qSkillObject.transform.position = transform.position;
        qSkillObject.transform.forward = forwardDirection;
        rigidbody.AddForce(forwardDirection.normalized * 40, ForceMode.Impulse);

        cantMoveTime = 0.3f;
        qSkillCoolTime = 0.3f;
    }
    void SkillW()
    {
        playerState = PlayerState.skillW;

        _agent.destination = transform.position;



        cantMoveTime = 3f;
        wSkillCoolTime = 7f;
    }
    void SkillE()
    {
        playerState = PlayerState.skillE;

        var forwardDirection = cursorOnSerface.transform.position - transform.position;
        _rigidbody.AddForce(forwardDirection.normalized * 20, ForceMode.Impulse);

        cantMoveTime = 0.5f;
        eSkillCoolTime = 8f;
    }
    void SkillR()
    {
        playerState = PlayerState.skillR;

        _agent.destination = transform.position;



        cantMoveTime = 3f;
        rSkillCoolTime = 30f;
    }
    void Dance1()
    {
        playerState = PlayerState.dance1;

        _agent.destination = transform.position;

        cantMoveTime = 2f;
    }
    void Dance2()
    {
        playerState = PlayerState.dance2;

        _agent.destination = transform.position;

        cantMoveTime = 4f;
    }

    void CoolTime()
    {
        qSkillCoolTime -= Time.deltaTime;
        wSkillCoolTime -= Time.deltaTime;
        eSkillCoolTime -= Time.deltaTime;
        rSkillCoolTime -= Time.deltaTime;
        if (qSkillCoolTime <= 0) qSkillCoolTime = 0;
        if (wSkillCoolTime <= 0) wSkillCoolTime = 0;
        if (eSkillCoolTime <= 0) eSkillCoolTime = 0;
        if (rSkillCoolTime <= 0) rSkillCoolTime = 0;
    }
}
