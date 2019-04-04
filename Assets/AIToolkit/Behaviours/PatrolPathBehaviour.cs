using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPathBehaviour : Behaviour
{
    private Pathfinding.List m_nodes;
    private Transform[] m_points;
    private int pointIndex;

    public PatrolPathBehaviour(Pathfinding.List nodes, Transform[] points) {
        m_nodes = nodes;
        m_points = points;
    }

    public void SetInfo(Pathfinding.List nodes, Transform[] points) {
        m_nodes = nodes;
        m_points = points;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        var path = new Pathfinding.List();
        if (agent.GetBlackboard().Get("Path", ref path) == false) {
            return eBehaviourResult.FAILURE;
        }

        Vector2 position;
        position = agent.gameObject.transform.position;
        
        // Check pointIndex
        if (pointIndex == m_points.Length - 1) { // 
            pointIndex = -1;
        }

        pointIndex++;


        bool found = false;
        do {
            var first = Pathfinding.Search.FindClosest(position.x, position.y, m_nodes);
            var end = Pathfinding.Search.FindClosest(m_points[pointIndex].position.x, m_points[pointIndex].position.y, m_nodes);
            found = Pathfinding.Search.AStar(first, end, path, Pathfinding.Search.HeuristicDistanceSqr);
            agent.GetBlackboard().Set("Path", path);
        } while (found == false);

        return eBehaviourResult.SUCCESS;
    }
}
