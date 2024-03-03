using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;
using UnityEditor;
using Unity.VisualScripting;
using System.Drawing.Drawing2D;

[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] Grid _grid;

    [Header("Params")]
    [SerializeField] private Vector2Int _size;
    [Expandable] public GridTileSet TileSet;
    [SerializeField] List<GridTile> _gridTiles;

    public static GridManager Instance;

    private void Awake()
    {
        if (!_grid) _grid = GetComponent<Grid>();

        Instance = this;
    }

    [Button]
    private void GenerateGrid()
    {
        //* Remove tiles
        foreach (var tile in _gridTiles)
        {
            if (tile.shouldChangeOnGenerate)
            {
                if (Application.isPlaying)
                {
                    Destroy(tile.gameObject);
                }
                else
                {
                    DestroyImmediate(tile.gameObject);
                }
            }
        }
        // _gridTiles.Clear();
        _gridTiles.RemoveAll(t => t == null);

        //* Generate New Tiles
        var coordinates = new List<Vector3Int>();

        // Calculate all coords for grid
        for (int x = 0; x < _size.x; x++)
        {
            for (int z = 0; z < _size.y; z++)
            {
                coordinates.Add(new Vector3Int(x, 0, z));
            }
        }

        //* For the tiles we kept, remove them from our new list of coords to generate at
        foreach (var tile in _gridTiles)
        {
            coordinates.Remove(tile.Coordinates);
        }

        foreach (var coord in coordinates)
        {
            CreateNewTile(coord);
        }
    }

    private void CreateNewTile(Vector3Int coord)
    {
        var newTile = Instantiate(TileSet.Base, transform);
        newTile.transform.position = _grid.CellToWorld(coord);
        newTile.Coordinates = coord;
        newTile.SetName();

        _gridTiles.Add(newTile);
    }
}