using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] SplineComputer _spline;
    [SerializeField] List<Transform> _pathTiles;

    private void Awake()
    {
        if (!_spline) _spline = GetComponent<SplineComputer>();
    }

    [Button]
    public void CreatePath()
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

    public void AddTileToPath(GridTile tile, bool shouldRebuild = false)
    {
        if (tile == null) return;

        var pointsToAdd = new List<SplinePoint>();

        if (tile.AltPoints != null && tile.AltPoints.Count > 0)
        {
            //* Make multiple points
            foreach (var point in tile.AltPoints)
            {
                pointsToAdd.Add(NewPoint(point.transform.position));
            }
        }
        else
        {
            //* just make one point
            pointsToAdd.Add(NewPoint(tile.transform.position));
        }

        _pathTiles.Add(tile.transform);

        var points = _spline.GetPoints().ToList();
        points.AddRange(pointsToAdd);

        _spline.SetPoints(points.ToArray());
        if (shouldRebuild) _spline.Rebuild();
    }

    public void RemoveLastTile(bool shouldRebuild = false)
    {
        var lastTile = _pathTiles.Last();
        var pointCount = lastTile.GetComponent<GridTile>().AltPoints.Count;

        var points = _spline.GetPoints().ToList();
        for (int i = 0; i < pointCount; i++)
        {
            points.RemoveAt(points.Count - i);
        }

        _spline.SetPoints(points.ToArray());
        if (shouldRebuild) _spline.Rebuild();
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
