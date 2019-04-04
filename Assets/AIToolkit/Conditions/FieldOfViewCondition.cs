using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewCondition : Condition
{
    private LayerMask targetMask;
    private LayerMask obstacleMask;
    private float viewRadius;
    private float viewAngle;

    public FieldOfViewCondition(LayerMask target, LayerMask obstacle, float radius, float angle) {
        targetMask = target;
        obstacleMask = obstacle;
        viewRadius = radius;
        viewAngle = angle;
    }

    public void Init(LayerMask target, LayerMask obstacle, float radius, float angle) {
        targetMask = target;
        obstacleMask = obstacle;
        viewRadius = radius;
        viewAngle = angle;
    }


    public override bool Test(AIAgent agent) {
        Transform agentTransform = agent.gameObject.transform;
        
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(agentTransform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = new Vector2(target.position.x - agentTransform.position.x, target.position.y - agentTransform.position.y);
            float angle = Vector2.Angle(dirToTarget, agentTransform.up);
            if (angle < viewAngle / 2) {
                float dstToTarget = Vector2.Distance(agentTransform.position, target.position);

                if (!Physics2D.Raycast(agentTransform.position, dirToTarget, dstToTarget, obstacleMask)) {
                    return true;
                }
            }
        }

        return false;
    }
}
