using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowBehaviour : Behaviour {
    [SerializeField]
    private float m_speed = 0;
    [SerializeField]
    private float m_rotation = 0;
    [SerializeField]
    private AIAgent m_target = null;

    public FollowBehaviour() {
        m_speed = 1f;
        m_rotation = 30f;
        m_target = null;
    }

    public void SetSpeed(float speed) {
        m_speed = speed;
    }

    public void SetTarget(AIAgent target) {
        m_target = target;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_target == null) {
            return eBehaviourResult.FAILURE;
        }        
        
        // Get Target Position
        Vector3 targetPos = m_target.gameObject.transform.position;
        targetPos.y = 0f;

        // Get Agent Position
        Vector3 agentPos = agent.gameObject.transform.position;
        agentPos.y = 0f;

        // Get the distance
        float distance = Vector3.Distance(agent.transform.position, m_target.gameObject.transform.position);

        if (distance > 0) {
            agent.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(agent.transform.rotation,
                Quaternion.LookRotation(targetPos - agentPos), m_rotation * Time.deltaTime));
            agent.GetComponent<Rigidbody>().MovePosition(agent.transform.position + agent.transform.forward * m_speed * deltaTime);
        }

        return eBehaviourResult.SUCCESS;
    }
}
