using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Player variables
    public int health;
    public int maxHealth;
    public int currentWeapon;

    public PlayerData()
    {
        health = PlayerControl.instance.GetHealth();
        maxHealth = PlayerControl.instance.GetMaxHealth();
        currentWeapon = (int)WeaponControl.instance.CurrentWeapon;
    }
}
