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
    public static int curHP;
    public static int maxHP;
    public static bool isJumping;
    public static Vector3 respawn;

    // Start is called before the first frame update
    void Start()
    {
        this.Rig2D = GetComponent<Rigidbody2D>();
        maxHP = 100;
        curHP = maxHP;
        respawn = new Vector3(-5, 0, 0);
        this.anm1 = GetComponent<Animator>();
        //this.director = GameObject.Find("kantoku");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.gameObject.tag == "credit")
        {
            this.director.GetComponent<UI>().GetCredit();
            Destroy(other.gameObject);
        }*/
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
    // Update is called once per frame
    void Update()
    {
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
        if (this.Rig2D.transform.position.y < -5.0f)
        {
            this.Rig2D.velocity = new Vector2(0, 0);
            curHP -= 10;
            transform.position = respawn;
        }

        if(this.Rig2D.velocity.x * localscale.x < 0 )
        {
            localscale.x *= -1.0f;
            transform.localScale = localscale;
        }

        if (curHP <= 0) {
            Debug.Log("ゲームオーバー");
        }
    }
}
