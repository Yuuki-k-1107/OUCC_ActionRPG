using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{

    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyFlyCol checkCollision;
    public int EnemHP = 30;
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
    private float moveTimer = 0.0f;
    private bool timeToBack;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {

            EnemHP -= Plyrctlr.Attack;

        }
    }
    void FixedUpdate()
    {
            if (sr.isVisible || nonVisibleAct)
        {
            if (moveTimer > 1.0f)
            {
                timeToBack = true;
                moveTimer = 0;
            }
            else
            {
                moveTimer += Time.deltaTime;
            }
            if (checkCollision.isOn||timeToBack)
                {
                    rightTleftF = !rightTleftF;
                timeToBack = false;
                }
                int xVector = -1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-2, 2, 1);
                }
                else
                {
                    transform.localScale = new Vector3(2, 2, 1);
                }
                rb.velocity = new Vector2(xVector * speed, 0);
//                anim.SetBool("walk", true);
            }
            else
            {
//                anim.SetBool("walk", false);
            }
        if (EnemHP <= 0)
        {
            if (!isDead)
            {
                Plyrctlr.curEXP += 3;
//                anim.Play("dead");
                isDead = true;
                col.enabled = false;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5));
                rb.velocity = new Vector2(0, -gravity);
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
}
