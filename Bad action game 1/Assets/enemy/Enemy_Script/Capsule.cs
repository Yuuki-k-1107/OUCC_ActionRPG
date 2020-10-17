using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("地面の接触判定")] public EnemyCollisionCheck groundcheckCollision;
    [Header("壁の接触判定")] public EnemyCollisionCheck wallcheckCollision;
    [Header("敵のサイズ")] public float es;
    [Header("回転速度")] public float rotspeed;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    private bool walltheft = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotspeed);

    } }
