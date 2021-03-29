using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
	public GameObject LaunchOfShot;
	public PlayerController playerController;
    public Vector2 jumpPower = new Vector2(0f, 1500.0f);
    public float speed;
    public float gravity;
    public float es;
    public int MaxHp = 20;
    [Header("攻撃間隔")] public float interval;
    [Header("1度に弾を発射する回数")] public int bullettimes = 5;
    [Header("地面に対する接触判定")] public DbottomCollisionCheck dbottomCollision;
    [Header("弾の速さ")] public float bulletSpeed = 10f;
	[Header("ダッシュ時の速さ")] public int DashMag = 2;
    public int Hp = 20;
    public EnemyCollisionCheck checkCollision;
    public int aiIfRUNTOPLAYER 			= 10;
	public int aiIfJUMPTOPLAYER 		= 10;
	public int aiIfESCAPE 				= 10;
    public int aiIfShot                 = 40;

    private bool isJump = false;
    private bool isDead = false;
    private bool isHit = false;
    private bool isGrounded = false;
    private string SpearTag = "spear";
    private string BatTag = "bat";
    private string PlayerShotTag = "PlayerShot";
    private float guntimer = 0.0f;
	private float deadTimer = 0.0f;
    private float guninterval = 0.7f;
    private float jumpStartTime = 0.0f;
    private float ySpeed;
    private float xSpeed = 0f;
    private float dir = 1.0f;
    private bool attackEnabled = false;
	private int	attackDamage 	= 0;
	private Vector2	attackNockBackVector = new Vector2(0.0f,0.0f);
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;

    public enum ENEMYAISTS // --- 敵のAIステート ---
    {
        ACTIONSELECT,		// アクション選択（思考）
        WAIT,				// 一定時間（止まって）待つ
        RUNTOPLAYER,		// 走ってプレイヤーに近づく
        JUMPTOPLAYER,		// ジャンプしてプレイヤーに近づく
        ESCAPE,				// プレイヤーから逃げる
        ATTACKONSIGHT,		// その場から移動せずに攻撃する（遠距離攻撃用）
        FREEZ,				// 行動停止（ただし移動処理は継続する）
    }

    public ENEMYAISTS 	aiState			= ENEMYAISTS.ACTIONSELECT;

    protected 	float				aiActionTimeLength		= 0.0f;
	protected 	float				aiActionTImeStart		= 0.0f;
	protected 	float				distanceToPlayer 		= 0.0f;
	protected 	float				distanceToPlayerPrev	= 0.0f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
		Hp = MaxHp;
        ySpeed = -gravity;
        dir = (transform.localScale.x > 0.0f) ? 1 : -1;
        transform.localScale = new Vector3(es * dir, es, transform.localScale.z);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
		if(!isHit){
			FixedUpdateAI();
			EndEnemyCommonWork();
			transform.localScale = new Vector3(es * dir, es, transform.localScale.z);
			rb.velocity = new Vector2(xSpeed, ySpeed);
		} else{
			Hp -= 1;
			rb.AddForce(new Vector2(100f,100f));
		}

		if(Hp <= 0){
			Dead();
		}

    }

    

    public  void FixedUpdateAI () {

		// AIステート
		switch (aiState) {
		case ENEMYAISTS.ACTIONSELECT	: // 思考の起点
			// アクションの選択
			int n = SelectRandomAIState();
			if (n < aiIfRUNTOPLAYER) {
				SetAIState(ENEMYAISTS.RUNTOPLAYER,3.0f);
			} else if (n < aiIfRUNTOPLAYER + aiIfJUMPTOPLAYER) {
				SetAIState(ENEMYAISTS.JUMPTOPLAYER,1.0f);
			} else if (n < aiIfRUNTOPLAYER + aiIfJUMPTOPLAYER + aiIfESCAPE) {
				SetAIState(ENEMYAISTS.ESCAPE,Random.Range(2.0f,5.0f));
			} else if(n < aiIfJUMPTOPLAYER + aiIfJUMPTOPLAYER + aiIfESCAPE + aiIfShot){
                SetAIState(ENEMYAISTS.ATTACKONSIGHT, Random.Range(1.0f,3.0f));
            }
            else
			{
				SetAIState(ENEMYAISTS.WAIT, Random.Range(0.0f,0.5f));
			}
			ActionMove (0.0f);
			break;

		case ENEMYAISTS.WAIT			: // 休憩
			ActionLookup(player,0.1f);
			ActionMove (0.0f);
			break;
			
		case ENEMYAISTS.RUNTOPLAYER		: // 近寄る
			if (!ActionMoveToNear(player,7.0f)) {
				RuntoPlayer();
			}
			break;
			
		case ENEMYAISTS.JUMPTOPLAYER	: // ジャンプで近寄る
			if (GetDistanePlayer() > 5.0f) {
				Attack_Jump();
			}  else {
				ActionLookup(player,0.1f);
				SetAIState(ENEMYAISTS.WAIT,1.0f);
			}
			break;

            case ENEMYAISTS.ATTACKONSIGHT   :
            //if(GetDistanePlayer() > 1.0f && GetDistanePlayer() < 7.0f){
                Attack_Fire();
            //} else {
                //ActionLookup(player,0.1f);
                //SetAIState(ENEMYAISTS.WAIT, 1.0f);
            //}
            break;
			
		case ENEMYAISTS.ESCAPE			: // 遠ざかる
			if (!ActionMoveToFar(player,7.0f)) {
				Escape();
			}
			break;
		}
	}

    public int SelectRandomAIState() {
		return Random.Range (0, 100 + 1);
	}

    public void EndEnemyCommonWork() {
		// アクションのリミット時間をチェック
		float time = Time.fixedTime - aiActionTImeStart;
		if (time > aiActionTimeLength) {
			aiState = ENEMYAISTS.ACTIONSELECT;
		}
	}

    public void SetAIState(ENEMYAISTS sts,float t) {
		aiState 			= sts;
		aiActionTImeStart  	= Time.fixedTime;
		aiActionTimeLength 	= t;
	}

    public void SetCombatAIState(ENEMYAISTS sts) {
		aiState 		  = sts;
		aiActionTImeStart = Time.fixedTime;
		ActionMove (0.0f);
	}

    public void ActionMove(float n) {
		if (n != 0.0f) {
			dir 	= Mathf.Sign(n);
		    xSpeed = speed * dir;
			anim.SetTrigger("Run");
		} else {
			xSpeed = 0;
			anim.SetTrigger("Idle");
		}
    }

    void RuntoPlayer() {
		ActionLookup(player,0.1f);
		xSpeed = speed * dir * DashMag;
		attackNockBackVector = new Vector2(1000.0f,100.0f);
		SetAIState(ENEMYAISTS.WAIT,0.2f);
	}

	void Escape() {
		ActionMove (0.0f);
		xSpeed = speed * dir * DashMag;
		SetAIState(ENEMYAISTS.WAIT,0.1f);
	}

	void Attack_Jump() {
		ActionLookup(player,0.1f);
		ActionMove (0.0f);
		attackEnabled = true;
		attackDamage 	= 1;
		attackNockBackVector = new Vector2(1000.0f,100.0f);
		ActionJump();
	}

    public bool ActionJump() {
		if (dbottomCollision.isOn && !isJump) {
			anim.SetTrigger ("Jump");
			rb.AddForce (jumpPower);
			isJump 		  = true;
			jumpStartTime = Time.fixedTime;
		}
		return isJump;
	}

    int i = 0;
    public void Attack_Fire(){
		while(i <= bullettimes) {
            //if(guntimer > guninterval){
			    ActionFire();
                //guntimer = 0;
                i++;
            //} else{
                //guntimer += Time.deltaTime;
            //}
		}
        attackNockBackVector = new Vector2(1000.0f,100.0f);
    }

    public void ActionFire() {
		Transform goFire = LaunchOfShot.transform;
		
		bullet = Instantiate (bullet,goFire.position,Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody2D>().AddForce(transform.forward * bulletSpeed, ForceMode2D.Force);
		
    }

	void Dead(){
        if (!isDead)
            {
                anim.Play("dead");
                isDead = true;
                capcol.enabled = false;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 5));
                if (deadTimer > 3.0f)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    deadTimer += Time.deltaTime;
                }
            }
		}

    public bool ActionLookup(GameObject go,float near) {
		if (Vector3.Distance(transform.position,go.transform.position) > near) {
			dir = (transform.position.x < go.transform.position.x) ? +1 : -1;
			return true;
		}
		return false;
	}


    public bool ActionMoveToNear(GameObject go,float near) {
		if (Vector3.Distance(transform.position,go.transform.position) > near) {
			ActionMove( (transform.position.x < go.transform.position.x) ? +1.0f : -1.0f );
			return true;
		}
		return false;
	}
	
	public bool ActionMoveToFar(GameObject go,float far) {
		if (Vector3.Distance(transform.position,go.transform.position) < far) {
			ActionMove( (transform.position.x > go.transform.position.x) ? +1.0f : -1.0f );
			return true;
		}
		return false;
	}

    public float GetDistanePlayer() {
		distanceToPlayerPrev = distanceToPlayer;
		distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);
		return distanceToPlayer;
	}



    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == SpearTag || collision.gameObject.tag == BatTag || collision.gameObject.tag == PlayerShotTag)
        {
            isHit = false;
        }
    }

}
