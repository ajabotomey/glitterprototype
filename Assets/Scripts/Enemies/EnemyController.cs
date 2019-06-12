using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Move main enemy controls into this class

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [SerializeField] private FieldOfView[] enemies;

    [SerializeField] private LayerMask enemyMask;
    public Transform[] deathPoints;

    public Vector2 SoundPosition {
        get; set;
    }

    public FieldOfView[] GetEnemies()
    {
        return enemies;
    }

    void Start() {
        instance = this;
    }

    public int deathPointCount() {
        return deathPoints.Length;
    }

    public Vector2 chooseRandomDeathPoint() {
        int index = Random.Range(0, deathPoints.Length);

        return deathPoints[index].position;
    }

    public void ReactToSound(float soundRadius, Vector2 soundPosition)
    {
        SoundPosition = soundPosition;
        Collider2D[] guardsInRange = Physics2D.OverlapCircleAll(transform.position, soundRadius, enemyMask);
        // Select one or two guards depending on the amount of guards
        // Stick with one guard for the moment
        // Select the closest guard

        if (guardsInRange.Length == 0)
            return;

        int closestIndex = 0;
        float closestDistance = 99999;
        for (int i = 0; i < guardsInRange.Length; i++) {
            Vector2 position = guardsInRange[i].transform.position;
            float distance = Vector2.Distance(position, soundPosition);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        guardsInRange[closestIndex].gameObject.GetComponent<EnemyGuardNav>().InvestigateSound(soundPosition);
    }

    public bool CheckEnemyPositionToSound(Vector2 position)
    {
        return position == SoundPosition;
    }

    public void TurnOnFovVisualisation()
    {
        foreach (var enemy in enemies) {
            enemy.VisualisationOn = true;
        }
    }

    public void TurnOffFovVisualisation()
    {
        foreach (var enemy in enemies) {
            enemy.VisualisationOn = false;
        }
    }
}
