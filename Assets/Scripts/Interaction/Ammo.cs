using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Interactable
{
    private bool ammoCollect;
    public GameObject ammo;
    protected override void Interact()
    {
        ammoCollect = !ammoCollect;
        Destroy(ammo);
        Debug.Log(("You collected ammo"));
    }
}
