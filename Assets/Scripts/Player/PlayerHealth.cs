using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
   private float health;
   private float lerpTimer;
   [Header("Health Bar")]
   public int maxHealth = 100;
   public float chipSpeed = 2f;
   public Image frontHealthBar;
   public Image backHealthBar;
   public TextMeshProUGUI healthText;

   [Header("Damage BG")] 
   public Image overlay;
   public float duration;
   public float fadeSpeed;

   private float durationTimer;  //duration checker

   public void Start()
   {
      health = maxHealth;
      overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
   }

    public void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //fading
                float Alpha = overlay.color.a;
                Alpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, Alpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
            if (fillFront < hFraction)
            {
                backHealthBar.color = Color.green;
                backHealthBar.fillAmount = hFraction;
                lerpTimer += Time.deltaTime;
                 float PercentComplete = lerpTimer / chipSpeed;
                 PercentComplete = PercentComplete * PercentComplete;
                frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
            }

            healthText.text = health + "/" + maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        Debug.Log("girdim" + health);

    }
}
