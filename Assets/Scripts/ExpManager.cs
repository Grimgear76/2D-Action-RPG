using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{

    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float exprGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action OnLevelUp;
    public static event Action OnReachedLevelOne;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }


    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * exprGrowthMultiplier);
        // add stats after leveling up
        StatsManager.Instance.LvlUp();

        //update every UI
        StatsUI.Instance.UpdateAllStats();

        OnLevelUp?.Invoke();

        // send event for level 1 reached
        if (level == 1)
        {
            OnReachedLevelOne?.Invoke();
        }
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
}
