using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakomove : MonoBehaviour {
    #region//インスペクター
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    #endregion

    #region//private変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private bool rightTleftF = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            EnemHP -= (4+Plyrctlr.Level);
            Debug.Log("Hit!");
            if(EnemHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sr.isVisible|| nonVisibleAct)
        {
            int xVector = -1;
            if (rightTleftF)
            {
                xVector = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb.velocity = new Vector2(xVector * speed, -gravity);
        }
    }
}
