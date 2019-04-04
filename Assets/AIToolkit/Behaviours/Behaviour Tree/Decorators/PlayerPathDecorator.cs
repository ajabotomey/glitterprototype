using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathDecorator : Decorator {
    private Pathfinding.List m_nodes;
    private Pathfinding.List m_path;

    public PlayerPathDecorator(Behaviour child = null, Pathfinding.List nodes = null) {
        m_child = child;
        m_nodes = nodes;
    }

    public void Init(Behaviour child = null, Pathfinding.List nodes = null) {
        m_child = child;
        m_nodes = nodes;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_child != null) {

            // Check if path exists
            m_path = new Pathfinding.List();
            if (agent.GetBlackboard().Get("Path", ref m_path) == false) {
                return eBehaviourResult.FAILURE;
            }

            // Check if the last node on the path is close to the player's position
            Vector3 playerPos = GameObject.Find("PlayerTank").transform.position;
            if (m_path.Last() != Pathfinding.Search.FindClosest(playerPos.x, playerPos.z, m_nodes)) {
                // Calculate New Path
                CalculateNewPath(agent);
            }

            return m_child.Execute(agent, deltaTime);
        }

        return eBehaviourResult.FAILURE;
    }

    // Calculate New Path
    private void CalculateNewPath(AIAgent agent) {
        Vector3 position;
        position = agent.gameObject.transform.position;

        bool found = false;
        do {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            var first = Pathfinding.Search.FindClosest(position.x, position.y, m_nodes);
            var length = m_nodes.Length;
            var end = Pathfinding.Search.FindClosest(playerPos.x, playerPos.y, m_nodes);
            if (first == end) {
                break;
            }
            //found = Pathfinding.Search.dijkstra(first, end, path);
            found = Pathfinding.Search.AStar(first, end, m_path, Pathfinding.Search.HeuristicDistanceSqr);
        } while (found == false);
    }
}
