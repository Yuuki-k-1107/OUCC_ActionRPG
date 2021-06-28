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
                WepomIndexContainer.ShootWeponIndex = count;
                WepomIndexContainer.CloseWeponIndex = 0;
            }
            else
            {
                WepomIndexContainer.CloseWeponIndex = count - shootWeponCount;
                WepomIndexContainer.ShootWeponIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(WepomIndexContainer.CloseWeponIndex > 0)
            {
                WepomIndexContainer.CloseWeponIndex = 0;
            }
            WepomIndexContainer.ShootWeponIndex++;
            Debug.Log("武器変えたよ");
            if(WepomIndexContainer.ShootWeponIndex > shootWeponCount)
            {
                WepomIndexContainer.ShootWeponIndex = 1;
            }
            count = WepomIndexContainer.ShootWeponIndex;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (WepomIndexContainer.ShootWeponIndex > 0)
            {
                WepomIndexContainer.ShootWeponIndex = 0;
            }
            WepomIndexContainer.CloseWeponIndex++;
            Debug.Log("武器変えたよ");
            if (WepomIndexContainer.CloseWeponIndex > closeWeponCount)
            {
                WepomIndexContainer.CloseWeponIndex = 1;
            }
            count = WepomIndexContainer.CloseWeponIndex + shootWeponCount;
        }
    }
}
