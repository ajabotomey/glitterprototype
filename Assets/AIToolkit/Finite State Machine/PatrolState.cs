using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {
    private float m_speed;
    private float m_rotation;
    private int m_currentTarget;
    private List<Vector3> m_locations;

    public PatrolState(float speed) {
        m_currentTarget = 0;
        m_speed = speed;
        m_rotation = 30f;
        m_locations = new List<Vector3>();
    }

    public void AddWaypoint(Vector3 pos) {
        m_locations.Add(pos);
    }

    public override void UpdateState(AIAgent agent, float deltaTime) {
        if (m_locations.Count == 0) {
            return;
        }

        // Get the position of the target
        Vector3 location = m_locations[m_currentTarget];

        // Get Agent Position
        Vector3 agentPos = agent.gameObject.transform.position;

        // Get the distance
        //float distance = Vector3.Distance(transform.position, location);

        //if (distance > 10) {
        //    GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation,
        //        Quaternion.LookRotation(location - agentPos), m_rotation * Time.deltaTime));
        //    GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * m_speed * deltaTime);
        //} else {
        //    // Go to the next target
        //    if (++m_currentTarget >= m_locations.Count) {
        //        m_currentTarget = 0;
        //    }
        //}
    }
}
