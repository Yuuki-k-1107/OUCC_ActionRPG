using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("弾速")] public float blspd = 3.0f;
    [Header("射程")] public float blrng = 100.0f;
    [Header("撃った人")] public GameObject shooter;
    [Header("射撃SE")] public AudioClip shotSE;
    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private Vector3 plpos;
    private int isright = 1;
    private Animator anim = null;
    private bool mark = false;
    private string player = "Player";//何かの間違いでプレイヤーにぶつかってもノーカン
    private string playershot = "PlayerShot";//弾同士のごっつんこもノーカン
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (rb == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        if (mark == false)
        {
            defaultPos = transform.position;
            mark = true;
            plpos = shooter.transform.position;
            if (plpos.x - defaultPos.x <= 0)
            {
                isright = -1;
            }
            else
            {
                isright = 1;
            }
            audioSource.PlayOneShot(shotSE);
        }
    }

    // Update is called once per frame
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
            this.gameObject.transform.Translate(blspd, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != player || collision.tag != playershot)
        {
            anim.SetBool("burst", true);
            if(collision.tag == "Enemy")
            Destroy(this.gameObject);
        }
    }
}
