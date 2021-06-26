using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static int Damage(string objectTag,int EnemHP,int enemyAttack, int enemyDefense)//返り値はswitchまたはifで使うことを想定しています。
    {
        int attack = PlayerController.Attack;
        if (objectTag == "PlayerShot")
        {

            if (attack > enemyDefense)
            {
                EnemHP -= (attack - enemyDefense);
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            return EnemHP;//正の数　これを敵のHPに代入しないと敵にダメージが入りません
        }
        if (objectTag == "PlayerCloseAttack")
        {
            attack-= 1 + 4 / PlayerController.Level;
            if (attack > enemyDefense)
            {
                EnemHP -= (attack - enemyDefense);
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            return EnemHP;//正の数　これを敵のHPに代入しないと敵にダメージが入りません
        }
        if (objectTag == "burst")
        {
            attack = attack / 2;
            if (attack > enemyDefense)
            {
                EnemHP -= (attack - enemyDefense);  Debug.Log("あたった");
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            return EnemHP;//正の数　これを敵のHPに代入しないと敵にダメージが入りません
        }
        if (objectTag == "Player")
        {
            if ((enemyAttack > PlayerController.Defense) && !PlayerController.isInvincible)
            {
                PlayerController.curHP -= (enemyAttack - PlayerController.Defense);
            }
            return -1;
        }
        return -2;
    }
    public static int Damage(int EnemHP, int enemyAttack, int enemyDefense, bool close)//返り値はswitchまたはifで使うことを想定しています。
    {
        int attack = PlayerController.Attack;
        if (close)
        {
            attack-= 1 + 4/PlayerController.Level;
        }
        if (attack > enemyDefense)
        {
            EnemHP -= (attack - enemyDefense);
        }
        else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
        return EnemHP;//正の数　これを敵のHPに代入しないと敵にダメージが入りません
    }
}
