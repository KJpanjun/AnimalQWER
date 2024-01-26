using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CursorActionManager : MonoBehaviour
{
    public GameObject cursorOnSerface;
    public GameObject cursorOnDefault;

    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCSerface();
        MoveCDefault();
    }

    void MoveCSerface()
    {
        RaycastHit hit;
        var defaultLayer = 1 << LayerMask.NameToLayer("Default");
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 9999f, defaultLayer))
            cursorOnSerface.transform.position = hit.point;
    }
    void MoveCDefault()
    {
        NavMeshHit hit;
        var defaultLayer = 1 << LayerMask.NameToLayer("Default");
        if (NavMesh.SamplePosition(cursorOnSerface.transform.position, out hit, 9999f, defaultLayer))
        {
            cursorOnDefault.transform.position = hit.position;
        }
    }
}

