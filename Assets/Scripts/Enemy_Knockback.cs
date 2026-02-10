using System.Collections;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_movement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_movement>();
    }

    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime, stunTime));

        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockbackForce;

    }
    
    IEnumerator StunTimer(float knoackbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knoackbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
