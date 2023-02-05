using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomNode : Node
{
    private SpriteRenderer nodeSprite;
    private Animator animator;
    public Fairy fairy;
    public bool isStem = false;

    private void Start()
    {

        animator = GetComponentInChildren<Animator>();
        nodeSprite = this.GetComponent<SpriteRenderer>();
    }


    private void OnMouseDown()
    {
        SceneManager.instance.ActivateNode(this);
    }

    public override void Activate()
    {
        createdObject.SetActive(true);
        nodeSprite.enabled = false;
        ActivateSelf();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).gameObject.GetComponent<MushroomNode>()?.ActivateSelf();
        }
    }

    public override void Deactivate()
    {
        DeactivateSelf();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).gameObject.GetComponent<MushroomNode>()?.DeactivateSelf();
        }
    }

    private void ActivateSelf()
    {
        createdObject.SetActive(true);
    }

    private void DeactivateSelf()
    {
        createdObject.SetActive(false);
        nodeSprite.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null){
            animator.SetTrigger("bounce");
        }
    }

    private void OnMouseOver()
    {
        fairy.Hover();
    }

    private void OnMouseExit()
    {
        fairy.DeHover();
    }
}
