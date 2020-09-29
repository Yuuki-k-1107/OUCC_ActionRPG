using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_move : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("敵のサイズ")] public float es;
    [Header("ジャンプ力")] public float jumpPowerConst;
    public float jumpGravity = 0.05f;
    public float start = 5.0f;
    public float finish = 5.3f;

    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    private bool isJump = false;
    private float deadTimer = 0.0f;
    private float jumptimer = 0.0f;
    private float jumpPower;

    #endregion


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
        if (!oc.playerStepOn)
        {
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }
                int xVector = -1;
                int yVector = 1;
                float ySpeed = 1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-es, es, 1);
                }
                else
                {
                    transform.localScale = new Vector3(es, es, 1);
                }
                 jumptimer += Time.deltaTime;
                if(jumptimer > finish){
                    isJump = false;
                    jumptimer = 0.0f;
                }
                else if(jumptimer > start){
                   isJump = true;
                }

                if(isJump){
                    jumpPower -= jumpGravity;
                    ySpeed = jumpPower - jumpGravity;
                }
                else if(!isJump){
                    ySpeed = -gravity;
                    jumpPower = jumpPowerConst;
                }
                rb.velocity = new Vector2(xVector * speed, yVector * ySpeed);
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
}
