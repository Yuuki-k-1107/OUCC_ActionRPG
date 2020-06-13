using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public int exp;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        exp = 0;
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpExp(int num)
    {
        exp += num;
    }

    public void ChangeHp(int num)
    {
        hp += num;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetEXP()
    {
        return exp;
    }

}
