using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveGrid : MonoBehaviour
{
    [SerializeField] GridManager _gridManager;
    [SerializeField] PathCreator _pathCreator;

    [Button]
    void SaveGridToJson()
    {
        string FILE_NAME = "GridSave.json";
        string PATH = $"{Application.persistentDataPath}/{FILE_NAME}";

        GridSave newSave = new GridSave(_gridManager.Size);

        foreach (var tile in _gridManager.GridTiles)
        {
            newSave.tiles.Add(new TileSave(tile.GridAlign.GridLocation, tile.TilePrefab, tile.rotationDirection));
        }

        foreach (var tile in _pathCreator.PathTiles)
        {
            newSave.path.Add(new PathTileSave(tile.GetComponent<AlignWithGrid>().GridLocation));
        }

        var json = JsonUtility.ToJson(newSave);
        Debug.Log($"Saving Grid To: {PATH}");

        File.WriteAllText(PATH, json);
    }
}

class GridSave
{
    public Vector2 size;
    public List<TileSave> tiles;
    public List<PathTileSave> path;

    public GridSave(Vector2 size)
    {
        this.size = size;

        tiles = new List<TileSave>();
        path = new List<PathTileSave>();
    }
}

[System.Serializable]

struct TileSave
{
    public Vector3Int gridCoord;
    public TileType tileType;
    public Vector3 direction;

    public TileSave(Vector3Int gridCoord, TileType tileType, Vector3 direction)
    {
        this.gridCoord = gridCoord;
        this.tileType = tileType;
        this.direction = direction;
    }
}

[System.Serializable]
struct PathTileSave
{
    public Vector3Int gridCoord;

    public PathTileSave(Vector3Int gridCoord)
    {
        this.gridCoord = gridCoord;
    }
}