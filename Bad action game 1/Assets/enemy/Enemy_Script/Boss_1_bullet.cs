using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_bullet : MonoBehaviour
{
    [Header("スピード")] public float speed = 3.0f;
    [Header("最大移動距離")] public float maxDistance = 100.0f;
    [Header("左向き")] public bool isLeft = false;
    [Header("撃った人")] public GameObject shooter;

    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private Animator anim = null;
    private bool mark = false;
    private Vector3 plpos;
    private int isright = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
        if (!mark)
        {
            defaultPos = transform.position;
            plpos = shooter.transform.position;
            if (plpos.x - defaultPos.x <= 0)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
            if (isLeft)
            {
                this.transform.rotation = new Quaternion(0, 180, 0, 0);
                speed = -speed;
            }
            mark = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);

        //最大移動距離を超えている
        if (d > maxDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("進んでる");
            transform.position = new Vector2(speed, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            //anim.SetBool("burst", true);
            {
                Debug.Log("Hit");
            }
            Destroy(this.gameObject);
        }
    }
}
