using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_move : MonoBehaviour
{
    public GameObject bullet;
    public float jumpPower;
    public float jumpPoworConst = 0.8f;
    public float jumpGravity = 0.05f;
    public float speed;
    public float gravity;
    public float es;
    [Header("攻撃間隔")] public float interval;
    public int HP = 20;
    public EnemyCollisionCheck checkCollision;


    private int move_type = 0;
    private int Attack_type = 0;

    private bool isIdle = true;
    private bool isAttack = false;
    private bool isRun = false;
    private bool isJump = false;
    private bool rightTleftF = false;
    private bool isDead = false;
    private bool isHit = false;
    private string SpearTag = "spear";
    private string BatTag = "bat";
    private string PlayerShotTag = "PlayerShot";
    private float jumpPos = 0.0f;
    private float guntimer = 0.0f;
    private float ySpeed;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate(){
        if(!isHit){

            if (checkCollision.isOn)
            {
                rightTleftF = !rightTleftF;
            }
            int xVector = -1;
            if (rightTleftF)
            {
                xVector = 1;
                transform.localScale = new Vector3(es, es, 1);
            }
            else
            {
                transform.localScale = new Vector3(-es, es, 1);
            }

            if(move_type == 1){
                isIdle = false;
                isAttack =true;
            }

            if(move_type == 2){
                isIdle = false;
                isRun = true;
                StartCoroutine("WaitFotWalk");
            }

            if(move_type == 3){
                isIdle = false;
                isJump = true;
                jumpPower = jumpPoworConst;
            }

            if(isIdle){
                move_type = Random.Range(0,4);
                jumpPos = transform.position.y;
                anim.SetBool("shoot", false);
                anim.SetBool("run_shoot", false);
            }
            else if(isRun){
               rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else if(isJump){
                jumpPower -= jumpGravity;
                ySpeed = jumpPower - jumpGravity;
                rb.velocity = new Vector2(xVector * speed,ySpeed);
                if(jumpPower<0 && transform.position.y <= jumpPos){
                    isIdle = true;
                    isJump = false;
                }
            }
            else if(isAttack){
                    Attack_type = Random.Range(0,2);
                    if(Attack_type == 0){
                       anim.SetBool("shoot", true);
                       rb.velocity = new Vector2(0, 0);
                       shoot();
                    }
                    else if(Attack_type == 1){
                        anim.SetBool("run_shoot", true);
                        rb.velocity = new Vector2(xVector * speed, -gravity);
                        shoot();
                    }
            }
        }
        else{
            HP -= 1;
            if(HP == 0){
            isDead = true;
            }

            if (isDead){
                    anim.Play("dead");
                    capcol.enabled = false;
                     Destroy(this.gameObject);
            }
        }
        SetAnimation();

    }

    IEnumerator WaitFotAttack()
    {
        yield return new WaitForSeconds(2.0f);
        isIdle = true;
        isAttack = false;
    }

    IEnumerator WaitFotWalk()
    {
        yield return new WaitForSeconds(0.5f);
        isIdle = true;
        isRun = false;
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

    private void shoot(){
        if(guntimer > interval){
            guntimer = 0.0f;
            GameObject fire = Instantiate(bullet);
            fire.transform.SetParent(transform);
            fire.transform.position = bullet.transform.position;
            fire.SetActive(true);
        }
        else
        {
            guntimer += Time.deltaTime;
        }
    }


    private void SetAnimation(){
        anim.SetBool("jump", isJump);
        anim.SetBool("Run", isRun);
    }
}
