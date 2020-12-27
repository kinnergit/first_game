using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{

    public Collider2D coll;
    public Text cherryNum;
    public Text gemNum;

    private static int cherryCollectionNum;
    private static int gemCollectionNum;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("cherry"))
            {
                cherryCollectionNum++;
                cherryNum.text = cherryCollectionNum.ToString();
            }
            
            if (gameObject.CompareTag("gem"))
            {
                gemCollectionNum++;
                gemNum.text = gemCollectionNum.ToString();
            }

            Destroy(gameObject);
        }
    }
}
