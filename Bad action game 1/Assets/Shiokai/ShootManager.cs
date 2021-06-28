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
    [Header("発射アニメarrow")] public string stanim_arrow;//shoot anime
    [Header("発射点")] public GameObject stmrk;

    
    [Header("発射アニメexplosionGun")] public string stanim_expGun;
    [Header("発射間隔explosionGun")] public float cltm_expGun;
    [Header("弾丸explosionGun")] public GameObject bulObj_expGun;
    

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


    private int weapindex = 0;



    private void SetCurrentParamOfWeaponIndex(int id)
    {
        switch(id)
        {
            case 1:
                cltm = cltm_gun;
                bulObj = bulObj_gun;
                stanim = stanim_gun;
                anim.SetBool(stanim_arrow, false);
                anim.SetBool(stanim_expGun, false);
                break;
            case 2:
                cltm = cltm_arrow;
                bulObj = bulObj_arrow;
                stanim = stanim_arrow;
                anim.SetBool(stanim_gun, false);
                anim.SetBool(stanim_expGun, false);
                break;

            case 3:
                cltm = cltm_expGun;
                bulObj = bulObj_expGun;
                stanim = stanim_expGun;
                anim.SetBool(stanim_arrow, false);
                anim.SetBool(stanim_gun, false);
                break;
            default:
                cltm = 0;
                bulObj = null;
                stanim = null;
                break;
        }
    }







    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        weapindex = WeaponIndexContainer.ShootWeaponIndex;

        SetCurrentParamOfWeaponIndex(weapindex);

        if (weapindex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && ready)
            {
                
                shot = true;
                ready = false;
                //            stpos = stmrk.transform.position;//マーカーの位置を取得
                //                GameObject bullets = Instantiate(bulObj, stmrk.transform.position, Quaternion.identity) as GameObject;//マーカーと同じ位置に弾を生成

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

