using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GridTileSet _tileSet;
    [SerializeField] GameObject _tile;

    [Header("Params")]
    public bool shouldChangeOnGenerate = true;
    public Vector3Int Coordinates;

    //* -----------------------------------------------------------------------------
    [Dropdown("GetDirectionNames"), OnValueChanged("OnRotationDirectionChanged")]
    public Vector3 rotationDirection;
    private DropdownList<Vector3> GetDirectionNames()
    {
        return new DropdownList<Vector3> {
            { "Forward",      new Vector3(0f, 0f, 0f) },
            { "Right",   new Vector3(0f, 90f, 0f) },
            { "Backward",    new Vector3(0f, 180f, 0f) },
            { "Left",    new Vector3(0f, 270f, 0f) },
        };
    }
    private void OnRotationDirectionChanged()
    {
        transform.localEulerAngles = rotationDirection;
    }

    //* -----------------------------------------------------------------------------

    [Dropdown("GetTilePrefab"), OnValueChanged("OnTilePrefabChanged")]
    public TilePrefab _tilePrefab;
    private DropdownList<TilePrefab> GetTilePrefab()
    {
        return new DropdownList<TilePrefab> {
            { "Standard", TilePrefab.Standard },
            { "Alt",  TilePrefab.Alt },
        };
    }
    private void OnTilePrefabChanged()
    {
        SetTile(_tileSet.GetTile(_tilePrefab));
    }
    public void SetTile(GameObject newTile)
    {
        // if (!shouldChangeOnGenerate) return;

        if (_tile)
        {
            if (Application.isPlaying)
            {
                Destroy(_tile);
            }
            else
            {
                DestroyImmediate(_tile);
            }
        }

        _tile = Instantiate(newTile, transform);
        _tile.name = _tilePrefab.ToString();
    }

    public void SetName()
    {
        gameObject.name = $"Grid Tile {Coordinates} {_tilePrefab}";
    }
}
