using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomNode : Node
{
    private void OnMouseDown()
    {
        SceneManager.instance.ActivateNode(this);
    }

    public override void Activate()
    {
        createdObject.SetActive(true);
    }

    public override void Deactivate()
    {
        createdObject.SetActive(false);
    }
}
