using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponChange : MonoBehaviour
{
    [SerializeField] [Header("遠距離武器の数")] private int shootWeaponCount = 4;
    [SerializeField] [Header("近距離武器の数")] private int closeWeaponCount = 2;
    private int weaponCount;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        weaponCount = shootWeaponCount + closeWeaponCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            count++;
            if(count > weaponCount)
            {
                count = 1;
            }
            if (count <= shootWeaponCount)
            {
                WeaponIndexContainer.ShootWeaponIndex = count;
                WeaponIndexContainer.CloseWeaponIndex = 0;
            }
            else
            {
                WeaponIndexContainer.CloseWeaponIndex = count - shootWeaponCount;
                WeaponIndexContainer.ShootWeaponIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(WeaponIndexContainer.CloseWeaponIndex > 0)
            {
                WeaponIndexContainer.CloseWeaponIndex = 0;
            }
            WeaponIndexContainer.ShootWeaponIndex++;
            Debug.Log(WeaponIndexContainer.ShootWeaponIndex);
            Debug.Log("武器変えたよ");
            if(WeaponIndexContainer.ShootWeaponIndex > shootWeaponCount)
            {
                WeaponIndexContainer.ShootWeaponIndex = 1;
            }
            count = WeaponIndexContainer.ShootWeaponIndex;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (WeaponIndexContainer.ShootWeaponIndex > 0)
            {
                WeaponIndexContainer.ShootWeaponIndex = 0;
            }
            WeaponIndexContainer.CloseWeaponIndex++;
            Debug.Log("武器変えたよ");
            if (WeaponIndexContainer.CloseWeaponIndex > closeWeaponCount)
            {
                WeaponIndexContainer.CloseWeaponIndex = 1;
            }
            count = WeaponIndexContainer.CloseWeaponIndex + shootWeaponCount;
        }
    }
}
