using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.5f;
    [SerializeField] private int minGold = 1;
    [SerializeField] private int maxGold = 10;

    public void TryDropGold()
    {
        if (goldPrefab == null)
        {
            Debug.LogWarning("GoldDrop: No gold prefab assigned on " + gameObject.name);
            return;
        }

        if (Random.value <= dropChance)
        {
            int goldAmount = Random.Range(minGold, maxGold + 1);
            SpawnGold(goldAmount);
        }
    }

    private void SpawnGold(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnOffset = Random.insideUnitCircle * 0.5f;
            Instantiate(goldPrefab, (Vector2)transform.position + spawnOffset, Quaternion.identity);
        }
    }
}