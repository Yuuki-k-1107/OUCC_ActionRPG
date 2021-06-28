using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotExpGun : MonoBehaviour
{
    [Header("発射間隔")] public float cltm;//クールタイム
    [Header("弾丸")] public GameObject bulObj;//bullet object
//    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメ")] public string stanim;//shoot anim
    [Header("発射点")] public GameObject stmrk;
    //    private Vector3 stpos;
    private Animator anim = null;
    //    private Rigidbody2D rb = null;
    private bool shot = false;
    private float count;//タイマー
    private bool ready = true;//再装填完了
    private float count2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)&&ready)
        {

            shot = true;
            ready = false;

        }

        if (ready == false)
        {
            count += Time.deltaTime;
            if (count >= cltm)
            {
                count = 0.0f;
                GameObject bullets = Instantiate(bulObj, stmrk.transform.position, Quaternion.identity) as GameObject;
                ready = true;
            }
        }
        if (ready)
        {
            count2 += Time.deltaTime;
            if (count2 >= cltm)
            {
                count2 = 0.0f;
                if (ready)
                {
                    shot = false;
                }
            }
        }
        anim = GetComponent<Animator>();
        anim.SetBool(stanim, shot);
    }
}
