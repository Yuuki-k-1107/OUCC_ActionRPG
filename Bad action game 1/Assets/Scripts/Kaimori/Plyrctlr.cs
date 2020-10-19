using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plyrctlr : MonoBehaviour
{
    public Rigidbody2D Rig2D;
    Animator anm1;
    //GameObject director;
    float liftForce = 500.0f;
    float moveForce = 25.0f;
    float limitspeed = 5.0f;
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプする高さ")] public float jumpHeight;
    [Header("ジャンプ制限時間")] public float jumpLimitTime;
    [Header("接地判定")] public isground ground;
    [Header("天井判定")] public isground head;
    [Header("ダッシュ速度表現")] public AnimationCurve dashCurve;
    [Header("ジャンプ速度表現")] public AnimationCurve jumpCurve;
    [Header("踏みつけ判定の高さの割合")] public float stepOnRate;
    [Header("現在HP")] public static int curHP;
    [Header("最大HP")] public static int maxHP;
    [Header("レベル")] public static int Level = 1;
    [Header("現在経験値")] public static int curEXP = 0;
    [Header("必要経験値")]public static int[] needEXP = new int[11];
    [Header("旧接地判定")] public static bool isJumping;
    [Header("リスポーン")] public static Vector3 respawn;

    private int wmode;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;
    private MovingFloor moveObj = null;
    private bool isGround = false;
    private bool isJump = false;
    private bool isHead = false;
    private bool isRun = false;
    private bool isDown = false;
    private bool isOtherJump = false;
    private bool isContinue = false;
    private bool nonDownAnim = false;
    private float verticalKey = 0.0f;
    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float dashTime = 0.0f;
    private float jumpTime = 0.0f;
    private float beforeKey = 0.0f;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private string enemyTag = "Enemy";
    private string deadAreaTag = "DeadArea";
    private string hitAreaTag = "HitArea";
    private string moveFloorTag = "MoveFloor";
    private string fallFloorTag = "FallFloor";

    // Start is called before the first frame update
    void Start()
    {
        this.Rig2D = GetComponent<Rigidbody2D>();
        //maxHP = 100;
        curHP = maxHP;
        wmode = 1;
        respawn = new Vector3(-5, 0, 0);
        //        this.anm1 = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        //this.director = GameObject.Find("kantoku");
        needEXP[0] = 0;
        needEXP[1] = 10;
        needEXP[2] = 30;
        needEXP[3] = 60;
        needEXP[4] = 100;
        needEXP[5] = 150;
        needEXP[6] = 250;
        needEXP[7] = 400;
        needEXP[8] = 700;
        needEXP[9] = 1000;
        needEXP[10] = 1500;
    }

    //接触したとき
    void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.gameObject.tag == "Enemy")
        {
            curHP -= (12 - Level);
            //curEXP += 3;
            this.Rig2D.AddForce(transform.right * 5.0f);
        }
        else 
        */
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("GoalScene");
        }
        else if (other.gameObject.tag == "Next1")
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (other.gameObject.tag == "deadAreaTag")
        {

        }
        else if (other.gameObject.tag == "hitAreaTag")
        {

        }
    }


    void FixedUpdate()
    {
        isGround = ground.IsGround();
        isHead = head.IsGround();

        float xSpeed = GetXSpeed();
        float ySpeed = GetYSpeed();

        SetAnimation();

        Vector2 addVelocity = Vector2.zero;
        if(moveObj !=null)
        {
            addVelocity = moveObj.GetVelocity();
        }
        this.Rig2D.velocity = new Vector2(xSpeed, ySpeed) + addVelocity;




        /*
        if (Input.GetKey(KeyCode.LeftControl)) limitspeed = 10.0f;
        else limitspeed = 5.0f;
        float speed = Mathf.Abs(this.Rig2D.velocity.x);
        this.anm1.SetFloat("SPEED", speed);
        this.anm1.SetFloat("V_V", this.Rig2D.velocity.y); //鉛直方向の速度
        Vector2 localscale = transform.localScale;

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && isground.ground == true)
        {
            this.Rig2D.AddForce(transform.up * liftForce);
            isground.ground = false;
            isJumping = true;
        }
        if (speed < limitspeed)
        {
            this.Rig2D.AddForce(transform.right * Input.GetAxis("Horizontal") * this.moveForce);
        }
        */
        if (this.Rig2D.transform.position.y < -5.0f)
        {
            this.Rig2D.velocity = new Vector2(0, 0);
            curHP -= 10;
            transform.position = respawn;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ここに武器発射を割り当て
            Debug.Log(wmode);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (this.wmode == 1)
            {
                wmode = 2;
            }
            else
            {
                wmode = 1;
            }
        }
        /*
        if (this.Rig2D.velocity.x * localscale.x < 0)
        {
            localscale.x *= -1.0f;
            transform.localScale = localscale;
        }
        */
        if (curHP <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
            Debug.Log("ゲームオーバー");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curEXP++;
        }

        if (Level <= 10)
        {
            if (curEXP >= needEXP[Level])
            {
                Level++;
                maxHP += 50;
                curHP = maxHP; //レベルアップ時HP全快
            }
        }
    }


    /// <summary>
    /// Y軸成分の速度計算
    /// </summary>
    /// <returns>Y軸方向の速さ</returns>
    private float GetYSpeed()
    {
        /*
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            verticalKey = 1.0f;
        }
        else
        {
            verticalKey = 0.0f;
        }*/
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;
        if (isOtherJump)
        {
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if(canHeight && canTime && !isHead)
            {
                Debug.Log("その他ジャンプ");
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isOtherJump = false;
                jumpTime = 0.0f;
            }
        }
        //地面にいるとき
        else if(isGround)
        {
            if(verticalKey > 0)
            {
                Debug.Log("地面にいる");
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        //ジャンプ中
        else if (isJump)
        {
            bool pushUpKey = verticalKey > 0;
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;
            if(pushUpKey && canHeight && canTime && !isHead)
            {
                Debug.Log("ジャンプ中");
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        if (isJump || isOtherJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        return ySpeed;
    }

    /// <summary>
    /// X成分の速度算出
    /// </summary>
    /// <returns>X軸方向の速さ</returns>
    private float GetXSpeed()
    {

        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        //前回の入力からダッシュの反転を判断して速度を変える
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }

        xSpeed *= dashCurve.Evaluate(dashTime);
        beforeKey = horizontalKey;
        return xSpeed;
    }

    /// <summary>
    /// アニメの設定
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("Jump", isJump || isOtherJump);
        anim.SetBool("Ground", isGround);
        anim.SetBool("Run", isRun);
    }


    #region//接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool enemy = (collision.collider.tag == enemyTag);
        bool moveFloor = (collision.collider.tag == moveFloorTag);
        bool fallFloor = (collision.collider.tag == fallFloorTag);

        if (enemy || moveFloor || fallFloor)
        {
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {
                    if (enemy || fallFloor)
                    {
                        ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                        if (o != null)
                        {
                            if (enemy)
                            {
                                otherJumpHeight = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                                o.playerStepOn = true;        //踏んづけたものに対して踏んづけた事を通知する
                                jumpPos = transform.position.y; //ジャンプした位置を記録する
                                isOtherJump = true;
                                isJump = false;
                                jumpTime = 0.0f;
                            }
                            else if (fallFloor)
                            {
                                o.playerStepOn = true;
                            }
                        }
                        else
                        {
                            Debug.Log("ObjectCollisionが付いてないよ!");
                        }
                    }
                    else if (moveFloor)
                    {
                        moveObj = collision.gameObject.GetComponent<MovingFloor>();
                    }
                }
                else
                {
                    if (enemy)
                    {
                        curHP -= (12 - Level);
                        //curEXP += 3;
                        this.Rig2D.AddForce(transform.right * 5.0f);
                        break;
                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == moveFloorTag)
        {
            //動く床から離れた
            moveObj = null;
        }
    }

    #endregion
}
