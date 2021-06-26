using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(大部分を EnemyGun から流用　数値は適宜変更　攻撃に関するスクリプトは除去)
public class Damagewall : MonoBehaviour
{
     #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyGunCol checkCollision;
    [Header("敵体力")] public int EnemHP = 5;      //原因不明だが銃撃1発で物理判定のみ残ったまま消滅してしまうのでそれにHPを合わせる
    [Header("敵を倒したときの経験値")] public int earnEXP = 50;
    [Header("敵の攻撃力")] public int enemyAttack = 0;
    [Header("敵の防御力")] public int enemyDefense = 0;
    [Header("歩行しているかどうか")] public bool isWalk = true;
    [Header("攻撃オブジェクト")] public GameObject attackObj;
    [Header("攻撃間隔")] public float interval;
    [Header("撃破SE")] public AudioClip clip;
    [Header("射撃SE")] public AudioClip shotSE;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    private float deadTimer = 0.0f;
    private float moveTimer = 0.0f;
    private bool timeToBack;
    private float timer;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {

            if (PlayerController.Attack > enemyDefense)
            {
                EnemHP -= (PlayerController.Attack - this.enemyDefense);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if ((enemyAttack > PlayerController.Defense) && !PlayerController.isInvincible)
            {
                PlayerController.curHP -= (this.enemyAttack - PlayerController.Defense);
            }
        }
        //HPが0になったら消滅
        if (EnemHP <= 0)
        {
            PlayerController.curEXP += earnEXP;　　　//倒したら経験値獲得
                //                anim.Play("dead");
                isDead = true;
                col.enabled = false;
                audioSource.PlayOneShot(clip, 0.8F);
            //内壁を無効にする
            gameObject.SetActive (false);
        }
    }
}