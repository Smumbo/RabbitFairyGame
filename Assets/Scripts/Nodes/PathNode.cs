using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathNode : Node
{
    private bool disableDrawing;
    private AudioSource sfx;

    private void Start()
    {
        disableDrawing = false;
        sfx = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DrawPath.instance.IsCanceled())
        {
            disableDrawing = false;
            return;
        }
        // If player is done drawing and we need to disable drawing,
        // disable it and activate the path belonging to this node
        if (disableDrawing && !DrawPath.instance.IsDrawing() && DrawPath.instance.IsFinished())
        {
            SceneManager.instance.SetCanDraw(false);
            disableDrawing = false;
            SceneManager.instance.ActivateNode(this);
        }

        if(DrawPath.instance.IsDrawing()){
            sfx.Play();
        }
        else{
            sfx.Stop();
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
            disableDrawing = true;
        }
    }

    public override void Activate()
    {
        createdObject = DrawPath.instance.GetCurrPath();
        EdgeCollider2D collider = createdObject.GetComponent<EdgeCollider2D>();
        collider.enabled = true;
    }

    public override void Deactivate()
    {
        Destroy(createdObject);
    }
}
