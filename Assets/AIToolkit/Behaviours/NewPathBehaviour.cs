using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPathBehaviour : Behaviour {
    private Pathfinding.List m_nodes;

    public NewPathBehaviour(Pathfinding.List nodes) {
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
            var first = Pathfinding.Search.FindClosest(position.x, position.y, m_nodes);
            var length = m_nodes.Length;
            var random = Random.Range(0, length);
            var end = m_nodes.Retrieve(random);
            //found = Pathfinding.Search.dijkstra(first, end, path);
            found = Pathfinding.Search.AStar(first, end, path, Pathfinding.Search.HeuristicDistanceSqr);
        } while (found == false);

        return eBehaviourResult.SUCCESS;
    }
}
