using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] GridSelectionManager selectionManager;
    [SerializeField] PathCreator pathCreator;

    [SerializeField] GridTileSet tileSet;
    [SerializeField] TileType selectedTileType;

    [SerializeField] bool EraseMode;
    [Space]
    public InputAction ToggleEraseMode;


    [Header("Create Path Mode")]
    public bool CreatePathMode = false;

    [Space]
    public InputAction ToggleCreatePathMode;
    public InputAction RemoveFromPathButton;
    public InputAction RefreshPath;
    public InputAction RotateClockwise;
    public InputAction RotateCounterClockwise;
    public InputAction IterateTileTypeUp;
    public InputAction IterateTileTypeDown;

    void Start()
    {
        if (!selectionManager) FindObjectOfType<GridSelectionManager>();
        if (!pathCreator) FindObjectOfType<PathCreator>();

        selectionManager.OnGridSelected.AddListener(GridSelected);

        ToggleEraseMode.Enable();
        ToggleCreatePathMode.Enable();
        RefreshPath.Enable();
        RotateClockwise.Enable();
        RotateCounterClockwise.Enable();
        IterateTileTypeUp.Enable();
        IterateTileTypeDown.Enable();

        ToggleEraseMode.started += ctx =>
        {
            EraseMode = !EraseMode;
        };

        ToggleCreatePathMode.started += ctx =>
        {
            CreatePathMode = !CreatePathMode;

            if (CreatePathMode)
            {
                RemoveFromPathButton.Enable();
            }
            else
            {
                RemoveFromPathButton.Disable();
            }
        };

        RemoveFromPathButton.started += ctx =>
        {
            pathCreator.RemoveLastTile(true);
        };

        RefreshPath.started += ctx =>
        {
            pathCreator.CreatePath();
        };

        RotateClockwise.started += ctx =>
        {
            selectionManager.GetSelectedGameObject().GetComponentInParent<GridTile>().transform.Rotate(Vector3.up, 90);
        };

        RotateCounterClockwise.started += ctx =>
        {
            selectionManager.GetSelectedGameObject().GetComponentInParent<GridTile>().transform.Rotate(Vector3.up, -90);
        };

        IterateTileTypeUp.started += ctx =>
        {
            selectedTileType++;
        };

        IterateTileTypeUp.started += ctx =>
        {
            selectedTileType--;
        };

        selectionManager.OnGridHover.AddListener(() =>
        {
            if (EraseMode && Input.GetMouseButton(0))
            {
                var selectedTile = selectionManager.GetSelectedGameObject()?.GetComponentInParent<GridTile>();

                selectedTile?.SetTile(TileType.Standard, true);
            }
        });
    }

    void GridSelected()
    {
        var selectedTile = selectionManager.GetSelectedGameObject()?.GetComponentInParent<GridTile>();

        if (CreatePathMode)
        {
            pathCreator.AddTileToPath(selectedTile, true);
        }
        else
        {
            selectedTile?.SetTile(selectedTileType, true);
        }
    }

    private void OnValidate()
    {
        if (!selectionManager) FindObjectOfType<GridSelectionManager>();
        if (!pathCreator) FindObjectOfType<PathCreator>();
    }
}
