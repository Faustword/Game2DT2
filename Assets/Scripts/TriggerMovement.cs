using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movingForward;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(movingForward == true)
        {
            Enemy.turnAround = true;
        }
        else
        {
            Enemy.turnAround = false;
        }
    }
}
