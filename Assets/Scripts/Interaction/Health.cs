using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Interactable
{
    private bool healthCollect;
    public GameObject health;
    
    protected override void Interact()
    {
        healthCollect = !healthCollect;
        Destroy((health));
        
        Debug.Log(("You collected health"));
    }
}
