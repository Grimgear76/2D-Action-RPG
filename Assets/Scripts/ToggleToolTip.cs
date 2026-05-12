using UnityEngine;

public class ToggleToolTip : MonoBehaviour
{
    public CanvasGroup toolTipCanvas;
    private bool toolTipOpen = false;

    private void Start()
    {
        HideToolTip();
    }

    private void OnEnable()
    {
        ExpManager.OnReachedLevelOne += ShowToolTip;
    }

    private void OnDisable()
    {
        ExpManager.OnReachedLevelOne -= ShowToolTip;
    }

    void Update()
    {
        if (toolTipOpen && Input.GetButtonDown("ToggleSkillTree"))
        {
            HideToolTip();
        }
    }

    private void HideToolTip()
    {
        toolTipCanvas.alpha = 0;
        toolTipCanvas.blocksRaycasts = false;
        toolTipCanvas.interactable = false;
        toolTipOpen = false;
    }

    private void ShowToolTip()
    {
        Time.timeScale = 0;
        toolTipCanvas.alpha = 1;
        toolTipCanvas.blocksRaycasts = true;
        toolTipCanvas.interactable = false; // was already false in original, keeping as-is
        toolTipOpen = true;
    }
}