using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask enemyLayer;
    public float knockbackForce = 10;
    public float knockbackTime = .15f;
    public float stunTime = .3f;
    public int damage = 1;
    
    


    public Animator anim;

    public float cooldown = 2;
    private float timer;

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);  //bool from animator

            timer = cooldown;
        }
    }

    public void Dealdamge()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.isTrigger) continue;
        }

            if (enemies.Length > 0) //Gets list of enemies in hitbox and calls their ChangeHelath and Knockback methods
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
        }

    }

    public void FinishAttcking()
    {
        anim.SetBool("isAttacking", false);  //Called in animation event
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }

}
