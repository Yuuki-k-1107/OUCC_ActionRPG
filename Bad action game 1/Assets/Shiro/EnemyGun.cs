using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyGunCol checkCollision;
    public int EnemHP = 30;
    public bool isWalk = true;
    [Header("攻撃オブジェクト")] public GameObject attackObj;
    [Header("攻撃間隔")] public float interval;
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
    private float timer;
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

            EnemHP -= (4 + Plyrctlr.Level);

        }
        else if (collision.gameObject.tag == "Wepons")
        {
            EnemHP -= (8 + Plyrctlr.Level);
        }
    }
    void FixedUpdate()
    {
        if (sr.isVisible || nonVisibleAct)
        {
            if (isWalk)
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

                if (checkCollision.isOn || timeToBack)
                {
                    rightTleftF = !rightTleftF;
                    timeToBack = false;
                }
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
            rb.velocity = new Vector2(xVector * speed, -gravity);
            //                anim.SetBool("walk", true);
        }
        else
        {
            //                anim.SetBool("walk", false);
        }
        if (timer > interval)
        {
            timer = 0.0f;
            Debug.Log("撃った");
            GameObject g = Instantiate(attackObj);
            g.transform.SetParent(transform);
            g.transform.position = attackObj.transform.position;
            g.SetActive(true);
        }
        else
        {
            timer += Time.deltaTime;
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
