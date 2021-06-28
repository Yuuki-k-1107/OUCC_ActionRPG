using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTekkyuu : MonoBehaviour
{
    [Header("�X�s�[�h")] public float speed = 3.0f;
    [Header("�ő�ړ�����")] public float maxDistance = 100.0f;
    [Header("�e�ۈЗ�")] public int bulletAttack = 5;
    [Header("�q�b�gSE")] public AudioClip PlayerDamagedSE;
    [Header("�d��")] public float gravity = 1.0f;
    //    [Header("������")] public bool isLeft = false;

    private Rigidbody2D rb;
    private Vector3 defaultPos;
    private bool mark = false;
    private float gravspeed = 0;

    //AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();
        if (rb == null)
        {
            //Debug.Log("�ݒ肪����܂���");
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
        /*if (!mark)
        {
                        if (isLeft)
                        {
                            this.transform.rotation = new Quaternion(0, 180, 0, 0);
                            speed = -speed;
                        } 
            mark = true;
        } */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, defaultPos);

        //�ő�ړ������𒴂��Ă���
        if (d > maxDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("�i��ł�");
            rb.MovePosition(transform.position += Vector3.left * Time.deltaTime * speed);
            rb.MovePosition(transform.position += Vector3.down * Time.deltaTime * gravspeed);
            gravspeed += gravity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            //anim.SetBool("burst", true);
            {
                //Debug.Log("Hit");
                if (collision.gameObject.tag == "Player")
                {
                    if ((bulletAttack > PlayerController.Defense) && !PlayerController.isInvincible)
                    {
                        PlayerController.curHP -= (this.bulletAttack - PlayerController.Defense);
                        AudioSource.PlayClipAtPoint(clip: PlayerDamagedSE, position: this.transform.position, volume: 0.5F);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
