using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidPathCondition : Condition {

    public override bool Test(AIAgent agent) {
        var path = new Pathfinding.List();
        if (agent.GetBlackboard().Get("Path", ref path)) {
            return path.Length != 0;
        }

        return false;
    }
}
