using UnityEngine;

public class damageManager : MonoBehaviour
{
    
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
            return zeroChange(EnemHP); //正の数　これを敵のHPに代入しないと敵にダメージが入りません
        }
        if (objectTag == "PlayerCloseAttack")
        {
            attack-= 1 + 4 / PlayerController.Level;
            if (attack > enemyDefense)
            {
                EnemHP -= (attack - enemyDefense);
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            return zeroChange(EnemHP);//正の数　これを敵のHPに代入しないと敵にダメージが入りません
        }
        if (objectTag == "burst")
        {
            attack = attack / 2;
            if (attack > enemyDefense)
            {
                EnemHP -= (attack - enemyDefense);  Debug.Log("あたった");
            }
            else EnemHP--; //詰み防止のために1ダメージを与えられるようにする
            return zeroChange(EnemHP);//正の数　これを敵のHPに代入しないと敵にダメージが入りません
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
        return zeroChange(EnemHP);//正の数　これを敵のHPに代入しないと敵にダメージが入りません
    }

    //EnemHPを0以下にしないようにする。
    private static int zeroChange(int value){
        if(value >= 0){
            return value;
        }else{
            return 0;
        }
    }
}
