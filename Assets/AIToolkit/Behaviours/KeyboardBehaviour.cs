using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyboardBehaviour : Behaviour {
    [SerializeField]
    private float m_speed;

    public KeyboardBehaviour() {
        m_speed = 10;
    }

    void SetSpeed(float speed) {
        m_speed = speed;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;
        movement.x = horizontal * m_speed * deltaTime;
        movement.y = 0;
        movement.z = vertical * m_speed * deltaTime;

        agent.transform.Translate(movement);

        return eBehaviourResult.SUCCESS;
    }
}
