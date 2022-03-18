using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WeaponIndexContainer;


public class WeaponChange : MonoBehaviour
{
    private uint ChangeCloseWeaponSequence()
    {
        uint lastWeapon = CloseWeaponIndex;
        // ShootWeaponIndex = (uint)ShootWeapon.None;

        uint nextWeapon = lastWeapon switch
        {
            (uint)CloseWeapon.None => (uint)CloseWeapon.Spear,
            (uint)CloseWeapon.Spear => (uint)CloseWeapon.Bat,
            (uint)CloseWeapon.Bat => (uint)CloseWeapon.Spear,
            _ => (uint)CloseWeapon.None
        };
        SetCloseWeapon(nextWeapon);
        Debug.Log("近接武器変えたよ");
        return nextWeapon;
        // count = CloseWeaponIndex + shootWeaponCount;
    }

    private uint ChangeShootWeaponSequence()
    {
        uint lastWeapon = ShootWeaponIndex;
        // CloseWeaponIndex = (uint)CloseWeapon.None;

        uint nextWeapon = lastWeapon switch
        {
            (uint)ShootWeapon.None => (uint)ShootWeapon.Arrow,
            (uint)ShootWeapon.Arrow => (uint)ShootWeapon.Gun,
            (uint)ShootWeapon.Gun => (uint)ShootWeapon.ExpGun,
            (uint)ShootWeapon.ExpGun => (uint)ShootWeapon.Arrow,
            _ => (uint)ShootWeapon.None
        };
        SetShootWeapon(nextWeapon);
        Debug.Log("遠距離武器変えたよ");
        return nextWeapon;
    }

    private (uint, uint) ChangeWeaponSequence()
    {
        uint lastWeaponType = WeaponTypeIndex;
        uint lastWeapon = lastWeaponType switch
        {
            (uint)WeaponType.Close => CloseWeaponIndex,
            (uint)WeaponType.Shoot => ShootWeaponIndex,
            _ => 0,
        };

        (uint nextWeaponType, uint nextWeapon) = (lastWeaponType, lastWeapon) switch
        {
            ((uint)WeaponType.Close, (uint)CloseWeapon.Bat) => ((uint)WeaponType.Shoot, (uint)ShootWeapon.Arrow),
            ((uint)WeaponType.Shoot, (uint)ShootWeapon.ExpGun) => ((uint)WeaponType.Close, (uint)CloseWeapon.Spear),
            (_, _) => (lastWeaponType, lastWeapon + 1)
        };
        SetWeapon(nextWeaponType, nextWeapon);
        Debug.Log("武器変えたよ");
        return (nextWeaponType, nextWeapon);
    }

    private uint ChangeWeaponTypeSequence()
    {
        uint lastWeapon = WeaponTypeIndex;
        uint nextWeapon = lastWeapon switch
        {
            (uint)WeaponType.Close => (uint)WeaponType.Shoot,
            (uint)WeaponType.Shoot => (uint)WeaponType.Close,
            _ => (uint)WeaponType.None
        };
        WeaponTypeIndex = nextWeapon;
        return nextWeapon;
    }

    private void SetCloseWeapon(uint index)
    {
        CloseWeaponIndex = index;
        WeaponTypeIndex = (uint)WeaponType.Close;
    }
    private void SetShootWeapon(uint index)
    {
        ShootWeaponIndex = index;
        WeaponTypeIndex = (uint)WeaponType.Shoot;
    }
    private void SetWeapon(uint type, uint index)
    {
        WeaponTypeIndex = type;
        switch (type)
        {
            default:
            case (uint)WeaponType.Close: CloseWeaponIndex = index;
                                        break;
            case (uint)WeaponType.Shoot: ShootWeaponIndex = index;
                                        break;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeWeaponSequence();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeShootWeaponSequence();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCloseWeaponSequence();
        }
    }
}
