using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] SplineComputer _spline;
    [SerializeField] Transform[] _pathTiles;

    private void Awake()
    {
        if (!_spline) _spline = GetComponent<SplineComputer>();
    }

    [Button]
    void CreatePath()
    {
        var points = new List<SplinePoint>();

        foreach (var tile in _pathTiles)
        {
            var gridTile = tile.GetComponent<GridTile>();

            if (gridTile.AltPoints != null && gridTile.AltPoints.Count > 0)
            {
                //* Make multiple points
                foreach (var point in gridTile.AltPoints)
                {
                    points.Add(NewPoint(point.transform.position));
                }
            }
            else
            {
                //* just make one point
                points.Add(NewPoint(tile.transform.position));
            }
        }

        _spline.SetPoints(points.ToArray());
    }

    SplinePoint NewPoint(Vector3 position)
    {
        var newPoint = new SplinePoint
        {
            normal = Vector3.up,
            size = 0.5f,
            color = Color.white,
            position = position
        };

        return newPoint;
    }
}
