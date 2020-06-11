using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plyrctlr : MonoBehaviour
{
    public Rigidbody2D Rig2D;
    GameObject director;
    float liftForce = 500.0f;
    float moveForce = 25.0f;
    float limitspeed = 5.0f;
    public static int curHP;
    public static int maxHP;
    public static Vector3 respawn;

    // Start is called before the first frame update
    void Start()
    {
        this.Rig2D = GetComponent<Rigidbody2D>();
        maxHP = 100;
        curHP = maxHP;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(this.Rig2D.velocity.y) <= 0.02f)
        {
            this.Rig2D.AddForce(transform.up * liftForce);
        }
        if (speed < limitspeed)
        {
            this.Rig2D.AddForce(transform.right * Input.GetAxis("Horizontal") * this.moveForce);
        }
        if (this.Rig2D.transform.position.y < -5.0f)
        {
            //this.director.GetComponent<UI>().LossLife();
            this.Rig2D.velocity = new Vector2(0, 0);
            curHP -= 10;
            transform.position = respawn;
        }

        if (curHP <= 0) {
            Debug.Log("ゲームオーバー");
        }
    }
}
