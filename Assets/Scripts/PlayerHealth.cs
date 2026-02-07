using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int MaxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        healthText.text = "HP: " + currentHealth + " / " + MaxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP: " + currentHealth + " / " + MaxHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
