using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] GridSelectionManager selectionManager;
    [SerializeField] PathCreator pathCreator;
    [SerializeField] PathLine pathLine;

    [SerializeField] GridTileSet tileSet;
    [SerializeField] public TileType SelectedTileType;

    [SerializeField] GridBuilderUI gridBuilderUI;

    [SerializeField] bool ResetMode;
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
            ResetMode = !ResetMode;
            gridBuilderUI.resetToggle.isOn = ResetMode;
        };

        ToggleCreatePathMode.started += ctx =>
        {
            TogglePathMode(!CreatePathMode);
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
            SelectedTileType++;
        };

        IterateTileTypeUp.started += ctx =>
        {
            SelectedTileType--;
        };

        selectionManager.OnGridHover.AddListener(() =>
        {
            if (ResetMode && Input.GetMouseButton(0))
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
            pathLine.DrawPath();
        }
        else
        {
            selectedTile?.SetTile(SelectedTileType, true);
        }
    }

    private void OnValidate()
    {
        if (!selectionManager) FindObjectOfType<GridSelectionManager>();
        if (!pathCreator) FindObjectOfType<PathCreator>();
    }

    public void ButtonToggleResetMode(bool value)
    {
        ResetMode = value;
    }
    public void TogglePathMode(bool value)
    {
        CreatePathMode = value;

        if (CreatePathMode)
        {
            pathLine.DrawPath();
            RemoveFromPathButton.Enable();
        }
        else
        {
            pathLine.HidePath();
            RemoveFromPathButton.Disable();
        }
    }

    public void RefreshPathUI()
    {
        pathCreator.CreatePath();
    }

    public void RemoveLastTileFromPath()
    {
        pathCreator.RemoveLastTile(true);
    }
}
