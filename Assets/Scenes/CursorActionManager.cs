using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CursorActionManager : MonoBehaviour
{
    public GameObject cSerface;
    public GameObject cDefault;
    public GameObject destination;
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
            Move();
        if (_playerInput.actions["Q"].triggered)
            WhenQ();
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
        if (NavMesh.SamplePosition(cSerface.transform.position, out hit, 9999f,defaultLayer))
        {
            cDefault.transform.position = hit.position;
        }
    }
    void Move()
    {
        destination.transform.position = cDefault.transform.position;
    }
    void WhenQ()
    {
        Debug.Log("Q");
    }
}

