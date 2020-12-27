using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{

    public float initSpeed = 1;
    public bool faceDir = true;
    public float jumpInterval = 2;
    
    
    public LayerMask ground;
    public Collider2D coll;

    private Rigidbody2D rb;
    private Animator anim;
    
    private float speed;
    private float minBound, maxBound;
    private float jumpTime = 0.1f;
    
    private bool jumping;
    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        var left = GameObject.Find("left");
        var right = GameObject.Find("right");
        
        minBound = left.transform.position.x;
        maxBound = right.transform.position.x;
        
        Destroy(left);
        Destroy(right);
        
        speed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        PlayAnimator();

        if (jumpTime < jumpInterval)
        {
            jumpTime += Time.deltaTime;
        }
        else
        {
            jumpTime = 0;
        }
    }

    void Movement()
    {
        if (coll.IsTouchingLayers(ground))
        {
            falling = false;

            if (jumpTime <= 0)
            {
                Jump(10);
            }
        }
        else
        {
            falling = false;
        }

        if (faceDir)
        {
            if (transform.position.x < minBound)
            {
                faceDir = false;
            }
        }
        else
        {
            if (transform.position.x > maxBound)
            {
                faceDir = true;
            }

        }
        
        if (rb.velocity.y < 0)
        {
            jumping = false;
            falling = true;
        }
    }

    void Jump(float force)
    {
        if (faceDir)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, force);
            
            if (transform.position.x < minBound)
            {
                faceDir = false;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, force);
            
            if (transform.position.x > maxBound)
            {
                faceDir = true;
            }

        }
        
        jumping = true;
        falling = false;
    }
    
    void PlayAnimator()
    {
        anim.SetBool("jumping", jumping);
        anim.SetBool("falling", falling);
    }
}
