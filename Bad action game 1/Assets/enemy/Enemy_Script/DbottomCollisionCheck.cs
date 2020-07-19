using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DbottomCollisionCheck : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;

    private string groundTag = "Ground";
    private string playerTag = "Player";


    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == playerTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == playerTag)
        {
            isOn = false;
        }
    }
    #endregion
}

