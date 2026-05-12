using UnityEngine;

public class ToggleQuestCanvas : MonoBehaviour
{
    private bool QuestCanvasOpen = false;
    public CanvasGroup QuestCanvas;

    [SerializeField] private QuestLogUI questLogUI;

    private void Start()
    {
        //HideQuestCanvas();
    }


    void Update()
    {
        if (QuestCanvasOpen && Input.GetButtonDown("ToggleQuestCanvas"))
        {
            HideQuestCanvas();
        }
        else if (!QuestCanvasOpen && Input.GetButtonDown("ToggleQuestCanvas"))
        {
            ShowQuestCanvas();
        }
    }

    private void HideQuestCanvas()
    {
        Time.timeScale = 1;
        QuestCanvas.alpha = 0;
        QuestCanvas.blocksRaycasts = false;
        QuestCanvas.interactable = false;
        QuestCanvasOpen = false;
    }

    private void ShowQuestCanvas()
    {
        Time.timeScale = 0;
        QuestCanvas.alpha = 1;
        QuestCanvas.blocksRaycasts = true;
        QuestCanvas.interactable = true;
        QuestCanvasOpen = true;

        questLogUI.toggleQuestStart();
    }
}

