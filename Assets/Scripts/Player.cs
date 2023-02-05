using System.Linq;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float bounceForce;
    public Vector2 groundCheckShift;
    public Vector2 groundCheckSize;
    public float groundCheckDistance;
    public float stickiness = 0.5f;
    private Rigidbody2D rb;
    private bool previouslyJumping = false;
    private Animator animator;
    private SpriteRenderer sprite;
    public GameObject lastCheckpoint;

    private AudioSource walksfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        walksfx = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        if(xMove < 0){
            sprite.flipX = true;

        }
        else if(xMove > 0){
            sprite.flipX = false;
        }

        if(xMove > 0 || xMove < 0)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Moving", true);
            if(!walksfx.isPlaying){
                walksfx.Play();
            }
        }
        else{
            animator.SetBool("Idle", true);
            animator.SetBool("Moving", false);
            walksfx.Stop();
        }

        Vector2 newVel = new Vector2((xMove * speed) - rb.velocity.x, 0);

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                newVel.y = jumpForce;
                previouslyJumping = true;
            }
            else if (!previouslyJumping)
            {
                newVel.y = -stickiness;
            }
            else
            {
                previouslyJumping = false;
            }
        }
        else
        {

            newVel /= rb.mass / Time.deltaTime;
            newVel.y += Physics2D.gravity.y * Time.deltaTime;
        }

        rb.velocity += newVel;
    }

    private bool IsGrounded()
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(rb.position + groundCheckShift, groundCheckSize, 0, Vector2.down * groundCheckDistance, groundCheckDistance, LayerMask.GetMask(new string[] { "Default" }));
        if (hit.Count() > 0)
        {
            foreach (RaycastHit2D h in hit)
            {
                if (!h.collider.isTrigger)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GoToCheckpoint() 
    {
        StartCoroutine(Die());
        //this.transform.position = lastCheckpoint.transform.position;
        rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Checkpoint>() != null)
        {
            lastCheckpoint = collision.gameObject;
        }
        else if (collision.gameObject.GetComponent<MushroomNode>() != null)
        {
            MushroomNode mushroom = collision.gameObject.GetComponent<MushroomNode>();
            // If the mushroom is active and we're falling, then do the bounce logic
            if (mushroom.createdObject.activeSelf)
            {
                // if walking up to stem
                if (mushroom.isStem)
                {
                    Vector2 dir = Vector2.up;
                    rb.velocity += dir * bounceForce;
                }
                // if falling onto mushroom
                else if (rb.velocity.y < 0)
                {
                    Vector2 dir = Vector2.Perpendicular(-rb.velocity);
                    dir.y = Mathf.Max(Mathf.Abs(dir.y), 1);
                    rb.velocity += dir * bounceForce;
                }
            }

        }
        else if(collision.gameObject.GetComponent<MushroomNode2>() != null){
            MushroomNode2 mushroom = collision.gameObject.GetComponent<MushroomNode2>();
            if(mushroom.createdObject.activeSelf){
                rb.velocity += Vector2.up * bounceForce * 2;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MushroomNode mushroom = collision.gameObject.GetComponent<MushroomNode>();
        if (mushroom != null && mushroom.createdObject.activeSelf)
        {
            Vector2 dir = Vector2.Perpendicular(collision.contacts[0].normal) * Vector2.Dot(rb.velocity, collision.contacts[0].normal);
            dir.y = Mathf.Max(Mathf.Abs(dir.y), 1);
            rb.velocity += dir * bounceForce;
        }
       
    }

    private IEnumerator Die(){
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.8f);
        this.transform.position = lastCheckpoint.transform.position;
    }


}
