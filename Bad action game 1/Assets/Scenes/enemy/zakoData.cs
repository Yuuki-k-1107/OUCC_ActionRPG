using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakoData : MonoBehaviour
{
    public static int EnemHP;
    // Start is called before the first frame update
    void Start()
    {
      EnemHP = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HP:" + EnemHP);
        if(EnemHP <= 0)
        {
            Plyrctlr.curEXP += 3;
            Destroy(this.gameObject);
        }
    }
}
