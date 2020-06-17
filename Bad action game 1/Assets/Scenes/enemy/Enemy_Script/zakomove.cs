using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakomove : MonoBehaviour {
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    private float deadTimer = 0.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (oc.playerStepOn)
        {
            if (!isDead)
            {
                anim.Play("dead");
                isDead = true;
                col.enabled = false;
            }
            else
            {

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
        else
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
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }

        }
        }
    }


