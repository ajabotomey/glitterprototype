﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;

    public void ApplyDamage(int damage) {
        health -= damage;
    }

    public bool isDead() {
        return health <= 0;
    }
}
