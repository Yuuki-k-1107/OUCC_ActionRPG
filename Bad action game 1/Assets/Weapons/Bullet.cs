using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("弾速")] public float bulspd = 3.0f;//bullet speed
    [Header("射程")] public float bulrng = 100.0f;//bullet range
    private Rigidbody2D rb;
    private Vector3 dfpos;//default position
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.Log("設定どっかで抜けてる");
            Destroy(this.gameObject);
        }
        dfpos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position, dfpos);
        //射程に達する
        if (d > bulrng)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rb.MovePosition(transform.position += Vector3.right * Time.deltaTime * bulspd);
        }
    }
    private void OntriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
