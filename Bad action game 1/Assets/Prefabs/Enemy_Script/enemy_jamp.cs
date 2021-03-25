using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_jamp : MonoBehaviour
{
    
    public float jumpPower;
    public float limit;
    private float jumptimer = 0.0f;
    private Rigidbody2D rb = null;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumptimer += 0.1f;
        if (jumptimer > limit)
        {
            
            rb.AddForce(Vector2.up*jumpPower);
            jumptimer = 0.0f;
        }
        
    }
}
