using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float weight;
    public Vector2 groundCheckShift;
    public float groundCheckDistance;
    private Rigidbody2D rb;
    public GameObject lastCheckpoint;
    private GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1

        Vector2 newVel = new Vector2((xMove * speed) - rb.velocity.x, 0);
        
        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                newVel.y = jumpForce;
            }
        }
        else
        {
            newVel /= weight / Time.fixedDeltaTime;
            newVel.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        }

        
        rb.velocity += newVel;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(rb.position + groundCheckShift, Vector2.down * groundCheckDistance, groundCheckDistance);
    }

    public void GoToCheckpoint() 
    {
        this.transform.position = lastCheckpoint.transform.position;
        rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Death>() != null)
        {
            this.transform.position = lastCheckpoint.transform.position;
        }
        else if (collision.gameObject.GetComponent<Checkpoint>() != null)
        {
            lastCheckpoint = collision.gameObject;
        }
    }
}
