using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : Behaviour {
    protected List<WeightedForce> m_forces;

    public SteeringBehaviour() {
        m_forces = new List<WeightedForce>();
    }

    public void AddForce(SteeringForce force, float weight = 1f) {
        WeightedForce wf = new WeightedForce();
        wf.force = force;
        wf.weight = weight;
        m_forces.Add(wf);
    }

    void SetWeightForForce(SteeringForce force, float weight) {
        for (int i = 0; i < m_forces.Count; i++) {
            if (m_forces[i].force == force) {
                m_forces[i].SetWeight(weight);
            }
        }
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        Vector2 force = Vector2.zero;

        foreach (var wf in m_forces) {
            Vector2 temp = wf.force.GetForce(agent);

            force.x += temp.x * wf.weight;
            force.y += temp.y * wf.weight;
        }

        // Cap the velocity
        float maxVelocity = 0;
        agent.GetBlackboard().Get("MaxVelocity", ref maxVelocity);
        Vector2 velocity = Vector2.zero;
        agent.GetBlackboard().Get("Velocity", ref velocity);

        velocity.x += force.x * deltaTime;
        velocity.y += force.y * deltaTime;

        float magnitudeSqrt = velocity.x * velocity.x + velocity.y * velocity.y;
        if (magnitudeSqrt > (maxVelocity * maxVelocity)) {
            float magnitude = Mathf.Sqrt(magnitudeSqrt);
            velocity.x = velocity.x / magnitude * maxVelocity;
            velocity.y = velocity.y / magnitude * maxVelocity;
        }

        //gameObject.transform.Translate(new Vector3(velocity.x * deltaTime, 0f, velocity.y * deltaTime));
        //agent.GetBlackboard().Set("NewVelocity", velocity);
        Vector3 movement = new Vector3(velocity.x * deltaTime, 0f, velocity.y * deltaTime);
        //Debug.Log(movement);
        agent.transform.Translate(movement);

        return eBehaviourResult.SUCCESS;
    }
}
