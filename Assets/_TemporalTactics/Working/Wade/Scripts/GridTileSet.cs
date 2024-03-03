using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Tile Set", menuName = "New Grid Tile Set", order = 100)]
public class GridTileSet : ScriptableObject
{
    public GridTile Base;
    public GameObject Standard;
    public GameObject AltPrefab;

    public GameObject GetTile(TileType tileName)
    {
        switch (tileName)
        {
            case TileType.Standard:
                return Standard;

            case TileType.Alt:
                return AltPrefab;


            default:
                return Standard;
        }
    }
}

public enum TileType
{
    Standard,
    Alt
}
