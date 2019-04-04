using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeightedForce {
    public SteeringForce force;
    public float weight;

    public void SetWeight(float value) {
        weight = value;
    }
}

public abstract class SteeringForce {
    public abstract Vector2 GetForce(AIAgent agent);
}
