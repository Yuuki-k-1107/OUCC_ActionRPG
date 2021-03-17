using UnityEngine;
using System.Collections;

public class mob_AI : MonoBehaviour {

    public GameObject firePrefab;

    private int move_type = 0;
    private Vector3 forward;
    
    public bool isIdle = true;
    public bool isAttack = false;
    public bool isWalk = false;
    public bool isJump = false;

    // JumpParams
    private float jumpPowor;
    public float jumpPoworConst = 0.8f;
    public float jumpGrvity = 0.05f;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {

        //アイドル状態
        if(isIdle){
            // ランダムでモーション種類基準となる番号を取得
            move_type = Random.Range(0, 4);

        // 歩くフラグが立っていたら
        }else if(isWalk){
            
            //playerオブジェクトをを取得して向きを決定
            GameObject player2 = GameObject.Find("player");
            Vector3 forward = player2.transform.position - transform.position;

            // 向きに対してモーションを行なう&向きも変える
            if(forward.x > 0){
                transform.Translate(Vector3.right * 0.1f);
                transform.localScale = new Vector3(-2, 2, 2);
            }else{
                transform.Translate(Vector3.left * 0.1f);
                transform.localScale = new Vector3(2, 2, 2);
            }
            return;

        // ジャンプフラグが立っていたら
        }else if(isJump){
            //ジャンプ力を計算
            jumpPowor = jumpPowor - jumpGrvity;
            transform.Translate(Vector3.up * jumpPowor);
            //地面に着地したら処理処理終了
            if(jumpPowor < 0 && transform.position.y <= 1){
                isIdle = true;
                isJump = false;
            }
            return;
        }else{
            return;
        }

        //Attack
        if(move_type == 1){
            isIdle = false;
            isAttack = true;

            //攻撃オブジェクトを生成
            GameObject fire = Instantiate(firePrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, 1), Quaternion.identity) as GameObject;

            // 攻撃オブジェクトのタグを変える
            fire.gameObject.tag = "mob";

            // 攻撃向きと飛ばす方向を決める
            GameObject player2 = GameObject.Find("player");
            Vector3 forward = player2.transform.position - transform.position;

            if(forward.x > 0){
                fire.gameObject.SendMessage("setDirection", true);
                transform.localScale = new Vector3(-2, 2, 2);
            }else{
                fire.gameObject.SendMessage("setDirection", false);
                transform.localScale = new Vector3(2, 2, 2);
            }

            //スリープ
            StartCoroutine("WaitFotAttack");
        }

        // Walk
        if(move_type == 2){
            isIdle = false;
            isWalk = true;
            StartCoroutine("WaitFotWalk");
        }

        
        // Jump
        if(move_type == 3){
            isIdle = false;
            isJump = true;
            jumpPowor = jumpPoworConst;
        }

    }
    
    IEnumerator WaitFotAttack()
    {
        yield return new WaitForSeconds(2.0f);
        isIdle = true;
        isAttack = false;
    }

    IEnumerator WaitFotWalk()
    {
        yield return new WaitForSeconds(0.5f);
        isIdle = true;
        isWalk = false;
    }
}