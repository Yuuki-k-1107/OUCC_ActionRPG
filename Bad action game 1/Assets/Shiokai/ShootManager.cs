using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    
    [Header("発射間隔gun")] public float cltm_gun;//クールタイム
    [Header("弾丸gun")] public GameObject bulObj_gun;//bullet object
                                            //    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメgun")] public string stanim_gun;//shoot anime
    [Header("発射間隔arrow")] public float cltm_arrow;//クールタイム
    [Header("弾丸arrow")] public GameObject bulObj_arrow;//bullet object
                                            //    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメarrpw")] public string stanim_arrow;//shoot anime
    [Header("発射点")] public GameObject stmrk;

    [Header("gunSE")] public AudioClip clipgun;
    [Header("arrowSE")] public AudioClip cliparrow;
    AudioSource audioSource;
    private AudioClip clip;

    //    private Vector3 stpos;
    private Animator anim = null;
    //    private Rigidbody2D rb = null;
    private bool shot = false;
    private float count;//タイマー
    private bool ready = true;//再装填完了
    private float count2;

    // public int shootIndex = 0;
    private float cltm;
    private GameObject bulObj;
    private string stanim;


    private int wepindex = 0;



    private void SetCurrentParamOfWeponIndex(int id)
    {
        switch(id)
        {
            case 1:
                cltm = cltm_gun;
                bulObj = bulObj_gun;
                stanim = stanim_gun;
                anim.SetBool(stanim_arrow, false);
                clip = clipgun;
                break;
            case 2:
                cltm = cltm_arrow;
                bulObj = bulObj_arrow;
                stanim = stanim_arrow;
                anim.SetBool(stanim_gun, false);
                clip = cliparrow;
                break;
            default:
                cltm = 0;
                bulObj = null;
                stanim = null;
                clip = null;
                break;
        }
    }







    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        wepindex = WepomIndexContainer.ShootWeponIndex;

        SetCurrentParamOfWeponIndex(wepindex);

        if (wepindex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && ready)
            {

                shot = true;
                ready = false;
                //            stpos = stmrk.transform.position;//マーカーの位置を取得
                //                GameObject bullets = Instantiate(bulObj, stmrk.transform.position, Quaternion.identity) as GameObject;//マーカーと同じ位置に弾を生成
                audioSource.PlayOneShot(clip);

            }
            else
            {
                //            shot = false;
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
            // anim = GetComponent<Animator>();

            anim.SetBool(stanim, shot);
            //        rb = GetComponent<Rigidbody2D>();
        }


    }
}

