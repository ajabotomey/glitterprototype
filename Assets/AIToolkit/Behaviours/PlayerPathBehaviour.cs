using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathBehaviour : Behaviour {
    private Pathfinding.List m_nodes;

    public PlayerPathBehaviour(Pathfinding.List nodes) {
        m_nodes = nodes;
    }

    public void SetNodes(Pathfinding.List nodes) {
        m_nodes = nodes;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        var path = new Pathfinding.List();
        if (agent.GetBlackboard().Get("Path", ref path) == false) {
            return eBehaviourResult.FAILURE;
        }

        Vector3 position;
        position = agent.gameObject.transform.position;

        bool found = false; 
        do {
            Vector3 playerPos = GameObject.Find("PlayerTank").transform.position;
            var first = Pathfinding.Search.FindClosest(position.x, position.y, m_nodes);
            var length = m_nodes.Length;
            var end = Pathfinding.Search.FindClosest(playerPos.x, playerPos.y, m_nodes);
            if (first == end) {
                continue;
            }
            //found = Pathfinding.Search.dijkstra(first, end, path);
            found = Pathfinding.Search.AStar(first, end, path, Pathfinding.Search.HeuristicDistanceSqr);
            agent.GetBlackboard().Set("Path", path);
        } while (found == false);

        return eBehaviourResult.SUCCESS;
    }
}
