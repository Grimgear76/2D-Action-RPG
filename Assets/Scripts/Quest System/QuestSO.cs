using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestSO", menuName = "QuestSO")]
public class QuestSO : ScriptableObject
{

    public string questName;
    [TextArea] public string questDescription;
    public int questLevel;

    public List<QuestObjective> objectives;
    public List<QuestReward> rewards;
}


[System.Serializable]
public class QuestObjective
{
    public string description;

    [SerializeField] private Object target;
    public EnemySO targetEnemy => target as EnemySO; // for killing enemies

    public int requiredAmount;
    
}

[System.Serializable]
public class QuestReward
{
    public ItemSO itemSO;
    public int quantity;
}
