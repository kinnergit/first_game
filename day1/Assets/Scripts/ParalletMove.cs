using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalletMove : MonoBehaviour
{
    public Camera cam;
    public float moveRate = 1f;

    private Vector2 startPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var camPos = cam.transform.position;
        
        transform.position = new Vector2(startPoint.x + camPos.x *  moveRate, transform.position.y);
    }
}
