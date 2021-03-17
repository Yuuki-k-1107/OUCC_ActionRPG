using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_move : EnemyBase
{

    void Start(){
        isJump = true;
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            Move(speed,gravity);
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
        else
        {
            Dead();
        }
    }

}
