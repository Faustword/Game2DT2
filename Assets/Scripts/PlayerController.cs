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

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 150, MAX_MANA = 25;
    public const int MIN_TIRED_HEALTH = 10;
    public const float MIN_SPEED = 2.5f, HEALTH_TIME_DECREASE = 2f;
    public const float SUPERJUMP_FORCE = 1.5f;
    public const int SUPERJUMP_COST = 3;

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

        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA;

        StartCoroutine("TirePlayer");
    }

    IEnumerator TirePlayer()
    {
        while (this.healthPoints > MIN_TIRED_HEALTH)
        {
            this.healthPoints--;
            yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
        }
        yield return null;
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
            Jump(true);
        }

        animator.SetBool("isGrounded", IsTouchingTheGround());
        }
    }

    void FixedUpdate()
    {
       if(GameManager.sharedInstance.currentGameState == GameState.inGame) {
            float currentSpeed = (runningSpeed - MIN_SPEED) * this.healthPoints / 100.0f;

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
                manaPoints -= SUPERJUMP_COST;
                rigidbody.AddForce(Vector2.up * jumpForce*SUPERJUMP_FORCE, ForceMode2D.Impulse);
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

        StopCoroutine("TirePlayer");
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

        if(this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;

        if(this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return this.healthPoints;
    }

    public int GetMana()
    {
        return this.manaPoints;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Enemy")
        {
            this.healthPoints -= 20;
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame && this.healthPoints <= 0)
        {
            Kill();
        }
    }

}


