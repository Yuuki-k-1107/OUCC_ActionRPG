using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D Rig2D;
    Animator anm1;
    //GameObject director;
    float liftForce = 500.0f;
    float moveForce = 25.0f;
    float limitspeed = 5.0f;
    public static int curHP;
    public static int maxHP;
    public static int Level = 1;
    public static int curEXP = 0;
    public static int Attack, Defense;
    public static int[] needEXP = new int[11];
    public static int bonusP = 0;
    public static int invincibleCount = 0;
    public static bool isJumping;
    public static bool isInvincible = false;
    public static Vector3 respawn;

    private int wmode;
    private int maxWeaponKind = 2;

    // Start is called before the first frame update
    void Start()
    {
        this.Rig2D = GetComponent<Rigidbody2D>();
        //maxHP = 100;
        curHP = maxHP;
        wmode = 1;
        respawn = new Vector3(-5, 0, 0);
        this.anm1 = GetComponent<Animator>();
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
        Attack = 5;
        Defense = 3;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!isInvincible)
            {
                isInvincible = true;
                invincibleCount = 100;
                curHP -= (12 - Defense);
                this.Rig2D.AddForce(transform.right * 8.0f);
            }
        }
        else if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("GoalScene");
        }
        else if (other.gameObject.tag == "1-1")
        {
            //SceneManager.LoadScene("Stage1-2"); when we introduce 1-2
            SceneManager.LoadScene("Stage2-1");
        }
        else if (other.gameObject.tag == "2-1")
        {
            //SceneManager.LoadScene("Stage2-2");
            SceneManager.LoadScene("Stage3-1");
        }
    }

    void OnTriggerStay2D(Collider2D other2)
    {
        /*if (other2.gameObject.tag == "door")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                stscore = UI.scored;
                stlife = UI.lived;
                respawn = new Vector3(-6.5f, 3.0f, 0);
                SceneManager.LoadScene("scene2");
            }
        }
        else if (other2.gameObject.tag == "door2")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                respawn = new Vector3(100.5f, 3.0f, 0);
                transform.position = respawn;
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (invincibleCount > 0)
        {
            invincibleCount--;
        } else
        {
            isInvincible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) limitspeed = 10.0f;
        else limitspeed = 5.0f;
        float speed = Mathf.Abs(this.Rig2D.velocity.x);
        this.anm1.SetFloat("SPEED", speed);
        this.anm1.SetFloat("V_V", this.Rig2D.velocity.y); //鉛直方向の速度
        Vector2 localscale = transform.localScale;

        if ((Input.GetKeyDown(KeyCode.UpArrow))/*(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && isground.ground == true*/)
        {
            this.Rig2D.AddForce(transform.up * liftForce);
            isground.ground = false;
            isJumping = true;
        }
        if (speed < limitspeed*99999)
        {
            this.Rig2D.AddForce(transform.right * Input.GetAxis("Horizontal") * this.moveForce);
        }
        if (this.Rig2D.transform.position.y < -5.0f)
        {
            this.Rig2D.velocity = new Vector2(0, 0);
            curHP -= (int)(maxHP/10);
            transform.position = respawn;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //ここに武器発射を割り当て
            Debug.Log(wmode);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (wmode == maxWeaponKind)
            {
                wmode++;
            }
            else
            {
                wmode = 1;
            }
        }

        if (this.Rig2D.velocity.x * localscale.x < 0)
        {
            localscale.x *= -1.0f;
            transform.localScale = localscale;
        }

        if (curHP <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
            Debug.Log("ゲームオーバー");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curEXP++;
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (bonusP > 0)
            {
                maxHP += 10;
                curHP += 10;
                bonusP--;
            }
            else
            {
                Debug.Log("ボーナスポイントが足りません");
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (bonusP > 0)
            {
                curHP = maxHP;
                Attack++;
                bonusP--;
            }
            else
            {
                Debug.Log("ボーナスポイントが足りません");
            }
        }

        if (Level <= 10)
        {
            if (curEXP >= needEXP[Level])
            {
                Level++;
                maxHP += 50;
                curHP = maxHP; //レベルアップ時HP全快
                Attack += 2;
                Defense++;
                bonusP += 3;
            }
        }
    }
}

