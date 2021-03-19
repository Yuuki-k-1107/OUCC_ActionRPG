using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
     #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("地面に対する接触判定")] public DbottomCollisionCheck dbottomCollision;
    [Header("敵のサイズ")] public float es;
    [Header("ジャンプ力")] public float jumpPower;
    public AnimationCurve jumpCurve;

    #endregion

    #region//プライベート変数
    protected Rigidbody2D rb = null;
    protected SpriteRenderer sr = null;
    protected Animator anim = null;
    protected BoxCollider2D col = null;
    protected bool rightTleftF = false;
    protected bool isDead = false;
    protected bool isHit = false;
    [SerializeField]
    protected bool isJump = false;
    protected bool isGrounded = false;
    protected bool canJumping = false;
    protected string SpearTag = "spear";
    protected string BatTag = "bat";
    protected string PlayerShotTag = "PlayerShot";
    protected float deadTimer = 0.0f;
    protected float start = 3.0f;
    protected float finish = 5.0f;
    [SerializeField]
    protected float jumptimer = 0.0f;
    [SerializeField]
    protected Vector2 velocity = Vector2.zero;
    protected float jumpTime = 0f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = false;
        }
    }

    protected void Move(float speed , float gravity){
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }
                
                    velocity.x = -1;

                if (rightTleftF)
                {
                    velocity.x = 1;
                    transform.localScale = new Vector3(-es, es, 1);
                }
                else
                {
                    transform.localScale = new Vector3(es, es, 1);
                }

                if(canJumping){

                    jumptimer += Time.deltaTime;

                    
                    if(jumptimer > start){
                            jumpTime += Time.deltaTime;
                            isJump = true;
                        }
                    

                    if(jumptimer > finish){
                        isJump = false;
                        jumptimer = 0;
                        jumpTime = 0f;
                    }
                }

                if(!isJump){
                    velocity.x *= speed;
                    velocity.y = -gravity;
                } else {
                    velocity.x *= speed;
                    velocity.y = jumpPower * jumpCurve.Evaluate(jumpTime);
                }



                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
    }

    protected void Dead(){
        if (!isDead)
            {
                anim.Play("dead");
                velocity = new Vector2(0, -gravity);
                rb.MovePosition(rb.position + velocity * Time.deltaTime);
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

     protected void Jump(){
        if(dbottomCollision.isOn){
            jumptimer += Time.deltaTime;
            if(jumptimer > start){
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if(jumptimer > finish){
                jumptimer = 0;
            }
        }
    }
}
