using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private int detonateRange;
    [SerializeField] private int detonateTime;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private int damage;

    private bool inFlight = true;

    private bool deployed = false;

    private float distance = 0;
    private Vector2 landPosition = Vector2.zero;
    private float midpoint = 0;
    private float zSpeed = 1.0f;

    private float elapsedTime;

    public void Init(float distance, Vector2 landPosition)
    {
        this.distance = distance;
        this.landPosition = landPosition;
        midpoint = distance / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFlight) {
            float step = zSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, landPosition) > 0.2) {
                transform.position = Vector2.MoveTowards(transform.position, landPosition, step);
                UpdateSprite();
            } else {
                if (inFlight) {
                    Landed();
                }
            }
        } else {
            // Detonate after a small amount of time
            if (elapsedTime >= detonateTime) {
                // Play detonation animation
                Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(transform.position, detonateRange, targetMask);
                for (int i = 0; i < targetsInRange.Length; i++) {
                    targetsInRange[i].GetComponent<EntityHealth>().ApplyDamage(damage);
                }
            }
        }

        elapsedTime += Time.deltaTime;
    }

    public void Landed()
    {
        inFlight = false;
    }

    private void UpdateSprite()
    {
        var currentDistance = Vector2.Distance(transform.position, landPosition);
        var percentage = (currentDistance / distance);

        if (percentage > 0.5) {
            zSpeed -= 0.0025f;
            transform.localScale += new Vector3(0.0025f, 0.0025f, 0);
        } else if (percentage <= midpoint) {
            zSpeed += 0.0025f;
            transform.localScale -= new Vector3(0.0025f, 0.0025f, 0);
        }
    }
}