using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackItemCollisionChecker : MonoBehaviour
{

    // このオブジェクトに触れたときに取得する近接武器のindex
    [SerializeField]
    [Header("近接武器のid")]private uint CloseWeaponIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeaponIndexContainer.CurrentCloseWeapon = CloseWeaponIndex switch
            {
                // 1
                (uint)WeaponIndexContainer.CloseWeapon.Spear => WeaponIndexContainer.CloseWeapon.Spear,
                // 2
                (uint)WeaponIndexContainer.CloseWeapon.Bat => WeaponIndexContainer.CloseWeapon.Bat,
                _ => WeaponIndexContainer.CloseWeapon.None,
            };
            WeaponIndexContainer.CurrentWeaponType = WeaponIndexContainer.WeaponType.Close;
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
