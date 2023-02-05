using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomNode2 : Node
{
    private SpriteRenderer nodeSprite;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        nodeSprite = this.GetComponent<SpriteRenderer>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        SceneManager.instance.ActivateNode(this);
        if(isActive){
            isActive = false;
            this.Deactivate();
        }
        else{
            isActive = true;
            this.Activate();
        }
    }

    public override void Activate()
    {
        nodeSprite.enabled = false;
        createdObject.SetActive(true);
    }

    public override void Deactivate()
    {
        nodeSprite.enabled = true;
        createdObject.SetActive(false);
    }
}
