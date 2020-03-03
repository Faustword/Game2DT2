using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyWall") == true) { 
             if(Enemy.turnAround == false)
             {
            Enemy.turnAround = true;
             }
            else
             {
               
                Enemy.turnAround = false;

             }
        }

        
    }
}
