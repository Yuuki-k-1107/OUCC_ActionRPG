using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndexContainer
{


    public static uint CloseWeaponIndex{get; set;} = 0;

    public enum CloseWeapon
    {
        None,
        Spear,
        Bat
    }
    public static uint ShootWeaponIndex{get; set;} = 0;
    public enum ShootWeapon
    {
        None,
        Arrow,
        Gun,
        ExpGun
    }

    public static uint WeaponTypeIndex{get; set;} = 0;

    public enum WeaponType
    {
        None,
        Close,
        Shoot
    }


}
