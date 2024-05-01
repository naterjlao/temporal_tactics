using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GridTile : MonoBehaviour
{
    [Header("References")]
    [Expandable] public GridTileSet TileSet;
    [SerializeField] GameObject _tile;
    [SerializeField] public List<PathAltPoint> AltPoints;

    [Header("Params")]
    public bool ShouldChangeOnGenerate = true;
    // public Vector3Int Coordinates;
    public AlignWithGrid GridAlign;

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
    [SerializeField] TileType _tilePrefab;
    public TileType TilePrefab => _tilePrefab;

    private DropdownList<TileType> GetTilePrefab()
    {
        return new DropdownList<TileType> {
            { "Standard", TileType.Standard },
            { "None", TileType.None },
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
    public void SetTile(TileType tileType, bool shouldChangeOnGenerate = false)
    {
        SetTile(TileSet.GetTile(tileType));

        if (tileType != TileType.Standard)
        {
            ShouldChangeOnGenerate = true;
        }
        else
        {
            ShouldChangeOnGenerate = shouldChangeOnGenerate;
        }
    }

    public void SetTile(GameObject newTile)
    {
#if UNITY_EDITOR
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

        // _tile = Instantiate(newTile, transform);
        _tile = PrefabUtility.InstantiatePrefab(newTile) as GameObject;
        SetName();

        if (!_tile) return;

        _tile.transform.parent = transform;
        _tile.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        AltPoints = _tile.GetComponentsInChildren<PathAltPoint>().ToList();
#endif
    }

    public void SetName()
    {
        if (!GridAlign) GridAlign = GetComponent<AlignWithGrid>();

        gameObject.name = $"Grid Tile {GridAlign?.GridLocation} {_tilePrefab}";
    }

}
