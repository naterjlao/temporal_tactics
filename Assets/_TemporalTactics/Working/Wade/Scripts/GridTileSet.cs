using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Tile Set", menuName = "New Grid Tile Set", order = 100)]
public class GridTileSet : ScriptableObject
{
    public GridTile Base;
    public GameObject Standard;
    public GameObject AltPrefab;

    public GameObject GetTile(TilePrefab tileName)
    {
        switch (tileName)
        {
            case TilePrefab.Standard:
                return Standard;

            case TilePrefab.Alt:
                return AltPrefab;


            default:
                return Standard;
        }
    }
}

public enum TilePrefab
{
    Standard,
    Alt
}
