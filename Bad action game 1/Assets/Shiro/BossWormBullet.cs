using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyBulletと違って壁や敵を貫通、水平方向でなく初期状態の方向に進む

public class BossWormBullet : MonoBehaviour
{
    [Header("スピード")] public float speed = 7.0f;
    [Header("最大移動距離")] public float maxDistance = 50.0f;
    [Header("弾丸威力")] public int bulletAttack = 5;
    [Header("ヒットSE")] public AudioClip PlayerDamagedSE;
    [Header("ワ―ムの頭")] public GameObject WormHead;
    //    [Header("左向き")] public bool isLeft = false;

    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private Vector3 forward;
    private Animator anim = null;
    private float angle = 0;
    private float is2nd = 1;

    BossWormHead WormHeadScript;

    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();
        if (rb == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
        WormHeadScript = WormHead.GetComponent<BossWormHead>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);

        if (WormHeadScript.bodyNumPub == 2) is2nd = 1.2f;
        else is2nd = 1;

        //最大移動距離を超えている
        if (d > maxDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("進んでる");
            angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            forward = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
            rb.MovePosition(transform.position + forward * Time.deltaTime * speed * is2nd );
            //Debug.Log(forward);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Ground")
        {
            //anim.SetBool("burst", true);
            if (collision.gameObject.tag == "Player")
            {
                if ((bulletAttack > PlayerController.Defense) && !PlayerController.isInvincible)
                {
                    PlayerController.curHP -= (this.bulletAttack - PlayerController.Defense);
                    AudioSource.PlayClipAtPoint(clip: PlayerDamagedSE, position: this.transform.position, volume: 0.5F);
                    Destroy(this.gameObject);
                }
            }
            
        }
    }
}
