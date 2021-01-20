using Management;
using UnityEngine;

public class Collection : MonoBehaviour
{

    public Collider2D coll;

    private GameManager gm;
    private Animator anim;

    private bool isCherryCollected;
    private bool isGemCollected;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        gm = GameManager.GetInstance();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;

            if (gameObject.CompareTag("cherry"))
            {
                isCherryCollected = true;
                anim.Play("CherryCollected");
            } else if (gameObject.CompareTag("gem"))
            {
                isGemCollected = true;
                anim.Play("GemCollected");
            }

            Destroy(gameObject, 0.2f);
        }
    }

    private void OnDestroy()
    {
        if (isCherryCollected)
        {
            gm.IncrCherryNum();
        } else if (isGemCollected)
        { 
            gm.IncrGemNum();
        }
    }
}
