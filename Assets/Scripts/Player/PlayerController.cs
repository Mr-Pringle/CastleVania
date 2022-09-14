using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]


public class PlayerController : MonoBehaviour
{
    //public enum MovementState { normal, stairs }
    //MovementState currentMovement = MovementState.normal;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed = 5.0f;
    public int jumpForce = 300;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.02f;

    Coroutine jumpForceChange;

    //private int _lives = 3;

    //public int lives
    //{
    //    get { return _lives; }
    //    set 
    //    {
    //        //Losing a life
    //        // if (_lives > value)

    //        _lives = value;

    //        if ( _lives > maxLives)
    //        {
    //            _lives = maxLives;
    //        }

    //        //If lives are less than zero = gameover
    //        //if(_lives < 0)

    //        Debug.Log("Lives are set to: " + lives.ToString());
    //    }
    //}

    public int maxLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //switch (currentMovement)
        //{
        //    case MovementState.normal:

        //        break;
        //    case MovementState.stairs:
                
        //        break;
        //}

        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);

        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        bool isFired = Input.GetButtonDown("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);



        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Fire")
                anim.SetTrigger("Fire");
            else if (curPlayingClip[0].clip.name == "Fire")
                rb.velocity = new Vector2((hInput * speed), rb.velocity.y);
            else if (curPlayingClip[0].clip.name == "JumpAttack")
                rb.gravityScale = 5;
            else
            {
                rb.gravityScale = 1;
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }

        }

        Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (!isGrounded && Input.GetButtonDown("Vertical"))
        {
            isFired = true;
        }

        anim.SetBool("isFired", isFired);
        anim.SetFloat("MoveValue", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (hInput > 0 && !sr.flipX || hInput < 0 && sr.flipX)
        {
            sr.flipX = (hInput > 0);
        }
    }
    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
        }
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "StairTrigger")
    //    {
           
    //    }
    //}
}
