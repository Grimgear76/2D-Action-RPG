using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int currentHealth;
    public int maxHealth;
    public EnemySO enemyData;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnMonsterDefeated?.Invoke(expReward);

            GetComponent<GoldDrop>()?.TryDropGold();

            //added this 
            QuestManager questManager = FindAnyObjectByType<QuestManager>();
            if (questManager != null)
                questManager.NotifyEnemyKilled(enemyData);


            SpawnManager spawner = FindAnyObjectByType<SpawnManager>();
            if (spawner != null)
                spawner.OnEnemyDied();

            Destroy(gameObject);
        }
    }
}