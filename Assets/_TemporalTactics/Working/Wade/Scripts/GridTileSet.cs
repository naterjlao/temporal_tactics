using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Tile Set", menuName = "New Grid Tile Set", order = 100)]
public class GridTileSet : ScriptableObject
{
    public GridTile Base;
    public GameObject Standard;
    public GameObject Bump;
    public GameObject CornerInner;
    public GameObject CornerLarge;
    public GameObject CornerOuter;
    public GameObject CornerRound;
    public GameObject CornerSquare;
    public GameObject Crossing;
    public GameObject Crystal;
    public GameObject Dirt;
    public GameObject DirtHigh;
    public GameObject End;
    public GameObject EndRound;
    public GameObject EndRoundSpawn;
    public GameObject EndSpawn;
    public GameObject Hill;
    public GameObject RiverBridge;
    public GameObject RiverCorner;
    public GameObject RiverFall;
    public GameObject RiverSlope;
    public GameObject RiverSlopeLarge;
    public GameObject RiverStraight;
    public GameObject RiverTransition;
    public GameObject Rock;
    public GameObject Slope;
    public GameObject Spawn;
    public GameObject SpawnRound;
    public GameObject Split;
    public GameObject Straight;
    public GameObject StraightHill;
    public GameObject StraightHillLarge;
    public GameObject Transition;
    public GameObject Tree;
    public GameObject TreeDouble;
    public GameObject TreeQuad;
    public GameObject wideCorner;
    public GameObject wideSplit;
    public GameObject wideStraight;
    public GameObject wideTransition;

    public GameObject GetTile(TileType tileName)
    {
        switch (tileName)
        {
            case TileType.Standard:
                return Standard;
            case TileType.Bump:
                return Bump;
            case TileType.CornerInner:
                return CornerInner;
            case TileType.CornerLarge:
                return CornerLarge;
            case TileType.CornerOuter:
                return CornerOuter;
            case TileType.CornerRound:
                return CornerRound;
            case TileType.CornerSquare:
                return CornerSquare;
            case TileType.Crossing:
                return Crossing;
            case TileType.Crystal:
                return Crystal;
            case TileType.Dirt:
                return Dirt;
            case TileType.DirtHigh:
                return DirtHigh;
            case TileType.End:
                return End;
            case TileType.EndRound:
                return EndRound;
            case TileType.EndRoundSpawn:
                return EndRoundSpawn;
            case TileType.EndSpawn:
                return EndSpawn;
            case TileType.Hill:
                return Hill;
            case TileType.RiverBridge:
                return RiverBridge;
            case TileType.RiverCorner:
                return RiverCorner;
            case TileType.RiverFall:
                return RiverFall;
            case TileType.RiverSlope:
                return RiverSlope;
            case TileType.RiverSlopeLarge:
                return RiverSlopeLarge;
            case TileType.RiverStraight:
                return RiverStraight;
            case TileType.RiverTransition:
                return RiverTransition;
            case TileType.Rock:
                return Rock;
            case TileType.Slope:
                return Slope;
            case TileType.Spawn:
                return Spawn;
            case TileType.SpawnRound:
                return SpawnRound;
            case TileType.Split:
                return Split;
            case TileType.Straight:
                return Straight;
            case TileType.StraightHill:
                return StraightHill;
            case TileType.StraightHillLarge:
                return StraightHillLarge;
            case TileType.Transition:
                return Transition;
            case TileType.Tree:
                return Tree;
            case TileType.TreeDouble:
                return TreeDouble;
            case TileType.TreeQuad:
                return TreeQuad;
            case TileType.wideCorner:
                return wideCorner;
            case TileType.wideSplit:
                return wideSplit;
            case TileType.wideStraight:
                return wideStraight;
            case TileType.wideTransition:
                return wideTransition;


            default:
                return Standard;
        }
    }
}

public enum TileType
{
    Standard,
    Bump,
    CornerInner,
    CornerLarge,
    CornerOuter,
    CornerRound,
    CornerSquare,
    Crossing,
    Crystal,
    Dirt,
    DirtHigh,
    End,
    EndRound,
    EndRoundSpawn,
    EndSpawn,
    Hill,
    RiverBridge,
    RiverCorner,
    RiverFall,
    RiverSlope,
    RiverSlopeLarge,
    RiverStraight,
    RiverTransition,
    Rock,
    Slope,
    Spawn,
    SpawnRound,
    Split,
    Straight,
    StraightHill,
    StraightHillLarge,
    Transition,
    Tree,
    TreeDouble,
    TreeQuad,
    wideCorner,
    wideSplit,
    wideStraight,
    wideTransition
}
