using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GridSelectionManager : MonoBehaviour
{
    [SerializeField] Grid _grid;
    [SerializeField] Camera _mainCam;
    [SerializeField] Transform _selectionObject;
    [SerializeField] MeshRenderer _selectionMeshRenderer;

    [SerializeField] Vector3 _lastPosition;
    [SerializeField] Vector3Int _selectionValue;
    [SerializeField] GameObject _selectedObject;
    [SerializeField] Vector3 mouseOffset;
    [SerializeField] LayerMask placementLayers;

    public Vector3Int GetSelectedCell() => _selectionValue;
    public Transform GetSelectedTransform() => _selectedObject.transform;
    public GameObject GetSelectedGameObject() => _selectedObject;


    [Header("Input")]
    [SerializeField] InputAction selectAction;

    [Header("Colors")]
    [SerializeField] Color StandardColor = Color.white;
    [SerializeField] Color SelectionColor = Color.blue;

    [Header("Events")]
    public UnityEvent OnGridHover;
    public UnityEvent OnGridSelected;
    public UnityEvent OnGridUnselected;


    void Start()
    {
        if (!_mainCam) _mainCam = Camera.main;
        if (!_grid) _grid = FindObjectOfType<Grid>();

        _selectionMeshRenderer = _selectionObject.GetComponentInChildren<MeshRenderer>();

        selectAction.Enable();
        selectAction.started += ctx =>
        {
            _selectionMeshRenderer.material.color = SelectionColor;

            OnGridSelected.Invoke();
        };

        selectAction.canceled += ctx =>
        {
            _selectionMeshRenderer.material.color = StandardColor;

            OnGridUnselected.Invoke();
        };
    }

    Ray ray;

    void Update()
    {
        ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, placementLayers) && !EventSystem.current.IsPointerOverGameObject())
        {
            _lastPosition = hit.point;
            _selectedObject = hit.collider.gameObject;

            _selectionValue = _grid.WorldToCell(_lastPosition + mouseOffset);
            _selectionObject.position = _grid.CellToWorld(_selectionValue);

            if (!_selectionObject.gameObject.activeSelf)
            {
                _selectionObject.gameObject.SetActive(true);
            }

            OnGridHover.Invoke();
        }
        else
        {
            _selectedObject = null;

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

    private void OnValidate()
    {
        if (!_mainCam) _mainCam = Camera.main;
        if (!_grid) _grid = FindObjectOfType<Grid>();
    }
}
