﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitychan_spear : MonoBehaviour
{
    private Animator anim;

    private Collider2D spearCollider;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CloseAttack_spear"))
        {
            anim.SetBool("Spear", true);


        }
        else if (Input.GetButtonUp("CloseAttack_spear"))
        {
            anim.SetBool("Spear", false);
        }
        
    }
}
