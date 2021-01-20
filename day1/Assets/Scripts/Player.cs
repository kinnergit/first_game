using System;
using System.Collections;
using Management;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float initSpeed = 5;
    public float crouchSpeed = 2;
    public float jumpForce = 12;
    public bool isPlayerOnGround = true;

    public GameObject celling;
    public GameObject flooring;

    public AudioSource jumpAudio, hurtAudio, collectAudio;
    
    public LayerMask ground;
    public Collider2D head;
    
    private Rigidbody2D rb;
    private Animator anim;

    private bool jumping;
    private bool falling;
    private bool crouch;

    private bool isHurt;
    
    private float speed;

    private Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        speed = initSpeed;
        
        InitJoy();
    }

    void InitJoy()
    {
        joystick = CanvasManager.GetInstance().joystick;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(flooring.transform.position, 0.01f, ground))
        {
            isPlayerOnGround = true;
            falling = false;
        }
        else
        {
            isPlayerOnGround = false;
        }
        
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
        // enum: -1, 0, 1
        // var faceDir = Input.GetAxisRaw("Horizontal");
        var faceDir = joystick.Horizontal == 0 ? 0 : Mathf.Sign(joystick.Horizontal);
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(faceDir, 1, 1);
        }
        
        // var vv = Input.GetAxisRaw("Vertical");
        var vv = joystick.Vertical;
        if (vv < -0.3f)
        {
            crouch = true;
            speed = crouchSpeed;
        }
        else if (crouch && Physics2D.OverlapCircle(celling.transform.position, 0.1f, ground))
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
        // var hv = Input.GetAxis("Horizontal");
        var hv = joystick.Horizontal;
        if (hv != 0)
        {
            rb.velocity = new Vector2(hv * speed, rb.velocity.y);
        }

        // Press the jump Key code
        // if (Input.GetButton("Jump"))
        if (vv > 0.3f)
        {
            if (isPlayerOnGround)
            {
                Jump(jumpForce, false);
            }
        }

        if (jumping && rb.velocity.y < 0)
        {
            jumping = false;
            falling = true;
        }
    }

    void Jump(float force, bool small = true)
    {
        if ( ! small)
        {
            jumpAudio.Play();
        }
        
        // isPlayerOnGround = false;
        rb.velocity = new Vector2(rb.velocity.x, force);
        jumping = true;
        falling = false;
    }

    void Flick(GameObject go, float force)
    {
        hurtAudio.Play();
        
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
            if (falling && Physics2D.OverlapCircle(flooring.transform.position, 0.03f))
            {
                Jump(8);

                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Death();
            }
            else
            {
                Flick(other.gameObject, 5);
                Jump(5);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cherry") || other.gameObject.CompareTag("gem"))
        {
            collectAudio.Play();
        } else if (other.gameObject.CompareTag("DeadLine"))
        {
            GetComponent<AudioSource>().Stop();
            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame()
    {
        GameManager.GetInstance().ClearCollectionNums();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
