using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    
    private Animator anim;

    public void Death()
    {
        anim.SetTrigger("death");
        audioSource.Play();
        
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
