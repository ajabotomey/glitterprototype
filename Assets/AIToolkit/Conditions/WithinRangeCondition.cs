using UnityEngine;

public class WithinRangeCondition : Condition {
    private AIAgent m_target;
    private float m_range;

    public WithinRangeCondition(AIAgent target, float range) {
        m_target = target;
        m_range = range;
    }

    public void Init(AIAgent target, float range) {
        m_target = target;
        m_range = range;
    }

    public override bool Test(AIAgent agent) {
        // Get Target Position
        Vector3 targetPos = m_target.gameObject.transform.position;

        // Get Agent Position
        Vector3 agentPos = agent.gameObject.transform.position;

        // Get the distance
        float distance = Vector3.Distance(agentPos, targetPos);

        return distance <= m_range;
    }
}
