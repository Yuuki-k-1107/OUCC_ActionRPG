using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

using static WeaponIndexContainer;

public class ShootManager : MonoBehaviour
{
    
    [Header("発射間隔gun")] private readonly float cltm_gun = 0.3f;//クールタイム
    [SerializeField][Header("弾丸gun")] private GameObject bulObj_gun;//bullet object
                                            //    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメgun")] private readonly string stanim_gun = "ShotGun";//shoot anime
    [Header("発射間隔arrow")] private readonly float cltm_arrow = 0.7f;//クールタイム
    [SerializeField][Header("弾丸arrow")] private GameObject bulObj_arrow;//bullet object
                                            //    [Header("弾速")] public float blspd = 3.0f;
    [Header("発射アニメarrow")] private readonly string stanim_arrow = "ShotBow";//shoot anime
    [SerializeField][Header("発射点")] private GameObject stmrk;

    
    [Header("発射アニメexplosionGun")] private readonly string stanim_expGun = "ShotExpGun";
    [Header("発射間隔explosionGun")] private readonly float cltm_expGun = 0.5f;
    [SerializeField][Header("弾丸explosionGun")] private GameObject bulObj_expGun;
    


    private Animator anim = null;
    private bool hasShootWeapon;

    private class BulletComposition
    {
        public float CoolTime{get; set;}
        public GameObject BulletObject{get; set;}
        public string AnimParam{get; set;}
    }


    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => hasShootWeapon && Input.GetKeyDown(KeyCode.Space));

            var bullet = Shoot();
            anim.SetBool(bullet.AnimParam, true);

            yield return new WaitForSeconds(bullet.CoolTime);

            anim.SetBool(bullet.AnimParam, false);
        }

    }

    private BulletComposition Shoot()
    {
        uint weaponIndex = ShootWeaponIndex;
        var bullet = weaponIndex switch
        {
            (uint)ShootWeapon.Arrow => new BulletComposition{CoolTime = cltm_gun,
                                        BulletObject = bulObj_gun,
                                        AnimParam = stanim_gun,},
            (uint)ShootWeapon.Gun => new BulletComposition{CoolTime = cltm_arrow,
                                        BulletObject = bulObj_arrow,
                                        AnimParam = stanim_arrow,},
            (uint)ShootWeapon.ExpGun => new BulletComposition{CoolTime = cltm_expGun,
                                        BulletObject = bulObj_expGun,
                                        AnimParam = stanim_expGun,},
            _ => new BulletComposition{CoolTime = 0,
                                        BulletObject = null,
                                        AnimParam = "",}
        };

        if (bullet.BulletObject != null)
        {
            Instantiate(bullet.BulletObject, stmrk.transform.position, Quaternion.identity);
        }
        return bullet;

    }




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ShootCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        hasShootWeapon = ShootWeaponIndex != (uint)ShootWeapon.None && WeaponTypeIndex == (uint)WeaponType.Shoot;
    }
}

