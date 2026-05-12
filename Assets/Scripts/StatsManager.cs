using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int maxHealth;
    public int currentHealth;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Auto-find the HealthText GameObject
        GameObject healthTextObj = GameObject.Find("HealthText");
        if (healthTextObj != null)
            healthText = healthTextObj.GetComponent<TMP_Text>();
        else
            Debug.LogWarning("HealthText GameObject not found in scene!");
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;

        statsUI.UpdateAllStats();
    }
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = "HP: " + currentHealth + "/ " + maxHealth;

        statsUI.UpdateAllStats();
    }
    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAllStats();
    }
    public void UpdateDamage(int amount)
    {
        damage += amount;
        statsUI.UpdateAllStats();
    }

    public void LvlUp()
    {
        weaponRange += .5f;
        damage += 1;
        speed += 1;
        maxHealth += 2;
        currentHealth += 2;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Update the health UI here, where healthText actually lives
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;


    }
}
