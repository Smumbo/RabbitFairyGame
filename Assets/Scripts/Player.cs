using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;


        if(Input.GetButtonDown("Jump")){
                //WHY DOESN'T RB.ADDFORCE WORK LIKE IT SHOULD STUPID PIECE OF CODE
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Death>() != null){
            this.transform.position = lastCheckpoint.transform.position;
        }
        else if(collision.gameObject.GetComponent<Checkpoint>() != null){
            lastCheckpoint = collision.gameObject;
        }
    }
}
