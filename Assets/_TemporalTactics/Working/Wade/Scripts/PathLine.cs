using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

public class PathLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] SplineComputer spline;

    public int numBetweenSplinePoints = 16; // Number of points to generate on the curve
    [SerializeField] float yOffset = 0.1f;
    int numberOfPoints = 0; // Adjust this value as needed

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void HidePath()
    {
        lineRenderer.positionCount = 0;
    }

    [Button]
    public void DrawPath()
    {
        if (spline == null || lineRenderer == null)
            return;

        var pointsCount = spline.GetPoints().Length;

        if (pointsCount == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }
        else if (pointsCount == 1)
        {
            lineRenderer.positionCount = 2;
            var point = spline.GetPoint(0);
            lineRenderer.SetPositions(new Vector3[] { point.position, point.position + Vector3.forward });
        }
        else
        {
            numberOfPoints = (pointsCount - 1) * numBetweenSplinePoints;

            lineRenderer.positionCount = numberOfPoints;
            for (int i = 0; i < numberOfPoints; i++)
            {
                float percentage = (float)i / (numberOfPoints - 1);
                var splineSample = spline.Evaluate(percentage).position;
                splineSample.y += yOffset;

                lineRenderer.SetPosition(i, splineSample);
            }
        }

    }
    private void OnValidate()
    {
        if (!spline) FindObjectOfType<SplineComputer>();
    }
}
