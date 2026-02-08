using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float speed = 3f;
    private int facingDirection = -1;
    private EnemyState enemyState, newState;   //Controls Animations

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start()
    {
     
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (enemyState == EnemyState.Chasing)    // Chasing Check
        {
            if(player.position.x > transform.position.x && facingDirection == -1 ||
               player.position.x < transform.position.x && facingDirection == 1)      //Used to flip direction
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;  
        }
    }

    void Flip()  //usedd to flip x
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)   // Used for Chasing Circle vision
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            ChangeState(EnemyState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)   // Turns off chasing hwne plaayer out of range
    {
        if (collision.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            player = null;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)    //State Machine for Enemy animations
    {   
        //exit the current animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("IsIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("IsChasing", false);

        enemyState = newState;

        //update the current state
        if (enemyState == EnemyState.Idle)
            anim.SetBool("IsIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("IsChasing", true);
    }
}

public enum EnemyState   //State machine states
{

    Idle,
    Chasing,

}
