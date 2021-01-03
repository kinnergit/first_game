using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    
    private Animator anim;

    public void Death()
    {
        audioSource.Play();
        anim.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
