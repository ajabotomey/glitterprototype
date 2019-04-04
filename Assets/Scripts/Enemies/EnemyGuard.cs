using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : MonoBehaviour {
    [Header("Agents")]
    [SerializeField][Tooltip("Player Agent")] private AIAgent m_player;
    [SerializeField] private AIAgent m_agent;

    [Header("Patrol Points")]
    [SerializeField]
    private Transform[] points;

    [Header("Field of View Variables")]
    [SerializeField] private float viewRadius;
    [SerializeField] [Range(0, 360)] private float viewAngle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Within Range Variables")]
    [SerializeField] private float stopRange = 10f;

    [Header("General Agent Variables")]
    [SerializeField] private float speed;

    Pathfinding.List m_nodes;

    // Start is called before the first frame update
    void Start()
    {
        m_nodes = NavMeshGenerator.instance.Grid;

        // Create Behaviours
        var followPath = ScriptableObject.CreateInstance<FollowPathBehaviour>();
        var patrolPath = ScriptableObject.CreateInstance<PatrolPathBehaviour>();
        patrolPath.SetInfo(m_nodes, points);
        var playerPath = ScriptableObject.CreateInstance<PlayerPathBehaviour>();
        playerPath.SetNodes(m_nodes);

        // Create Decorators
        var playerPathDecorator = ScriptableObject.CreateInstance<PlayerPathDecorator>();
        playerPathDecorator.Init(followPath, m_nodes);
        var patrolPathDecorator = ScriptableObject.CreateInstance<LogDecorator>();
        patrolPathDecorator.Init(patrolPath, "Moving to next patrol point");

        // Create Conditions
        var fovCondition = ScriptableObject.CreateInstance<FieldOfViewCondition>();
        fovCondition.Init(targetMask, obstacleMask, viewRadius, viewAngle);
        var stopCondition = ScriptableObject.CreateInstance<WithinRangeCondition>();
        stopCondition.Init(m_player, stopRange);

        // Behaviour Tree Branches
        var guardBehaviour = ScriptableObject.CreateInstance<SelectorBehaviour>();
        var fovBehaviour = ScriptableObject.CreateInstance<SequenceBehaviour>();
        var pathFollowBehaviour = ScriptableObject.CreateInstance<SelectorBehaviour>();

        // Construct the behaviour tree
        guardBehaviour.AddChild(fovBehaviour);
            fovBehaviour.AddChild(fovCondition);
            fovBehaviour.AddChild(playerPath);

        guardBehaviour.AddChild(pathFollowBehaviour);
            pathFollowBehaviour.AddChild(followPath);
            pathFollowBehaviour.AddChild(patrolPath);

        // Set behaviours
        // Create the path
        Pathfinding.List path = new Pathfinding.List();
        var pos = m_agent.gameObject.transform.position;
        var first = Pathfinding.Search.FindClosest(pos.x, pos.y, m_nodes);
        var end = Pathfinding.Search.FindClosest(points[0].position.x, points[0].position.y, m_nodes);
        Pathfinding.Search.AStar(first, end, path, Pathfinding.Search.HeuristicDistanceSqr);
        m_agent.GetBlackboard().Set("Path", path);
        m_agent.GetBlackboard().Set("Speed", speed);
        m_agent.AddBehaviour(guardBehaviour);
    }

    // Update is called once per frame
    void Update()
    {
        m_agent.UpdateAgent(Time.deltaTime);
    }
}
