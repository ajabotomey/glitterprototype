using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekForce : SteeringForce {
    AIAgent m_target;

    public SeekForce(AIAgent target = null) {
        m_target = target;
    }

    public void SetTarget(AIAgent target) {
        m_target = target;
    }

    public override Vector2 GetForce(AIAgent agent) {
        Vector3 targetPos = m_target.gameObject.transform.position;
        Vector3 agentPos = agent.gameObject.transform.position;

        Vector3 diff = targetPos - agentPos;
        float distance = Vector3.Distance(agentPos, targetPos);
        if (distance > 0) {
            distance = Mathf.Sqrt(distance);

            diff.x /= distance;
            diff.z /= distance;
        }

        float maxForce = 0;
        agent.GetBlackboard().Get("MaxForce", ref maxForce);

        return new Vector2(diff.x * maxForce, diff.z * maxForce);
    }
}
