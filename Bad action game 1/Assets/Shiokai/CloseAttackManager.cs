using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

using static WeaponIndexContainer;

public class CloseAttackManager : MonoBehaviour
{
    

    [Header("攻撃アニメspear")] private readonly string stanim_spear = "Spear";
    private readonly float coolTime_spear = 0.15f;
   
    [Header("攻撃アニメbat")] private readonly string stanim_bat = "Bat";
    private readonly float coolTime_bat = 0.19f;
    [SerializeField][Header("当たり判定")] private GameObject clmrk;
    private Animator anim = null;
    //    private Rigidbody2D rb = null;

    // private Vector3 cltrans;
    // private readonly int direction = 1;
    private bool hasCloseWeapon;

    private class CloseWeaponComposition
    {
        public float CoolTime{get; set;}
        public GameObject ColliderObject{get; set;}
        public string AnimParam{get; set;}
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => hasCloseWeapon && Input.GetKeyDown(KeyCode.Space));
        
            var closeWeapon = Attack();
            anim.SetBool(closeWeapon.AnimParam, true);

            yield return new WaitForSeconds(closeWeapon.CoolTime);

            anim.SetBool(closeWeapon.AnimParam, false);
        }

    }

    private CloseWeaponComposition Attack()
    {
        uint weaponIndex = CloseWeaponIndex;
        var closeWeapon = weaponIndex switch
        {
            (uint)CloseWeapon.Bat => new CloseWeaponComposition{CoolTime = coolTime_bat,
                                                                ColliderObject = clmrk,
                                                                AnimParam = stanim_bat},
            (uint)CloseWeapon.Spear => new CloseWeaponComposition{CoolTime = coolTime_spear,
                                                                ColliderObject = clmrk,
                                                                AnimParam = stanim_spear},
            _ => new CloseWeaponComposition{CoolTime = 0,
                                            ColliderObject = null,
                                            AnimParam = "",}
        };
        if (closeWeapon.ColliderObject != null)
        {
            var cltrans = transform.position + Vector3.right;
            // cltrans.x += direction;
            Instantiate(clmrk, cltrans, transform.rotation);
        }
        return closeWeapon;
    }




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // direction = 1;
        StartCoroutine(AttackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        hasCloseWeapon = CloseWeaponIndex != (uint)CloseWeapon.None && WeaponTypeIndex == (uint)WeaponType.Close;
    }
}

