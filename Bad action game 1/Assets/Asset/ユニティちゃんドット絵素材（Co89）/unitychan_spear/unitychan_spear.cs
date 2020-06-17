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

        //子オブジェクト"spear_collider"を取得
        GameObject spearcollider = transform.Find("spear_collider").gameObject;

        //"spear_collider"からBoxCollider2Dを取得
        spearCollider = spearcollider.GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CloseAttack_spear"))
        {
            anim.SetBool("spear", true);

            spearCollider.enabled = true;

            Invoke("ColliderReset", 0.5f);
        }
        else if (Input.GetButtonUp("CloseAttack_spear"))
        {
            anim.SetBool("spear", false);
        }
        
    }
}
