using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrawing : MonoBehaviour
{
    // public variables
    public Material lineMaterial;
    public float lineWidth = 0.1f;
    public float maxLength = 10f;

    // lists to store the renderer and collider for the paths
    private List<LineRenderer> lines;
    private List<EdgeCollider2D> edgeColliders;

    // current renderer and collider being drawn
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private Vector2[] edgePoints;

    // current and previous mouse position
    private Vector3 mousePos;
    private Vector3 lastMousePos;

    // keep track of the current line length
    private float currentLength;

    void Start()
    {
        lines = new List<LineRenderer>();
        edgeColliders = new List<EdgeCollider2D>();
    }

    void Update()
    {
        // mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            // start a new line
            currentLength = 0f;
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.parent = transform;

            // create a line renderer
            lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 0;
            lines.Add(lineRenderer);

            // calculate mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            lastMousePos = mousePos;
        }
        // mouse held
        if (Input.GetMouseButton(0))
        {
            // update mouse position
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;

            // add to length
            currentLength += Vector3.Distance(mousePos, lastMousePos);

            // only continue if below max length
            if (currentLength <= maxLength)
            {
                // add to line renderer
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
                lastMousePos = mousePos;
            }
        }
        // mouse released
        if (Input.GetMouseButtonUp(0))
        {
            // create collider
            GameObject edgeObject = new GameObject("Edge");
            edgeObject.transform.parent = transform;
            edgeCollider = edgeObject.AddComponent<EdgeCollider2D>();
            edgeColliders.Add(edgeCollider);

            // generate collider points using line renderer
            edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                edgePoints[i] = lineRenderer.GetPosition(i);
            }
            edgeCollider.points = edgePoints;
        }
    }
}
