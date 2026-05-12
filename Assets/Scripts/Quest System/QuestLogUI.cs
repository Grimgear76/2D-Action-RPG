using UnityEngine;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;

    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private QuestObjectiveSlot[] objectiveSlots;
    [SerializeField] private QuestRewardSlot[] rewardSlots;
    [SerializeField] private QuestLogSlot[] questSlots;

    private QuestSO questSO;

    
    public QuestSO CurrentQuest => questSO;
    public int GetCurrentAmount(QuestObjective objective) => questManager.GetCurrentAmount(questSO, objective);

    private void Start()
    {
        toggleQuestStart();
    }

    public void toggleQuestStart()
    {
        if (questSlots.Length > 0 && questSlots[0].currentQuest != null)
            HandleQuestClicked(questSlots[0].currentQuest);
    }

    public void HandleQuestClicked(QuestSO questSO)
    {
        Debug.Log("This");
        this.questSO = questSO;
        questNameText.text = questSO.questName;
        questDescriptionText.text = questSO.questDescription;

        DisplayObjectives();
        DisplayRewards();
    }

    private void DisplayObjectives()
    {
        for (int i = 0; i < objectiveSlots.Length; i++)
        {
            if (i < questSO.objectives.Count)
            {
                var objective = questSO.objectives[i];
                //questManager.UpdateObjectiveProgress(questSO, objective); // dont know if need keep

                int currentAmount = questManager.GetCurrentAmount(questSO, objective);
                string progress = questManager.GetProgressText(questSO, objective);
                bool isComplete = currentAmount >= objective.requiredAmount;

                objectiveSlots[i].gameObject.SetActive(true);
                objectiveSlots[i].RefreshObjectives(objective.description, progress, isComplete);
            }
            else
            {
                objectiveSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisplayRewards()
    {
        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if(i < questSO.rewards.Count)
            {
                var reward = questSO.rewards[i];
                rewardSlots[i].DisplayReward(reward.itemSO.icon, reward.quantity);
                rewardSlots[i].gameObject.SetActive(true);

            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void RemoveCurrentQuest()
    {
        foreach (var slot in questSlots)
        {
            if (slot.currentQuest == questSO)
            {
                slot.gameObject.SetActive(false);
                break;
            }
        }

        questSO = null;
        questNameText.text = "";
        questDescriptionText.text = "";

        foreach (var slot in objectiveSlots)
            slot.gameObject.SetActive(false);

        foreach (var slot in rewardSlots)
            slot.gameObject.SetActive(false);

        // Auto select next available quest
        foreach (var slot in questSlots)
        {
            if (slot.gameObject.activeSelf && slot.currentQuest != null)
            {
                HandleQuestClicked(slot.currentQuest);
                return;
            }
        }
    }
}
