using UnityEngine;

public class keyItemDisplay : MonoBehaviour
{
    public Animator anim;
    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            

            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            

            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }

    //silences the playerInRange not being used warning
    private void silence()
    {
        if (playerInRange)
        {
            return;
        }
    }
}
