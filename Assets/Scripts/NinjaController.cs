using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float velocityX = 10;
    public float jumpForce = 40;

    private float vertical;
    private float speed = 10f;
    private bool isLadder;
    private bool isClimbing;

    public GameObject rightBullet;
    public GameObject leftBullet;

    private ScoreController game;

    //const
    private const int IDLE = 0;
    private const int RUN = 1;
    private const int JUMP = 2;
    private const int SLIDE = 3;
    private const int GLIDE = 4;
    private const int CLIMB = 5;

    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        game = FindObjectOfType<ScoreController>();

    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(IDLE);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(RUN);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(RUN);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(JUMP);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            changeAnimation(SLIDE);
        }

        if (Input.GetKey(KeyCode.P))
        {
            changeAnimation(GLIDE);
        }

        if (Input.GetKeyUp(KeyCode.S)){
            var bullet = sr.flipX ? leftBullet : rightBullet;
            var position = new Vector2(transform.position.x, transform.position.y);
            var rotation = rightBullet.transform.rotation;
            Instantiate(bullet, position, rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            changeAnimation(CLIMB);
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            game.LoseLife();
        }
    }
}
