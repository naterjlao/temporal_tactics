using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = System.Random;

[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] Grid _grid;

    [Header("Params")]
    [SerializeField] public Vector2Int Size;
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
        for (int x = 0; x < Size.x; x++)
        {
            for (int z = 0; z < Size.y; z++)
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
        newTile.TileSet = TileSet;
        newTile.SetTile(TileType.Standard);

        _gridTiles.Add(newTile);
    }
}