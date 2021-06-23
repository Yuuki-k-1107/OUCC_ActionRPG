using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWormHead : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("活動開始までの時間")] public float timeToStart = 0.0f;
    [Header("バトルエリアの広さ(スポーン位置原点)")] public float battleAreaX = 18.0f;
    public float battleAreaY = 16.0f;
    [Header("重力")] public float gravity = 0.03f;
    [Header("敵体力")] public int EnemHP1st = 100;
    public int EnemHP2nd = 20;
    [Header("敵を倒したときの経験値")] public int earnEXP = 50;
    [Header("敵の近接攻撃力")] public int enemyAttack = 5;
    [Header("敵の防御力")] public int enemyDefense = 3;
    [Header("射撃確率 (0.002f以下推奨)")] public float attackDelay = 0.0006f;
    [Header("射撃オブジェクト")] public GameObject attackObj;
    [Header("撃破SE")] public AudioClip clip;
    [Header("射撃SE")] public AudioClip attackSE;
    [Header("攻撃SE")] public AudioClip bonkSE;
    [Header("画像差分")] public Sprite normal;

    public Sprite attack;
    public Sprite dead;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool isActive = false;
    private bool is1stForm = false;
    private bool is2ndForm = false;
    private bool isDead = false;
    private bool bonk = false;
    private bool attacking = false;
    private bool soundPlay = false;
    private int attackCount = 0;
    private int soundCount = 0;
    private bool attacked = false;
    private bool isLeft = false;
    private float timer=0f;
    private float timerSub = 0.0f;
    private float gravspeed = 0.0f;
    private float speed = 0.0f;
    private float speedAbs = 0.0f;
    public float speedAbsPublic
    {
        get { return this.speedAbs; }
        private set { this.speedAbs = value; } //BossWormBodyと値共有用、読み取り専用
    }
    private float speedHorizon;
    private float speedVertical;
    private int DefaultEnemHP;
    private int EnemHP;
    private int bodyNumber = 7; //頭と尻尾だけ=2,最大7
    public int bodyNumPub
    {
        get { return this.bodyNumber; }
        private set { this.bodyNumber = value; } //BossWormBodyと値共有
    }
    private Vector3 defaultPos;
    private Vector3 latestPos;
    private Vector3 deadPos;
    private float battleAreaYmin;
    private float battleAreaYmax;
    private float battleAreaXmin;
    private float battleAreaXmax;    

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
        DefaultEnemHP = EnemHP1st;
        EnemHP = EnemHP1st;
        defaultPos = transform.position;
        latestPos = transform.position;

        battleAreaYmin = defaultPos.y - (battleAreaY * 7.5f / 10); //ワ―ムは地面より下にいってほしいよね
        battleAreaYmax = defaultPos.y + (battleAreaY / 2);
        battleAreaXmin = defaultPos.x - (battleAreaX / 2);
        battleAreaXmax = defaultPos.x + (battleAreaX / 2);
        speedVertical = Mathf.Sqrt((battleAreaYmax - battleAreaYmin) * gravity * 2) * 6.5f; //v0 = root(2gh),天井まで行くときの初速度
        speedHorizon = (battleAreaXmax - battleAreaXmin) * gravity * 6.5f * 6.0f / (2 * speedVertical); //t=2v0/g, 天井まで投げるときはエリアを横断できる水平速度
        //なんかそのままじゃ速度足りない(未解決)ので6.0fとか6.5fとかかけちゃった
    }

    public void RelayOnTriggerEnter(Collider2D collision)
    {
        //Bodyの方でコライダー衝突
        if (collision.gameObject.tag == "PlayerShot")
        {

            if (PlayerController.Attack > enemyDefense)
                EnemHP -= (PlayerController.Attack - this.enemyDefense);
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
        }
        if (collision.gameObject.tag == "Player")
        {
            if ((enemyAttack > PlayerController.Defense) && !PlayerController.isInvincible)
            {
                PlayerController.curHP -= (this.enemyAttack - PlayerController.Defense);
                audioSource.PlayOneShot(bonkSE, 0.8F);
                bonk = true;
                Debug.Log("bonk");
                spriteRenderer.sprite = attack;
            }
        }
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
        }
        if (collision.gameObject.tag == "Player")
        {
            if ((enemyAttack > PlayerController.Defense) && !PlayerController.isInvincible)
            {
                PlayerController.curHP -= (this.enemyAttack - PlayerController.Defense);
                audioSource.PlayOneShot(bonkSE, 0.8F);
                bonk = true;
                Debug.Log("bonk!");
                spriteRenderer.sprite = attack;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerSub += Time.deltaTime;
        if ((timer > timeToStart) && !isActive)
        {
            //Debug.Log("WormActivated");
            is1stForm = true;
            isActive = true;
            timer = 0.0f;
        }   
 
        if (bonk && timer>5.0f)
        {
            bonk = false; //攻撃モーション解除
            spriteRenderer.sprite = normal;
            timer = 0.0f;
        }

        if (attacking && !isDead)
        {
            if (attackCount > 5)
            {
                attacking = false;
                attacked = true;
                spriteRenderer.sprite = normal;
            }
            else if (timer > 0.5f)
            {
                timer = 0.0f;
                //3wayのスマートな実装わからん
                GameObject g1 = Instantiate(attackObj);
                g1.transform.SetParent(transform.parent.gameObject.transform);
                g1.transform.position = attackObj.transform.position;
                g1.transform.rotation = transform.rotation;
                Vector3 rotationAngles1 = g1.transform.rotation.eulerAngles; //角度を編集するときはオイラー角にしようね(n敗)
                rotationAngles1.z -= 90.0f; //ワ―ムの頭下向きなのにbullet左向きだからね……
                g1.transform.rotation = Quaternion.Euler(rotationAngles1);
                g1.transform.localScale *= 4;
                g1.SetActive(true);

                GameObject g2 = Instantiate(attackObj);
                g2.transform.SetParent(transform.parent.gameObject.transform);
                g2.transform.position = attackObj.transform.position;
                g2.transform.rotation = transform.rotation;
                Vector3 rotationAngles2 = g2.transform.rotation.eulerAngles; //角度を編集するときはオイラー角にしようね(n敗)
                rotationAngles2.z -= 135.0f; //ワ―ムの頭下向きなのにbullet左向きだからね……
                g2.transform.rotation = Quaternion.Euler(rotationAngles2);
                g2.transform.localScale *= 4;
                g2.SetActive(true);

                GameObject g3 = Instantiate(attackObj);
                g3.transform.SetParent(transform.parent.gameObject.transform);
                g3.transform.position = attackObj.transform.position;
                g3.transform.rotation = transform.rotation;
                Vector3 rotationAngles3 = g3.transform.rotation.eulerAngles; //角度を編集するときはオイラー角にしようね(n敗)
                rotationAngles3.z -= 45.0f; //ワ―ムの頭下向きなのにbullet左向きだからね……
                g3.transform.rotation = Quaternion.Euler(rotationAngles3);
                g3.transform.localScale *= 4;
                g3.SetActive(true);

                audioSource.PlayOneShot(attackSE, 0.1F);
                attackCount++;
            }
        }

        if (is1stForm)
        {
            if ((float)EnemHP / (float)DefaultEnemHP > 0.8)
            {
                bodyNumber = 7;
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0.6)
            {
                bodyNumber = 6;
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0.4)
            {
                bodyNumber = 5;
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0.2)
            {
                bodyNumber = 4;
            }
            else if ((float)EnemHP / (float)DefaultEnemHP > 0)
            {
                bodyNumber = 3;
            }
            else if ((float)EnemHP / (float)DefaultEnemHP <= 0)
            {
                bodyNumber = 2;
                is1stForm = false;
                is2ndForm = true;
                EnemHP = EnemHP2nd;
                speedHorizon *= 1.5f;
                speedVertical *= 1.2f; //暴れる
            }
            if ((Random.value < attackDelay) && !attacked)
            {
                attacking = true;
                attackCount = 0;
                spriteRenderer.sprite = attack;
            }
        }

        if (is2ndForm)
        {
            if (EnemHP <= 0)
            {
                is2ndForm = false;
                isDead = true;
                //audioSource.PlayOneShot(clip, 0.8F);
                deadPos = transform.position;
                timer = 0.0f;
                bodyNumber = 0;
            }
            if (Random.value < attackDelay)
            {
                attacking = true;
                attackCount = 0;
                spriteRenderer.sprite = attack;
            }
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x <= battleAreaXmin)
            isLeft = false;
        if (transform.position.x >= battleAreaXmax)
            isLeft = true;
        if (transform.position.y >= battleAreaYmax)
            gravspeed = (-1) * gravspeed; //*bonk*
        if (transform.position.y <= battleAreaYmin)
        {
            gravspeed = speedVertical * (0.8f + Random.value * 0.2f); //最大速度の0.5~1倍
            speed = speedHorizon * (0.8f + Random.value * 0.2f);
            if (Random.value > 0.5) isLeft = true;
            else isLeft = false;
            attacked = false;
            if (isDead)
            {
                Destroy(this.gameObject);
            }
        }
        int isLeftInt;
        if (isLeft)
        {
            isLeftInt = 1;
            //Debug.Log("left");
        }
        else
        {
            isLeftInt = -1;
            //Debug.Log("righhhhhhhhht");
        }
        rb.MovePosition(transform.position + Vector3.up * Time.deltaTime * gravspeed + Vector3.left * Time.deltaTime * speed * isLeftInt );
        speedAbs = Mathf.Sqrt(speed * speed + gravspeed * gravspeed);
        gravspeed -= gravity;//F
        Vector2 diff = transform.position - latestPos;
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.down, diff);
            latestPos = transform.position;
        }

        
        
        if (isDead)
        {  
            spriteRenderer.sprite = dead;
            if (timer >= 3.0f)
            {
                rb.MovePosition(deadPos + Vector3.down * (timer - 3.0f) * 5 );
            }
            else transform.position = deadPos;
            if (soundPlay && soundCount <= 7)
            {
                audioSource.PlayOneShot(clip, 0.6F);
                soundCount++;
                soundPlay = false;
            }
            else if (timerSub >= 0.5f)
            {
                timerSub = 0.0f;
                soundPlay = true;
            }
        }
    }
}
