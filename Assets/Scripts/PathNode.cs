using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private bool stopDrawing;

    private void Start()
    {
        stopDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is done drawing and we need to disable drawing, disable it
        if (stopDrawing && !DrawPath.instance.IsDrawing())
        {
            SceneManager.instance.SetCanDraw(false);
            stopDrawing = false;
        }
    }

    private void OnMouseEnter()
    {
        // Enable drawing when the cursor enters path node
        SceneManager.instance.SetCanDraw(true);
    }

    private void OnMouseExit()
    {
        // Disable drawing when the cursor exits path node, iff player is not currently drawing
        if (!DrawPath.instance.IsDrawing())
        {
            SceneManager.instance.SetCanDraw(false);
        }
        else
        {
            // Disable drawing once player stops drawing
            stopDrawing = true;
        }
    }
}
