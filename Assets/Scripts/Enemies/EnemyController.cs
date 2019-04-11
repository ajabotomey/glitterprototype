using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Move main enemy controls into this class

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public Transform[] deathPoints;

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
}
