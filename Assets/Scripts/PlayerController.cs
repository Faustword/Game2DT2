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
    private Vector3 startPosition;
    private int healthPoints, manaPoints;


    void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = startPosition;
        Debug.Log("Empieza en " + startPosition);

        this.healthPoints = 100;
        this.manaPoints = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame) { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
               // if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump(true);
                }

        }

        animator.SetBool("isGrounded", IsTouchingTheGround());
        }
    }

    void FixedUpdate()
    {
       if(GameManager.sharedInstance.currentGameState == GameState.inGame) {
            float currentSpeed = (runningSpeed - 0.5f) * this.healthPoints / 100.0f;

            if (rigidbody.velocity.x < currentSpeed)
        {
            rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);
        }
        }

    }

    void Jump(bool isSuperJump)
    {
        if (IsTouchingTheGround())
        { 
            if(isSuperJump && this.manaPoints > 5)
            {
                rigidbody.AddForce(Vector2.up * jumpForce*1.5f, ForceMode2D.Impulse);
            } else
            {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
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

        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0);
        if(currentMaxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }
    }

    public float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x,0),
                                                   new Vector2(this.transform.position.x,0)
                                                   );
        return travelledDistance;
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;

        if(this.healthPoints >= 150)
        {
            this.healthPoints = 150;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;

        if(this.manaPoints >= 25)
        {
            this.manaPoints = 25;
        }
    }
}


