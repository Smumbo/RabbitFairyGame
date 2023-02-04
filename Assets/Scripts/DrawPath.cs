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
    private bool cancel;
    private bool finished;

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
            cancel = false;
            finished = false;

            // start a new path
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
        if (Input.GetMouseButton(0) && isDrawing)
        {
            // if player right clicks while drawing, cancel
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(currPath);
                isDrawing = false;
                cancel = true;
                return;
            }

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

                // update mouse position
                lastMousePos = mousePos;
            }
        }
        // mouse released
        if (Input.GetMouseButtonUp(0))
        {
            if (cancel)
            {
                cancel = false;
                return;
            }

            // generate collider points from line renderer
            Vector2[] edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                edgePoints[i] = lineRenderer.GetPosition(i);
            }
            edgeCollider.points = edgePoints;

            isDrawing = false;
            finished = true;
        }
    }

    public GameObject GetCurrPath()
    {
        return currPath;
    }

    public bool IsDrawing()
    {
        return isDrawing;
    }

    public bool IsCanceled()
    {
        return cancel;
    }

    public bool IsFinished()
    {
        return finished;
    }
    
}

