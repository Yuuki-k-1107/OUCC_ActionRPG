using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isground : MonoBehaviour
{
    [Header("エフェクト付きの床を判定するか？")] public bool checkPlatformGround = true;
    private string groundTag = "tile1";
    private string platformTag = "GroundPlatform";
    private string moveFloorTag = "MoveFloor";
    private string fallFloorTag = "FallFloor";
    private bool isGround = true;
    //    Animator anim1;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundExit = true;
        }
        else if (checkPlatformGround && (collision.tag == platformTag || collision.tag == moveFloorTag || collision.tag == fallFloorTag))
        {
            isGroundExit = true;
        }
    }
}
