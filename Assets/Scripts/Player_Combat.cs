using UnityEngine;

public class Player_Combat : MonoBehaviour
{
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

    public void FinishAttcking()
    {
        anim.SetBool("isAttacking", false);
    }

}
