using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float speed = 3f;
    public float attackCooldown = 2;
    public float attackRange = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerlayer;

    private float attackCooldownTimer;
    private int facingDirection = -1;
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private EnemyState enemyState, newState;   //Controls Animations


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }


        if (enemyState == EnemyState.Chasing)    // Chasing Check
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Chase()
    {
        if (player.position.x > transform.position.x && facingDirection == -1 ||
               player.position.x < transform.position.x && facingDirection == 1)      //Used to flip direction
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;

    }

    void Flip()  //usedd to flip x
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()  // Used for Chasing Circle vision
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerlayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            //if player is in attack range And cooldown is ready
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0) //Checks if players Position < attackrange then changes to attack state
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
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
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("IsAttacking", false);

        enemyState = newState;

        //update the current Animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("IsIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("IsChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("IsAttacking", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}

public enum EnemyState   //State machine states
{

    Idle,
    Chasing,
    Attacking,

}
