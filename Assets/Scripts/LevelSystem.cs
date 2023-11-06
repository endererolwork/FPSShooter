using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXP;
    public float requiredXP;

    private float lerpTimer;
    private float delayTimer;

    public Image frontXPBar;
    public Image backXpBar;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI talentText;

    public int talentPoints = 0;
    public GameObject[] buttons;
    public void Start()
    {
        frontXPBar.fillAmount = currentXP / requiredXP;
        backXpBar.fillAmount = currentXP / requiredXP;
        levelText.text = "Level " + level;

        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

    public void Update()
    {
        UpdateXpUI();
        if (currentXP > requiredXP)
        {
            LevelUp();
        }

        if (talentPoints > 0)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
            
        }
        else
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXP / requiredXP;
        float FXP = frontXPBar.fillAmount;

        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXPBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }

        xpText.text = currentXP + "/" + requiredXP;
    }

    public void GainExp(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontXPBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        GetComponent<PlayerAttack>().IncreaseMaxAmmo();
        levelText.text = "Level " + level;
        talentPoints++;
        talentText.text = "Talent Points : " + talentPoints;
        
    }

    public void DecrementTalentPoints()
    {
        talentPoints--;
    }
}