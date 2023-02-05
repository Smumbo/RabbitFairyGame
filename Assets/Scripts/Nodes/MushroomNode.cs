using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomNode : Node
{
    private SpriteRenderer nodeSprite;
    private Animator animator;

    private void OnMouseDown()
    {
        SceneManager.instance.ActivateNode(this);
        nodeSprite = this.GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void Activate()
    {
        createdObject.SetActive(true);
        nodeSprite.enabled = false;
    }

    public override void Deactivate()
    {
        createdObject.SetActive(false);
        nodeSprite.enabled = true;
    }
}
