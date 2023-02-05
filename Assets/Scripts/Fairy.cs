using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    private Animator animator;
    private Color originalColor;
    public Color hoverColor;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mousePos;
    }

    public void Hover(){
        animator.SetBool("Idle", false);
        animator.SetBool("Fast", true);
        //sprite.color = hoverColor;
    }

    public void DeHover(){
        animator.SetBool("Idle", true);
        animator.SetBool("Fast", false);
        //sprite.color = originalColor;
    }
}
