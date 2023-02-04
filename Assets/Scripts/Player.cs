using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float bounceForce;
    public Vector2 groundCheckShift;
    public float groundCheckDistance;
    private Rigidbody2D rb;
    public GameObject lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

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
            newVel /= rb.mass / Time.fixedDeltaTime;
            newVel.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        }

        rb.velocity += newVel;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(rb.position + groundCheckShift, Vector2.down * groundCheckDistance, groundCheckDistance);
    }

    private bool HitMushroom()
    {
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(rb.position + groundCheckShift, Vector2.down * groundCheckDistance, groundCheckDistance))
        {
            if (hit.collider != null)
                return hit.collider.gameObject.GetComponent<MushroomNode>() != null;
        }
        return false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MushroomNode>() != null)
        {
            Vector2 dir = Vector2.Perpendicular(-collision.contacts[0].normal);
            dir.y = Mathf.Max(Mathf.Abs(dir.y), 1);
            rb.velocity += dir * bounceForce;
        }
    }
}
