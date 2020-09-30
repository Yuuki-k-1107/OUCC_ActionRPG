using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakomove : MonoBehaviour {
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("敵のサイズ")] public float es;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    private bool isHit = false;
    private string SpearTag = "spear";
    private string BatTag = "bat";
    private string PlayerShotTag = "PlayerShot";
    private float deadTimer = 0.0f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }
                int xVector = -1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-es, es, 1);
                }
                else
                {
                    transform.localScale = new Vector3(es, es, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
        else
        {
            if (!isDead)
            {
                anim.Play("dead");
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5));
                if (deadTimer > 3.0f)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    deadTimer += Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = false;
        }
    }
}


