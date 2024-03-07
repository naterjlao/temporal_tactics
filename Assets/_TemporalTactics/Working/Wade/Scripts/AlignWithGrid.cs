using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AlignWithGrid : MonoBehaviour
{
    [SerializeField] public Vector3Int GridLocation;
    [SerializeField] public Grid grid;

    void OnValidate()
    {
        if (!grid) grid = FindObjectOfType<Grid>();

        if (grid) transform.position = grid.CellToWorld(GridLocation);
    }
}
