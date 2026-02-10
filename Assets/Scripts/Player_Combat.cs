using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask enemylayer;
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
            anim.SetBool("isAttacking", true);

            timer = cooldown;
        }
    }

    public void Dealdamge()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemylayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
        }

    }

    public void FinishAttcking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }

}
