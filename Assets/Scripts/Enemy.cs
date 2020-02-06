using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float runningSpeed = 1.5f;
    public static bool turnAround;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if(turnAround == true)
        {
            currentRunningSpeed = runningSpeed;
            transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidbody.velocity = new Vector2(currentRunningSpeed, rigidbody.velocity.y); 
        }
    }

}
