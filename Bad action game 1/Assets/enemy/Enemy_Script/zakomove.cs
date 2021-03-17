using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakomove : EnemyBase {
   private void FixedUpdate(){
       if(!isHit){
            Move(speed,gravity);
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
       } else{
            Dead();
       }
   }
}


