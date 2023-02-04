using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    // public variables
    public Material pathMaterial;
    public float lineWidth = 0.1f;
    public float maxLength = 10f;

    // current and previous path
    private GameObject currPath;
    private GameObject lastPath = null;

    // current renderer and collider being drawn
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    // current and previous mouse position
    private Vector3 mousePos;
    private Vector3 lastMousePos;

    // keep track of the current line length
    private float currentLength;

    public static DrawPath instance;
    private bool isDrawing;

    public DrawPath()
    {
        instance = this;
    }

    void Update()
    {
        // only draw if player is hovering on a node 
        if (!SceneManager.instance.GetCanDraw())
        {
            return;
        }

        // mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;

            // start a new line
            currentLength = 0f;
            currPath = new GameObject("Path");
            currPath.transform.parent = transform;

            // create line renderer
            lineRenderer = currPath.AddComponent<LineRenderer>();
            lineRenderer.material = pathMaterial;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 0;

            // create collider
            edgeCollider = currPath.AddComponent<EdgeCollider2D>();
            edgeCollider.enabled = false;

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

            // only continue drawing if below max length
            if (currentLength <= maxLength)
            {
                // add to line renderer
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
            }

            // update mouse position
            lastMousePos = mousePos;
        }
        // mouse released
        if (Input.GetMouseButtonUp(0))
        {
            // If there is a previous path, destroy it and make the new one
            if (lastPath != null)
            {
                Destroy(lastPath);
            }
            lastPath = currPath;

            // generate collider points from line renderer
            Vector2[] edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                edgePoints[i] = lineRenderer.GetPosition(i);
            }
            edgeCollider.points = edgePoints;
            edgeCollider.enabled = true;

            isDrawing = false;
        }
    }

    public bool IsDrawing()
    {
        return isDrawing;
    }
}

