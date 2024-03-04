using System.Collections;
using System.Collections.Generic;
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
        var points = new SplinePoint[_pathTiles.Length];

        for (int i = 0; i < _pathTiles.Length; i++)
        {
            points[i] = new SplinePoint();
            points[i].position = _pathTiles[i].position;
            points[i].normal = Vector3.up;
            points[i].size = 1f;
            points[i].color = Color.white;
        }

        _spline.SetPoints(points);
    }
}
