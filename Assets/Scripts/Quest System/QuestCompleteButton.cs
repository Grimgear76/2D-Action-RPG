using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestCompleteButton : MonoBehaviour
{
    [SerializeField] private QuestLogUI questLogUI;
    [SerializeField] private string winScene = "MainMenu";
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void OnCompleteButtonClicked()
    {
        QuestSO quest = questLogUI.CurrentQuest;

        if (quest == null)
        {
            Debug.Log("No quest selected.");
            return;
        }

        if (!AllObjectivesComplete(quest))
        {
            Debug.Log("Quest not complete yet.");
            return;
        }

        GiveRewards(quest);
    }

    private bool AllObjectivesComplete(QuestSO quest)
    {
        foreach (var objective in quest.objectives)
        {
            int current = questLogUI.GetCurrentAmount(objective);
            if (current < objective.requiredAmount)
                return false;
        }
        return true;
    }

    private void GiveRewards(QuestSO quest)
    {
        int goldRewarded = 0;

        foreach (var reward in quest.rewards)
        {
            if (reward.itemSO.isGold)
                goldRewarded += reward.quantity;

            inventoryManager.AddItem(reward.itemSO, reward.quantity);
        }

        questLogUI.RemoveCurrentQuest(); // ADD THIS

        if (inventoryManager.gold > 999)
        {
            SceneManager.LoadScene(winScene);
            return;
        }

        Debug.Log($"Quest '{quest.questName}' completed! Gold rewarded: {goldRewarded}");
    }
}