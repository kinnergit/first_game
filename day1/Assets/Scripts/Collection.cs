using System;
using Management;
using UnityEngine;

public class Collection : MonoBehaviour
{

    public Collider2D coll;

    private GameManager gm;

    private bool isCherryCollected;
    private bool isGemCollected;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        gm = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;

            if (gameObject.CompareTag("cherry"))
            {
                isCherryCollected = true;
            } else if (gameObject.CompareTag("gem"))
            {
                isGemCollected = true;
            }

            Destroy(gameObject);
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
