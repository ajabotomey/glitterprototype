using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehaviour : Behaviour {
    private Pathfinding.List m_nodes;

    public Pathfinding.List Nodes {
        get {
            return m_nodes;
        }

        set {
            m_nodes = value;
        }
    }

    public PathBehaviour() {
        m_nodes = new Pathfinding.List();
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        var path = new Pathfinding.List();
        if (agent.GetBlackboard().Get("Path", ref path) == false) {
            return eBehaviourResult.FAILURE;
        }

        float speed = 0;
        agent.GetBlackboard().Get("Speed", ref speed);

        Vector3 position;
        position = agent.gameObject.transform.position;

        var first = path.First();

        // Distance to first
        //float distance = Vector3.Distance(first.Position, position);
        float xDiff = first.Position.x + position.x;
        float yDiff = first.Position.y + position.y;
        float distance = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);

        // If not at target, move towards target
        if (distance > 5) {
            xDiff /= distance;
            yDiff /= distance;

            // Move to target
            Vector3 movement = new Vector3(xDiff * speed * deltaTime, 0f, yDiff * speed * deltaTime);
            //agent.gameObject.transform.Translate(movement);
        } else {
            // Remove first node
            path.RemoveFirst();

            // If last node, pick new path
            if (path.Length == 0) {
                bool found = false;
                do {
                    var length = m_nodes.Length;
                    var end = m_nodes.Retrieve((int)UnityEngine.Random.value % length);
                    //found = Pathfinding.Search.dijkstra(first, end, path);
                    found = Pathfinding.Search.AStar(first, end, path, Pathfinding.Search.HeuristicDistanceSqr);
                } while (found == false);
            }
        }

        return eBehaviourResult.SUCCESS;
    }
}
