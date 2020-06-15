using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_jamp : MonoBehaviour
{
    
    public float jumpPower;
    private float jumptimer = 0.0f;
    

    

    // Update is called once per frame
    void Update()
    {
        jumptimer += Time.deltaTime;
        if (jumptimer > 2.0f)
        {
            
            transform.Translate(Vector3.up * jumpPower);
            jumptimer = 0.0f;
        }
        
    }
}
