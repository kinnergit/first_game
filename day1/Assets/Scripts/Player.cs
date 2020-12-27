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
    public Collider2D coll;
    public Collider2D head;

    private Rigidbody2D rb;
    private Animator anim;

    private bool jumping;
    private bool falling;
    private bool idle;
    private bool crouch;

    private bool isHurt;
    
    private float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        speed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt)
        {
            StartCoroutine(HurtRecover());
        }
        else
        {
            Movement();
        }
        
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

        // 蹲下，低头
        head.enabled = ! crouch;
        
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
                Jump(jumpForce);
            }
        }

        if (jumping && rb.velocity.y < 0)
        {
            jumping = false;
            falling = true;
        }
    }

    void Jump(float force)
    {
        isPlayerOnGround = false;
        rb.velocity = new Vector2(rb.velocity.x, force);
        jumping = true;
        falling = false;
    }

    void Flick(GameObject go, float force)
    {
        if (transform.position.x < go.transform.position.x)
        {
            isHurt = true;
            rb.velocity = Vector2.left * force;
        }
        else if (transform.position.x > go.transform.position.x) 
        {
            isHurt = true;

            rb.velocity = Vector2.right * force;
        }
    }

    void PlayAnimator()
    {
        anim.SetFloat("running", Math.Abs(rb.velocity.x));
        anim.SetBool("jumping", jumping);
        anim.SetBool("falling", falling);
        anim.SetBool("crouch", crouch);
        anim.SetBool("hurt", isHurt);
    }

    IEnumerator HurtRecover(float recoverTime = 0.3f)
    {
        yield return new WaitForSeconds(recoverTime);
        isHurt = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 碰到敌人
        if (other.gameObject.CompareTag("enemy"))
        {
            // 踩到敌人
            if (falling)
            {
                Jump(8);
                Destroy(other.gameObject);
            }
            else
            {
                Flick(other.gameObject, 5);
                Jump(5);
            }
        }
    }
}
