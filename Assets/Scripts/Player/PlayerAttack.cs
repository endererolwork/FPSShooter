using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   public TextMeshProUGUI ammoText;
   public TextMeshProUGUI pierceShotText;

   public int gunDMG;
   public int ammoMax;
   public int currentAmmo;
   public int pierceShot = 1;
   public float pierceCooldown = 30f;
   private bool isPierceOnCooldown = false;
   private float pierceTimer = 0f;
   
   
   private void Start()
   {
      
   }

   private void Update()
   {
      if (isPierceOnCooldown)
      {
         pierceTimer += Time.deltaTime;
         if (pierceTimer >= pierceCooldown)
         {
            isPierceOnCooldown = false;
            pierceShot = 1;
            pierceTimer = 0f;
         }
      }
      ammoText.text = currentAmmo + "/" + ammoMax;
      pierceShotText.text = "Pierce Shot : " + pierceShot;
   }

   public void IncreaseMaxAmmo()
   {
      ammoMax = ammoMax + 3;
   }
   
   public void DecrementAmmo()
   {
      currentAmmo -= 1;
   }

   public void DecrementPierceShot()
   {
      pierceShot = 0;
      isPierceOnCooldown = true;
   }

   public void RestoreAmmo()
   {
      currentAmmo += 10;
      if (currentAmmo > ammoMax)
      {
         currentAmmo = ammoMax;
      }
   }
}
