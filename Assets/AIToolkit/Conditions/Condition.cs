using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : Behaviour {
    public abstract bool Test(AIAgent agent);

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (Test(agent)) {
            return eBehaviourResult.SUCCESS;
        }

        return eBehaviourResult.FAILURE;
    }
}
