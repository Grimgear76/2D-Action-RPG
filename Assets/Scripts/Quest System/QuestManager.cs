using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{

    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();

    [SerializeField] private List<QuestSO> startingQuests;

    private void Start()
    {
        foreach (var quest in startingQuests)
        {
            questProgress[quest] = new Dictionary<QuestObjective, int>();
        }
    }

    public void NotifyEnemyKilled(EnemySO killedEnemy)
    {

        foreach (var questSO in questProgress.Keys)
            UpdateObjectiveProgress(questSO, killedEnemy);
    }

    public void UpdateObjectiveProgress(QuestSO questSO, EnemySO killedEnemy)
    {
        if (!questProgress.ContainsKey(questSO))
            questProgress[questSO] = new Dictionary<QuestObjective, int>();

        var progressDictionary = questProgress[questSO];

        foreach (var objective in questSO.objectives)
        {
            Debug.Log($"Checking objective: {objective.description} | targetEnemy: {objective.targetEnemy} | killedEnemy: {killedEnemy}");

            if (objective.targetEnemy != killedEnemy)
            {
                Debug.Log($"SKIPPED - target does not match");
                continue;
            }

            if (!progressDictionary.ContainsKey(objective))
                progressDictionary[objective] = 0;

            if (progressDictionary[objective] < objective.requiredAmount)
                progressDictionary[objective]++;

            Debug.Log($"Progress updated: {progressDictionary[objective]} / {objective.requiredAmount}");
        }
    }




    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSO,objective);

        if (currentAmount >= objective.requiredAmount)
        {
            return "Complete";
        }
        else if (objective.targetEnemy != null)
        {
            return $"{currentAmount} / {objective.requiredAmount}";
        }
        else
        {
            return "In progress";
        }
    }

    public int GetCurrentAmount (QuestSO questSO, QuestObjective objective)
    {
        if(questProgress.TryGetValue(questSO, out var objectiveDictionary))
        {
            if (objectiveDictionary.TryGetValue(objective, out int amount))
            {
                return amount;
            }
        }
        return 0;
    }
}
