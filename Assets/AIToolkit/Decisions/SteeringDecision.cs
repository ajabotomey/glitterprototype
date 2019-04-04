using UnityEngine;

public class SteeringDecision : Decision {
    protected SteeringForce m_force;

    public SteeringDecision(SteeringForce force = null) {
        m_force = force;
    }

    public void SetForce(SteeringForce force) {
        m_force = force;
    }

    public override void MakeDecision(AIAgent agent, float deltaTime) {
        // Cap the velocity
        float maxVelocity = 0;
        agent.GetBlackboard().Get("MaxVelocity", ref maxVelocity);
        Vector2 velocity = Vector2.zero;
        agent.GetBlackboard().Get("Velocity", ref velocity);

        Vector2 force = m_force.GetForce(agent);

        velocity.x += force.x * deltaTime;
        velocity.y += force.y * deltaTime;

        float magnitudeSqrt = velocity.x * velocity.x + velocity.y * velocity.y;
        if (magnitudeSqrt > (maxVelocity * maxVelocity)) {
            float magnitude = Mathf.Sqrt(magnitudeSqrt);
            velocity.x = velocity.x / magnitude * maxVelocity;
            velocity.y = velocity.y / magnitude * maxVelocity;
        }

        //gameObject.transform.Translate(new Vector3(velocity.x * deltaTime, 0f, velocity.y * deltaTime));
        agent.GetBlackboard().Set("NewVelocity", velocity);
    }
}
