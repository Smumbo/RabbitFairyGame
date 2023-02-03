using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrawing : MonoBehaviour
{
    public Material lineMaterial;
    public float lineWidth = 0.1f;
    private List<LineRenderer> lines;
    private List<EdgeCollider2D> edgeColliders;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Vector3 mousePos;
    private Vector2[] edgePoints;

    void Start()
    {
        lines = new List<LineRenderer>();
        edgeColliders = new List<EdgeCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.parent = transform;
            lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 0;
            lines.Add(lineRenderer);
        }
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject edgeObject = new GameObject("Edge");
            edgeObject.transform.parent = transform;
            edgeCollider = edgeObject.AddComponent<EdgeCollider2D>();
            edgeColliders.Add(edgeCollider);

            edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                edgePoints[i] = lineRenderer.GetPosition(i);
            }
            edgeCollider.points = edgePoints;
        }
    }
}
