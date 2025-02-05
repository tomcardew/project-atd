using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LineController : MonoBehaviour
{
    // Public variables
    public Vector3 start;
    public Vector3 end;
    public float lineWidth = 2f;
    public Color lineColor = Color.white;

    // Private variables
    private LineRenderer lineRenderer;
    private List<Vector2> points;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        GetPoints();
        DrawPath();
    }

    private void GetPoints()
    {
        points = new List<Vector2>();
        points.Add(start);
        points.Add(end);
    }

    private void DrawPath()
    {
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}
