using System.Collections;
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

        spearCollider = GameObject.Find("unitychan_spear1").GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CloseAttack"))
        {
            anim.SetBool("spear", true);

            spearCollider.enabled = true;

            Invoke("ColliderReset", 0.5f);
        }
        else if (Input.GetButtonUp("CloseAttack"))
        {
            anim.SetBool("spear", false);
        }
        
    }
}
