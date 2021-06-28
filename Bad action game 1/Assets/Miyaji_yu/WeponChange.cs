using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeponChange : MonoBehaviour
{
    
    [Header("遠距離武器の数")] public static int shootWeponCount = 3;
    [Header("近距離武器の数")] public static int closeWeponCount = 2;
    private int weponCount;
    public static int count;

    // Start is called before the first frame update
    void Start()
    {
        weponCount = shootWeponCount + closeWeponCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            count++;
            if(count > weponCount)
            {
                count = 1;
            }
            if (count <= shootWeponCount)
            {
                WeponIndexContainer.ShootWeponIndex = count;
                WeponIndexContainer.CloseWeponIndex = 0;
            }
            else
            {
                WeponIndexContainer.CloseWeponIndex = count - shootWeponCount;
                WeponIndexContainer.ShootWeponIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(WeponIndexContainer.CloseWeponIndex > 0)
            {
                WeponIndexContainer.CloseWeponIndex = 0;
            }
            WeponIndexContainer.ShootWeponIndex++;
            Debug.Log("武器変えたよ");
            if(WeponIndexContainer.ShootWeponIndex > shootWeponCount)
            {
                WeponIndexContainer.ShootWeponIndex = 1;
            }
            count = WeponIndexContainer.ShootWeponIndex;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (WeponIndexContainer.ShootWeponIndex > 0)
            {
                WeponIndexContainer.ShootWeponIndex = 0;
            }
            WeponIndexContainer.CloseWeponIndex++;
            Debug.Log("武器変えたよ");
            if (WeponIndexContainer.CloseWeponIndex > closeWeponCount)
            {
                WeponIndexContainer.CloseWeponIndex = 1;
            }
            count = WeponIndexContainer.CloseWeponIndex + shootWeponCount;
        }
    }
}
