using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyShotPlayer : MonoBehaviour
{
    private bool gunbut = false;//発射合図
    [Header("発射間隔")] public float cltm;//クールタイム
    private float count;//タイマー
    private bool ready=true;//再装填完了
    public bool Gunbut()
    {
        if (Input.GetKey(KeyCode.Space)&&ready)//ここ変えたら銃を撃つボタン変えられるよ
        {
            gunbut = true;
            ready = false;
        }
        else
        {
            gunbut = false;
        }
        return gunbut;
    }

    void Update()
    {
        if (ready == false)
        {
            count += Time.deltaTime;
            if (count >= cltm)
            {
                count = 0.0f;
                ready = true;
            }
        }
    }
}
