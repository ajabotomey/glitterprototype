using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Create subclasses. One for creatures and one for vehicles to deal with rotation and movement a bit better
public class FollowPathBehaviour : Behaviour {

    private float rotationSpeed = 10.0f;
    
    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        var path = new Pathfinding.List();
        if (agent.GetBlackboard().Get("Path", ref path) == false) {
            return eBehaviourResult.FAILURE;
        }

        if (path.Length == 0) {
            return eBehaviourResult.FAILURE;
        }

        float speed = 0;
        agent.GetBlackboard().Get("Speed", ref speed);

        Vector3 position;
        position = agent.gameObject.transform.position;

        var first = path.First();

        // Distance to first
        //float distance = Vector3.Distance(first.Position, position);
        float xDiff = first.Position.x - position.x;
        float yDiff = first.Position.y - position.y;
        float distance = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);

        // If not at target, move towards target
        if (distance > 0.5) {
            xDiff /= distance;
            yDiff /= distance;

            // Rotate towards target - Needs refining.
            Transform agentTransform = agent.gameObject.transform;
            Vector2 agentPosition = new Vector2(agentTransform.position.x, agentTransform.position.y);
            Quaternion rotation = Quaternion.Slerp(agentTransform.rotation, Quaternion.LookRotation(first.Position - agentPosition), rotationSpeed * Time.deltaTime);
            agentTransform.rotation = rotation;

            // Move to target
            Vector3 movement = new Vector3(xDiff * speed * deltaTime, yDiff * speed * deltaTime, 0f);
            agentTransform.Translate(movement);
        }
        else {
            // Remove first node
            path.RemoveFirst();
        }

        return eBehaviourResult.SUCCESS;
    }
}
