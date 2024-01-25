using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CursorActionManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cSerface;
    public GameObject cDefault;
    public GameObject playerDestination;
    public enum CursorState
    {
        idle,
        Q,
        W
    }
    public CursorState cState = CursorState.idle;
    public bool isCastImmediate = false;
    public GameObject qSkillPrefab;
    public GameObject wSkillPrefab;

    private PlayerInput _playerInput;
    private Camera _camera;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCSerface();
        MoveCDefault();
        if (_playerInput.actions["RightButtonMouse"].triggered)
            RightClick();
        if (_playerInput.actions["LeftButtonMouse"].triggered)
            LeftClick();
        if (_playerInput.actions["Q"].triggered)
            cState = CursorState.Q;
        if (_playerInput.actions["W"].triggered)
            cState = CursorState.W;
        if (isCastImmediate)
        {
            if (cState == CursorState.Q) SkillQ();
            if (cState == CursorState.W) SkillW();
        }
        DrawSkillArea();
    }

    void MoveCSerface()
    {
        RaycastHit hit;
        var defaultLayer = 1 << LayerMask.NameToLayer("Default");
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 9999f, defaultLayer))
            cSerface.transform.position = hit.point;
    }
    void MoveCDefault()
    {
        NavMeshHit hit;
        var defaultLayer = 1 << LayerMask.NameToLayer("Default");
        if (NavMesh.SamplePosition(cSerface.transform.position, out hit, 9999f, defaultLayer))
        {
            cDefault.transform.position = hit.position;
        }
    }
    void RightClick()
    {
        playerDestination.transform.position = cDefault.transform.position;
        cState = CursorState.idle;
    }
    void LeftClick()
    {
        if (cState == CursorState.Q)
            SkillQ();
        else if (cState == CursorState.W)
            SkillW();
    }

    void SkillQ()
    {


        playerDestination.transform.position = player.transform.position;
        cState = CursorState.idle;
    }
    void SkillW()
    {
        cState = CursorState.idle;
    }

    void DrawSkillArea()
    {
        if (cState == CursorState.idle)
        {
            qSkillPrefab.gameObject.SetActive(false);
            wSkillPrefab.gameObject.SetActive(false);
        }
        if (cState == CursorState.Q)
        {
            qSkillPrefab.gameObject.SetActive(true);
            Vector3 _direction = (cDefault.transform.position - player.transform.position);
            _direction.y = 0;
            _direction = _direction.normalized;
            qSkillPrefab.transform.position = _direction * 2 + player.transform.position;
            qSkillPrefab.transform.rotation = Quaternion.LookRotation(_direction);
        }
        if (cState == CursorState.W)
        {
            wSkillPrefab.gameObject.SetActive(true);
            wSkillPrefab.transform.position = cDefault.transform.position;
        }
    }

}

