using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //    [Header("発射点")] public GameObject stmrk;
    [Header("弾速")] public float blspd = 3.0f;
    [Header("射程")] public float blrng = 100.0f;
    [Header("撃った人")] public string str;
    private GameObject shooter;
    private Rigidbody2D rb;
    //    private Rigidbody2D strb;
    private Vector3 defaultPos;
    private Vector3 plpos;
    private Animator anim = null;
    private bool mark = false;
    //    private int isright = 1;
    //    private string player = "Player";//何かの間違いでプレイヤーにぶつかってもノーカン
    //    private string playershot = "PlayerShot";//弾同士のごっつんこもノーカン
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shooter = GameObject.Find(str);
        //        strb = shooter.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }

    }

    void FixedUpdate()
    {
        if (mark == false)
        {
            defaultPos = this.transform.position;
            plpos = shooter.transform.position;
            if (defaultPos.x >= plpos.x)
            {
                Debug.Log("0");
            }
            else
            {
                Debug.Log("180");
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                blspd = -blspd;
            }
            mark = true; Vector3
            force = new Vector3(blspd, 3.0f, 0.0f);    // 力を設定
            rb.AddForce(force, ForceMode2D.Impulse);  // 力を加える
        }
        float d = Vector3.Distance(transform.position, defaultPos);
        //最大移動距離を超えている
        if (d > blrng)
        {
            Destroy(this.gameObject);
        }
        else
        {
//            rb.velocity = new Vector3(blspd, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerShot")
        {
            Destroy(this.gameObject);
        }
    }
}
