using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_move : EnemyBase
{

    // Update is called once per frame
    void FixedUpdate()
    {
        canJumping = true;
        if(!isHit){
            Move(speed, gravity);
            rb.velocity = velocity;
        }
        else {
            Dead();
        }
    }
}
