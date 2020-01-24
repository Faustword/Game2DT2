using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    public float jumpForce = 5f;
    private Rigidbody2D rigidbody;
    public LayerMask groundLayer;
    public Animator animator;
    public float runningSpeed = 1.5f;
    private Vector3 starPosition;


    void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        starPosition = this.transform.position;
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = starPosition;
        Debug.Log("Empieza en " + starPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame) { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        animator.SetBool("isGrounded", IsTouchingTheGround());
        }
    }

    void FixedUpdate()
    {
       if(GameManager.sharedInstance.currentGameState == GameState.inGame) { 
            if (rigidbody.velocity.x < runningSpeed)
        {
            rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y);
        }
        }

    }

    void Jump()
    {
        if (IsTouchingTheGround()){ 
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);
    }
}


