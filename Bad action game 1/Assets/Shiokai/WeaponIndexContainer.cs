using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndexContainer
{


    public static CloseWeapon CurrentCloseWeapon{get; set;} = CloseWeapon.None;

    public enum CloseWeapon
    {
        None,
        Spear,
        Bat
    }
    public static ShootWeapon CurrentShootWeapon{get; set;} = ShootWeapon.None;
    public enum ShootWeapon
    {
        None,
        Arrow,
        Gun,
        ExpGun
    }

    public static WeaponType CurrentWeaponType{get; set;} = WeaponType.None;

    public enum WeaponType
    {
        None,
        Close,
        Shoot
    }


}
