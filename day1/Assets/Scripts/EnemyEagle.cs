using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : MonoBehaviour
{
    public float initSpeed = 1;
    public bool faceDir = true;
    
    public LayerMask ground;
    public Collider2D coll;

    private Rigidbody2D rb;
    private Animator anim;
    
    private float speed;
    private float minBound, maxBound;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        var top = GameObject.Find("top");
        var bottom = GameObject.Find("bottom");
        
        minBound = bottom.transform.position.y;
        maxBound = top.transform.position.y;
        
        Destroy(top);
        Destroy(bottom);
        
        speed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        FaceCheck();
        
        if (faceDir)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
        }
    }
    void FaceCheck()
    {
        if (faceDir)
        {
            if (transform.position.y < minBound)
            {
                faceDir = false;
            }
        }
        else
        {
            if (transform.position.y > maxBound)
            {
                faceDir = true;
            }
        }
    }
}
