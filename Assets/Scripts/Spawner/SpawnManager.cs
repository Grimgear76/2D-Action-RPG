using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject meleePrefab;
    public GameObject rangedPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    [Header("Tilemap Settings")]
    public Tilemap groundTilemap;

    [Header("Enemy Multipliers")]
    [Tooltip("Multiplies the max health of every spawned enemy. 1 = no change, 2 = double health.")]
    public float healthMultiplier = 1f;
    [Tooltip("Multiplies the damage of every spawned enemy. 1 = no change, 2 = double damage.")]
    public float damageMultiplier = 1f;

    private const int meleeWeight = 2;
    private const int rangedWeight = 1;
    private const int totalWeight = meleeWeight + rangedWeight;

    private float timer;
    private int currentEnemyCount;
    private List<Vector2> allSpawnPoints = new List<Vector2>();
    private List<Vector2> remainingSpawnPoints = new List<Vector2>();

    private void Start()
    {
        CacheValidTiles();
    }

    private void CacheValidTiles()
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        foreach (Vector3Int cellPos in bounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(cellPos))
            {
                Vector3 worldPos = groundTilemap.GetCellCenterWorld(cellPos);
                allSpawnPoints.Add(new Vector2(worldPos.x, worldPos.y));
            }
        }
        Debug.Log($"SpawnManager: Cached {allSpawnPoints.Count} valid spawn points.");
        Refill();
    }

    private void Refill()
    {
        remainingSpawnPoints = new List<Vector2>(allSpawnPoints);
        Shuffle(remainingSpawnPoints);
    }

    private void Shuffle(List<Vector2> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            TrySpawn();
            timer = 0f;
        }
    }

    private void TrySpawn()
    {
        if (remainingSpawnPoints.Count == 0)
        {
            Debug.Log("SpawnManager: All spots used, refilling.");
            Refill();
        }

        Vector2 spawnPos = remainingSpawnPoints[remainingSpawnPoints.Count - 1];
        remainingSpawnPoints.RemoveAt(remainingSpawnPoints.Count - 1);

        int roll = Random.Range(0, totalWeight);
        GameObject prefab = roll < meleeWeight ? meleePrefab : rangedPrefab;

        GameObject spawnedEnemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        ApplyMultipliers(spawnedEnemy);

        currentEnemyCount++;
    }

    private void ApplyMultipliers(GameObject enemy)
    {
        // Apply health multiplier
        Enemy_Health enemyHealth = enemy.GetComponent<Enemy_Health>();
        if (enemyHealth != null)
        {
            enemyHealth.maxHealth = Mathf.RoundToInt(enemyHealth.maxHealth * healthMultiplier);
            enemyHealth.currentHealth = enemyHealth.maxHealth; // Re-sync current to scaled max
        }

        // Apply damage multiplier
        Enemy_Combat enemyCombat = enemy.GetComponent<Enemy_Combat>();
        if (enemyCombat != null)
        {
            enemyCombat.damage = Mathf.RoundToInt(enemyCombat.damage * damageMultiplier);
        }
    }

    public void OnEnemyDied()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
        timer = 0f;
    }
}