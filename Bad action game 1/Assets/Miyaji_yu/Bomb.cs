using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //    [Header("発射点")] public GameObject stmrk;
    [Header("弾速")] public float blspd = 3.0f;
    [Header("射程")] public float blrng = 100.0f;
    [Header("撃った人")] public string str;
    [Header("エフェクト")] public GameObject burst_effect;
    private GameObject shooter;
    private Rigidbody2D rb;
    //    private Rigidbody2D strb;
    private Vector3 defaultPos;
    private Vector3 plpos;
    private Animator anim = null;
    private bool mark = false;
    private float count = 0.0f; //存在している時間をカウント
    [SerializeField] [Tooltip("爆発までの時間")] private float maxCount = 1.0f;

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
            force = new Vector3(blspd, 6.0f, 0.0f);    // 力を設定
            rb.AddForce(force, ForceMode2D.Impulse);  // 力を加える
            
        }

    }

    void FixedUpdate()
    {
        
        float d = Vector3.Distance(transform.position, defaultPos);
        //最大移動距離を超えている
        if (d > blrng)
        {
            Destroy(this.gameObject);
        }
        else
        {

        }
    }

    private void Update() 
    {
        count += Time.deltaTime;
        if(count > maxCount)
        {
            burst();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "PlayerShot")
        {
            {
                zakoData.EnemHP -= 3;
                Debug.Log("Hit");
            }
            burst();
        }
    }

    private void burst(){
        GameObject burstObj = Instantiate(burst_effect, this.transform.position, this.transform.rotation) as GameObject;
        CircleCollider2D col = burstObj.GetComponent<CircleCollider2D>();
        col.radius = 0.3f;
        Destroy(this.gameObject);
    }
}