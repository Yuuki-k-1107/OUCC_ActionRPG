using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBow : MonoBehaviour
{
    [Header("発射間隔")] public float cltm;//クールタイム
    [Header("弾丸")] public GameObject bulObj;//bullet object
                                            //    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメ")] public string stanim;//shoot anime
    [Header("発射点")] public GameObject stmrk;
    //    private Vector3 stpos;
    private Animator anim = null;
    //    private Rigidbody2D rb = null;
    private bool shot = false;
    private float count;//タイマー
    private bool ready = true;//再装填完了
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ready)
            {
                shot = true;
                //            stpos = stmrk.transform.position;//マーカーの位置を取得
                GameObject bullets = Instantiate(bulObj, stmrk.transform.position, Quaternion.identity) as GameObject;//マーカーと同じ位置に弾を生成
            }
            else
            {
                shot = false;
            }
        }
        else
        {
            shot = false;
        }

        if (ready == false)
        {
            count += Time.deltaTime;
            if (count >= cltm)
            {
                count = 0.0f;
                ready = true;
            }
        }
        anim = GetComponent<Animator>();
        anim.SetBool(stanim, shot);
        //        rb = GetComponent<Rigidbody2D>();
    }
}
