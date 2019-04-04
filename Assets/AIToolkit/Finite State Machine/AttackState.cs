using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttackState : State {
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_rotation;
    [SerializeField]
    private AIAgent m_target;

    public AttackState(AIAgent target, float speed) {
        m_target = target;
        m_speed = speed;
        m_rotation = 30f;
    }

    public override void UpdateState(AIAgent agent, float deltaTime) {
        if (m_target == null) {
            return;
        }

        // Get Target Position
        Vector3 targetPos = m_target.gameObject.transform.position;
        targetPos.y = 0f;

        // Get Agent Position
        Vector3 agentPos = agent.gameObject.transform.position;
        agentPos.y = 0f;

        // Get the distance
        //float distance = Vector3.Distance(transform.position, m_target.gameObject.transform.position);

        //if (distance > 0) {
        //    GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation,
        //        Quaternion.LookRotation(targetPos - agentPos), m_rotation * Time.deltaTime));
        //    GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * m_speed * deltaTime);
        //}
    }
}
