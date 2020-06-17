using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_jamp : MonoBehaviour
{
    
    public float jumpPower;
    public float jumptimer = 0.0f;
    private Rigidbody2D rb = null;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumptimer += 0.1f;
        if (jumptimer > 2.0f)
        {
            
            rb.velocity = new Vector2(0, jumpPower);
            jumptimer = 0.0f;
        }
        
    }
}
