using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static WeaponIndexContainer;


public class WeaponChange : MonoBehaviour
{
    private CloseWeapon ChangeCloseWeaponSequence()
    {
        var lastWeapon = CurrentCloseWeapon;

        var nextWeapon = lastWeapon switch
        {
            CloseWeapon.None => CloseWeapon.Spear,
            CloseWeapon.Spear => CloseWeapon.Bat,
            CloseWeapon.Bat => CloseWeapon.Spear,
            _ => CloseWeapon.None
        };
        SetCloseWeapon(nextWeapon);
        Debug.Log("近接武器変えたよ");
        return nextWeapon;
    }

    private ShootWeapon ChangeShootWeaponSequence()
    {
        var lastWeapon = CurrentShootWeapon;

        var nextWeapon = lastWeapon switch
        {
            ShootWeapon.None => ShootWeapon.Arrow,
            ShootWeapon.Arrow => ShootWeapon.Gun,
            ShootWeapon.Gun => ShootWeapon.ExpGun,
            ShootWeapon.ExpGun => ShootWeapon.Arrow,
            _ => ShootWeapon.None
        };
        SetShootWeapon(nextWeapon);
        Debug.Log("遠距離武器変えたよ");
        return nextWeapon;
    }

    private (WeaponType, uint) ChangeWeaponSequence()
    {
        var lastWeaponType = CurrentWeaponType;
        uint lastWeapon = lastWeaponType switch
        {
            WeaponType.Close => (uint)CurrentCloseWeapon,
            WeaponType.Shoot => (uint)CurrentShootWeapon,
            _ => 0,
        };

        (var nextWeaponType, uint nextWeapon) = (lastWeaponType, lastWeapon) switch
        {
            (WeaponType.Close, (uint)CloseWeapon.Bat) => (WeaponType.Shoot, (uint)ShootWeapon.Arrow),
            (WeaponType.Shoot, (uint)ShootWeapon.ExpGun) => (WeaponType.Close, (uint)CloseWeapon.Spear),
            (_, _) => (lastWeaponType, lastWeapon + 1)
        };
        SetWeapon(nextWeaponType, nextWeapon);
        Debug.Log("武器変えたよ");
        return (nextWeaponType, nextWeapon);
    }

    private WeaponType ChangeWeaponTypeSequence()
    {
        var lastWeapon = CurrentWeaponType;
        var nextWeapon = lastWeapon switch
        {
            WeaponType.Close => WeaponType.Shoot,
            WeaponType.Shoot => WeaponType.Close,
            _ => WeaponType.None
        };
        CurrentWeaponType = nextWeapon;
        return nextWeapon;
    }

    private void SetCloseWeapon(CloseWeapon closeWeapon)
    {
        CurrentCloseWeapon = closeWeapon;
        CurrentWeaponType = WeaponType.Close;
    }
    private void SetShootWeapon(ShootWeapon shootWeapon)
    {
        CurrentShootWeapon = shootWeapon;
        CurrentWeaponType = WeaponType.Shoot;
    }
    private void SetWeapon(WeaponType type, uint index)
    {
        CurrentWeaponType = type;
        switch (type)
        {
            default:
            case WeaponType.Close: CurrentCloseWeapon = index switch
                                    {
                                        // 1
                                        (uint)CloseWeapon.Spear => CloseWeapon.Spear,
                                        // 2
                                        (uint)CloseWeapon.Bat => CloseWeapon.Bat,
                                        _ => CloseWeapon.None,
                                    };
                                    break;
            case WeaponType.Shoot: CurrentShootWeapon = index switch
                                    {
                                        // 1
                                        (uint)ShootWeapon.Arrow => ShootWeapon.Arrow,
                                        // 2
                                        (uint)ShootWeapon.Gun => ShootWeapon.Gun,
                                        // 3
                                        (uint)ShootWeapon.ExpGun => ShootWeapon.ExpGun,
                                        _ => ShootWeapon.None,
                                    };
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
