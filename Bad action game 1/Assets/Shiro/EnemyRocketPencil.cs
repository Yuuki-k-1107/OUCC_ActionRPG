using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketPencil : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("接触判定")] public EnemyRocketPencilCol checkCollision;
    [Header("敵体力")] public int EnemHP = 10;
    [Header("敵を倒したときの経験値")] public int earnEXP = 3;
    [Header("敵の防御力")] public int enemyDefense = 0;
    [Header("攻撃オブジェクト")] public GameObject attackObj;
    [Header("攻撃間隔")] public float interval;
    [Header("撃破SE")] public AudioClip clip;
    [Header("射撃SE")] public AudioClip shotSE;
    [Header("画像差分")] public Sprite HPfull;
    public Sprite HP75per;
    public Sprite HP50per;
    public Sprite HP25per;
    public Sprite HP0per;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool isDead = false;
    private float deadTimer = 0.0f;
    private float timer;
    private int DefaultEnemHP;
    private Vector3 bulletPosition;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DefaultEnemHP = EnemHP;
        bulletPosition = attackObj.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {

            if (PlayerController.Attack > enemyDefense)
            {
                EnemHP -= (PlayerController.Attack - this.enemyDefense);
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            if ((float)EnemHP / (float)DefaultEnemHP > 0.75)
            {
                spriteRenderer.sprite = HPfull;
                bulletPosition = new Vector3(0.0f, 0.0f, 0.0f) + attackObj.transform.position; //弾の出る位置を口に調整
                col.size = new Vector2(0.4f, 0.12f); //コライダーを見た目通りに
                col.offset = new Vector2(0.04f, 0.0f);
            }
            if ((float)EnemHP / (float)DefaultEnemHP > 0.5)
            {
                spriteRenderer.sprite = HP75per;
                bulletPosition = new Vector3(0.0f, -0.75f, 0.0f) + attackObj.transform.position;
                col.size = new Vector2(0.4f, 0.9f);
                col.offset = new Vector2(0.04f, -0.15f);
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0.25)
            {
                spriteRenderer.sprite = HP50per;
                bulletPosition = new Vector3(0.0f, -1.5f, 0.0f) + attackObj.transform.position;
                col.size = new Vector2(0.4f, 0.6f);
                col.offset = new Vector2(0.04f, -0.3f);
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0)
            {
                spriteRenderer.sprite = HP25per;
                bulletPosition = new Vector3(0.0f, -2.25f, 0.0f) + attackObj.transform.position;
                col.size = new Vector2(0.4f, 0.3f);
                col.offset = new Vector2(0.04f, -0.45f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll; //場所固定
        if (timer > interval)
        {
            timer = 0.0f;
            //Debug.Log("撃った");
            GameObject g = Instantiate(attackObj);
            g.transform.SetParent(transform);
            g.transform.position = bulletPosition;
            g.SetActive(true);
            audioSource.PlayOneShot(shotSE, 0.01F);
        }
        else if (isDead) ;
        else
        {
            timer += Time.deltaTime;
        }
        if (EnemHP <= 0)
        {
            if (!isDead)
            {
                PlayerController.curEXP += earnEXP;
                //                anim.Play("dead");
                isDead = true;
                col.enabled = false;
                audioSource.PlayOneShot(clip, 0.8F);
                spriteRenderer.sprite = HP0per;
            }
            else
            {
                if (deadTimer > 4.0f)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    deadTimer += Time.deltaTime;
                }
            }
        }
    }
}

