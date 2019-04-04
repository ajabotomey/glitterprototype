using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeForce : SteeringForce {
    AIAgent m_target;

    public FleeForce(AIAgent target = null) {
        m_target = target;
    }

    public void SetTarget(AIAgent target) {
        m_target = target;
    }

    public override Vector2 GetForce(AIAgent agent) {
        Vector2 targetPos = m_target.gameObject.transform.position;
        Vector2 agentPos = agent.gameObject.transform.position;

        Vector2 diff = agentPos - targetPos;
        float distance = Vector2.Distance(agentPos, targetPos);
        if (distance > 0) {
            distance = Mathf.Sqrt(distance);

            diff.x /= distance;
            diff.y /= distance;
        }

        float maxForce = 0;
        agent.GetBlackboard().Get("MaxForce", ref maxForce);

        return new Vector2(diff.x * maxForce, diff.y * maxForce);
    }
}
