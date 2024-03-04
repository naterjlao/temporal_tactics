using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [Header("References")]
    [Expandable] public GridTileSet TileSet;
    [SerializeField] GameObject _tile;

    [Header("Params")]
    public bool ShouldChangeOnGenerate = true;
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
    public TileType _tilePrefab;
    private DropdownList<TileType> GetTilePrefab()
    {
        return new DropdownList<TileType> {
            { "Standard", TileType.Standard },
            { "Bump", TileType.Bump },
            { "CornerInner", TileType.CornerInner },
            { "CornerLarge", TileType.CornerLarge },
            { "CornerOuter", TileType.CornerOuter },
            { "CornerRound", TileType.CornerRound },
            { "CornerSquare", TileType.CornerSquare },
            { "Crossing", TileType.Crossing },
            { "Crystal", TileType.Crystal },
            { "Dirt", TileType.Dirt },
            { "DirtHigh", TileType.DirtHigh },
            { "End", TileType.End },
            { "EndRound", TileType.EndRound },
            { "EndRoundSpawn", TileType.EndRoundSpawn },
            { "EndSpawn", TileType.EndSpawn },
            { "Hill", TileType.Hill },
            { "RiverBridge", TileType.RiverBridge },
            { "RiverCorner", TileType.RiverCorner },
            { "RiverFall", TileType.RiverFall },
            { "RiverSlope", TileType.RiverSlope },
            { "RiverSlopeLarge", TileType.RiverSlopeLarge },
            { "RiverStraight", TileType.RiverStraight },
            { "RiverTransition", TileType.RiverTransition },
            { "Rock", TileType.Rock },
            { "Slope", TileType.Slope },
            { "Spawn", TileType.Spawn },
            { "SpawnRound", TileType.SpawnRound },
            { "Split", TileType.Split },
            { "Straight", TileType.Straight },
            { "StraightHill", TileType.StraightHill },
            { "StraightHillLarge", TileType.StraightHillLarge },
            { "Transition", TileType.Transition },
            { "Tree", TileType.Tree },
            { "TreeDouble", TileType.TreeDouble },
            { "TreeQuad", TileType.TreeQuad },
            { "wideCorner", TileType.wideCorner },
            { "wideSplit", TileType.wideSplit },
            { "wideStraight", TileType.wideStraight },
            { "wideTransition", TileType.wideTransition },
        };
    }
    private void OnTilePrefabChanged()
    {
        SetTile(TileSet.GetTile(_tilePrefab));
        ShouldChangeOnGenerate = false;
    }
    public void SetTile(TileType tileType)
    {
        SetTile(TileSet.GetTile(tileType));
    }
    // private void OnValidate()
    // {
    //     SetTile(TileSet.GetTile(_tilePrefab));
    // }

    public void SetTile(GameObject newTile)
    {
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
        SetName();
    }

    public void SetName()
    {
        gameObject.name = $"Grid Tile {Coordinates} {_tilePrefab}";
    }

}
