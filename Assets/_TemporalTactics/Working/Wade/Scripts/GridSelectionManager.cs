using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridSelectionManager : MonoBehaviour
{
    [SerializeField] Grid _grid;
    [SerializeField] Camera _mainCam;
    [SerializeField] Transform _selectionObject;
    [SerializeField] MeshRenderer _selectionMeshRenderer;

    [SerializeField] Vector3 _lastPosition;
    [SerializeField] Vector3Int _selectionValue;
    [SerializeField] Vector3 mouseOffset;
    [SerializeField] LayerMask placementLayers;

    [Header("Input")]
    [SerializeField] InputAction selectAction;

    [Header("Colors")]
    [SerializeField] Color StandardColor = Color.white;
    [SerializeField] Color SelectionColor = Color.blue;

    void Start()
    {
        if (!_mainCam) _mainCam = Camera.main;

        _selectionMeshRenderer = _selectionObject.GetComponentInChildren<MeshRenderer>();

        selectAction.Enable();
        selectAction.started += ctx =>
        {
            print($"Select {_selectionValue}");
            // _selectionMeshRenderer.material.SetColor("Color Bottom", SelectionColor);
            _selectionMeshRenderer.material.color = SelectionColor;
        };

        selectAction.canceled += ctx =>
        {
            _selectionMeshRenderer.material.color = StandardColor;
            // _selectionMeshRenderer.material.SetColor("Color Bottom", StandardColor);
        };
    }

    Ray ray;

    void Update()
    {
        ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _lastPosition = hit.point;
            _selectionValue = _grid.WorldToCell(_lastPosition + mouseOffset);
            _selectionObject.position = _grid.CellToWorld(_selectionValue);

            if (!_selectionObject.gameObject.activeSelf)
            {
                _selectionObject.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_selectionObject.gameObject.activeSelf)
            {
                _selectionObject.gameObject.SetActive(false);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo line along the ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f); // Change the magnitude as needed
    }
}
