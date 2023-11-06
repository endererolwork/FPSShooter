using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   public TextMeshProUGUI ammoText;

   public int gunDMG;
   public int ammoMax;
   public int currentAmmo;
   public int pierceShot;
   public float pierceCooldown = 30f;
   private bool isPierceOnCooldown = false;
   
   private void Start()
   {
      
   }

   private void Update()
   {
      ammoText.text = currentAmmo + "/" + ammoMax;
   }

   public void IncreaseMaxAmmo()
   {
      ammoMax = ammoMax + 3;
   }
   
   public void DecrementAmmo()
   {
      currentAmmo -= 1;
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
