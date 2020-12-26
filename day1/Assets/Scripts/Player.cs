using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float initSpeed = 5;
    public float crouchSpeed = 2;
    public float jumpForce = 12;
    public bool isPlayerOnGround = true;

    public LayerMask ground;
    
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coll;

    private bool jumping;
    private bool falling;
    private bool idle;
    private bool crouch;

    private float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();

        speed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        PlayAnimator();
    }

    void Movement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            falling = false;
            isPlayerOnGround = true;
        }
        else
        {
            isPlayerOnGround = false;
        }
        
        // enum: -1, 0, 1
        var faceDir = Input.GetAxisRaw("Horizontal");
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
        
        var vv = Input.GetAxisRaw("Vertical");
        if (vv < 0)
        {
            crouch = true;
            speed = crouchSpeed;
        }
        else
        {
            crouch = false;
            speed = initSpeed;
        }
        
        // range: [-1, 1]
        var hv = Input.GetAxis("Horizontal");
        if (hv != 0)
        {
            rb.velocity = new Vector2(hv * speed, rb.velocity.y);
        }

        // Press the jump Key code
        if (Input.GetButtonDown("Jump"))
        {
            if (isPlayerOnGround)
            {
                isPlayerOnGround = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumping = true;
                falling = false;
            }
        }

        if (jumping && rb.velocity.y < 0)
        {
            jumping = false;
            falling = true;
        }
    }

    void PlayAnimator()
    {
        anim.SetFloat("running", Math.Abs(rb.velocity.x));
        anim.SetBool("jumping", jumping);
        anim.SetBool("falling", falling);
        anim.SetBool("crouch", crouch);
    }

}
