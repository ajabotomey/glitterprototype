using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private EntityHealthBar healthBar;

    void Start()
    {
        if (healthBar) {
            healthBar.Init(health);
        }
    }

    public void ApplyDamage(int damage) {

        if (isDead())
            return;

        health -= damage;

        if (healthBar) {
            healthBar.TakeDamage(damage);
        }
    }

    public bool isDead() {
        return health <= 0;
    }
}
