
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dossun_move : MonoBehaviour
{

    [Header("重力")] public float gravity;
    [Header("上昇速度")] public float upspeed;
    [Header("床の接触判定")] public DbottomCollisionCheck bottomcheckCollision;
   
 

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    private float downPos = 0.0f;
    private bool upcheck = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        downPos = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sr.isVisible)
        {
            if (bottomcheckCollision.isOn)
            {
                upcheck = true;
            }
            
            
                if (upcheck)
                {
                    if (transform.position.y < downPos)
                    {
                        rb.velocity = new Vector2(0, upspeed);
                    }
                    else
                    {
                        upcheck = false;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0, -gravity);
                }
            
        }
        else
        {
            rb.Sleep();
        }
    }
}
