using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitychan_bat : MonoBehaviour
{
    private Animator anim;

    private Collider2D batCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //子オブジェクト"bat_collider"を取得
        GameObject batcollider = transform.Find("bat_collider").gameObject;

        batCollider = GameObject.Find("unitychan_bat1").GetComponent<BoxCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CloseAttack_bat"))
        {
            anim.SetBool("bat", true);

            batCollider.enabled = true;

            Invoke("ColliderReset", 0.5f);
        }
        else if (Input.GetButtonUp("CloseAttack_bat"))
        {
            anim.SetBool("bat", false);
        }

    }
}
