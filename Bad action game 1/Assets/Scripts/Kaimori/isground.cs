using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isground : MonoBehaviour
{
    public static bool ground = true;
    Animator anim1;

    public void OnTriggerEnter2D(Collider2D something)
    {
        if(something.gameObject.tag == "enemy"||something.gameObject.tag == "tile1"||something.gameObject.tag == "surinuke")
        {
            ground = true;
            PlayerController.isJumping = false;
            this.anim1.SetTrigger("GROUND");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.anim1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
