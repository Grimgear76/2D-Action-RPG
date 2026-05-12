using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public TMP_Text healthText;
    public Animator healthTextAnim;
    public float timer;
    public float RegenInterval = 6f;

    private void Start()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }

    private void Update()
    {
        // missing any health
        if (StatsManager.Instance.currentHealth<StatsManager.Instance.maxHealth)
        {
            //starts timer
            timer += Time.deltaTime;

            //once timer reaches regen time
            if (timer >= RegenInterval)
            {
                //regen 1 health
                naturalHealthRegen();
                timer = 0f; //reset timer
            }
        }
        
    }

    private void naturalHealthRegen()
    {
        timer += Time.deltaTime;
        // every few seconds add health to player
        ChangeHealth(1);
    }


    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;

        if (StatsManager.Instance.currentHealth <= 0)
        {
            //death screen
            SceneManager.LoadScene("EndScreen");
        }
    }
}
