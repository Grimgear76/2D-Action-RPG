using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;
    public SkillSO skillSO;

    public int currentLevel;
    public bool isUnlocked;

    public Image skillIcon;
    public Button skillButton;
    public TMP_Text skillLevelText;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    private void OnValidate()
    {
        if(skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }
    }

    public void TryUpgradeSkill()
    {
        if(isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            ApplySkillEffect();
            OnAbilityPointSpent?.Invoke(this);

            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    private void ApplySkillEffect()
    {
        if (skillSO.maxHealthBonus > 0)
        {
            // tries to add health
            StatsManager.Instance.UpdateMaxHealth(skillSO.maxHealthBonus);
            StatsManager.Instance.UpdateHealth(skillSO.maxHealthBonus);

        }

        if (skillSO.maxDamageBonus > 0)
        {
            // tries to add health
            StatsManager.Instance.UpdateDamage(skillSO.maxDamageBonus);
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if(isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }
    }
}
