using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootItemCollisionChecker : MonoBehaviour
{

    // このオブジェクトに触れたときに取得する近接武器のindex
    [SerializeField]
    [Header("遠距離武器のid")]private uint ShootWeaponIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeaponIndexContainer.CurrentShootWeapon = ShootWeaponIndex switch
            {
                // 1
                (uint)WeaponIndexContainer.ShootWeapon.Arrow => WeaponIndexContainer.ShootWeapon.Arrow,
                // 2
                (uint)WeaponIndexContainer.ShootWeapon.Gun => WeaponIndexContainer.ShootWeapon.Gun,
                // 3
                (uint)WeaponIndexContainer.ShootWeapon.ExpGun => WeaponIndexContainer.ShootWeapon.ExpGun,
                _ => WeaponIndexContainer.ShootWeapon.None,
            };
            WeaponIndexContainer.CurrentWeaponType = WeaponIndexContainer.WeaponType.Shoot;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
