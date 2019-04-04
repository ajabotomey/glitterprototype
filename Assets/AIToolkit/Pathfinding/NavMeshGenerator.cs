// Code adapted from https://github.com/7ark/Unity-Pathfinding/blob/master/AINavMeshGenerator.cs under MIT Licence

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[ExecuteInEditMode]
public class NavMeshGenerator : MonoBehaviour
{
    enum Directions { Right, DownRight, Down, DownLeft, Left, UpLeft, Up, UpRight }

    [SerializeField]
    private float updateInterval = 0.1f;
    [SerializeField]
    private float pointDistributionSize = 0.5f;
    [SerializeField]
    LayerMask destroyNodeMask;
    [SerializeField]
    LayerMask obstacleMask;

    public Rect size;
    public static NavMeshGenerator instance;

    private int nodeEdges = 8;

    private float updateTimer = 0;
    private Pathfinding.List grid = null;
    public Pathfinding.List Grid {
        get {
            if (grid == null) {
                GenerateNewGrid();
            }
            return grid;
        }
    }
    private Dictionary<Vector2, Node> positionNodeDictionary = new Dictionary<Vector2, Node>();

    public void GenerateNewGrid() {
        FillOutGrid();
        DestroyBadNodes();
        CheckForBadNodes();
    }

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewGrid();
        updateTimer = updateInterval;
    }

    private void FillOutGrid() {
        grid = new Pathfinding.List();
        positionNodeDictionary.Clear();
        Vector2 currentPoint = new Vector2((size.x - size.width / 2) + pointDistributionSize, (size.y + size.height / 2) - pointDistributionSize);
        int iteration = 0;
        bool alternate = false;
        bool cacheIteration = false;
        int length = -1;
        int yLength = 0;
        while (true) {
            iteration++;
            Node newNode = new Node(currentPoint);
            Grid.Add(newNode);
            positionNodeDictionary.Add(currentPoint, newNode);
            currentPoint += new Vector2(pointDistributionSize * 2, 0);
            if (currentPoint.x > size.x + size.width / 2) {
                if (length != -1) {
                    while (iteration < length) {
                        Node extraNode = new Node(currentPoint);
                        Grid.Add(extraNode);
                        iteration++;
                    }
                } else {
                    Node extraNode = new Node(currentPoint);
                    Grid.Add(extraNode);
                }
                currentPoint = new Vector2((size.x - size.width / 2) + (alternate ? pointDistributionSize : 0), currentPoint.y - pointDistributionSize);
                alternate = !alternate;
                cacheIteration = true;
                yLength++;
            }
            if (currentPoint.y < size.y - size.height / 2) {
                break;
            }
            if (cacheIteration) {
                if (length == -1) {
                    length = iteration + 1;
                }
                iteration = 0;
                cacheIteration = false;
            }
        }
        for (int i = 0; i < Grid.Length; i++) {
            for (int direction = 0; direction < nodeEdges; direction++) {
                Grid.Retrieve(i).Edges.Add(GetNodeFromDirection(i, (Directions)direction, length));
            }
        }
    }

    private void DestroyBadNodes() {
        //First check if each node is inside a destroy mask
        for (int i = Grid.Length - 1; i >= 0; i--) {
            Node node = Grid.Retrieve(i);
            Collider2D hit = Physics2D.OverlapCircle(node.Position, pointDistributionSize, destroyNodeMask);
            if (hit != null) {
                //At this point, we know this node is bad, and we must destroy it. For humanity itself.
                for (int j = 0; j < node.Edges.Count; j++) {
                    //Go through all the connections to this node
                    if (node.Edges[j] != null) {
                        if (node.Edges[j].Target != null) {
                            Node edgeNode = node.Edges[j].Target;
                            for (int k = 0; k < edgeNode.Edges.Count; k++) {
                                //Set the nodes connections reference to this node to null, because it no longer exists.
                                //Is that confusing? It sounds confusing.
                                if (edgeNode.Edges[k] != null) {
                                    if (edgeNode.Edges[k].Target != null) {
                                        if (edgeNode.Edges[k].Target == node) {
                                            edgeNode.Edges[k].Target = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Grid.Remove(node);
            }
        }
    }

    private void CheckForBadNodes() {
        for (int i = 0; i < Grid.Length; i++) {
            Grid.Retrieve(i).Reset();
        }

        //Make any node with a destroyed outside have an extra layer barrier around it, so that they dont get too close to walls
        for (int i = 0; i < Grid.Length; i++) {
            Node node = Grid.Retrieve(i);
            if (node.Valid) {
                if (node.Edges != null) {
                    for (int j = 0; j < node.Edges.Count; j++) {
                        if (node.Edges[j] != null) {
                            Node connection = node.Edges[j].Target;
                            if (connection == null) {
                                node.Valid = false;
                            }
                        }
                    }
                }
            }
        }

        //Then check if the node is inside a normal mask to disable it.
        for (int i = 0; i < Grid.Length; i++) {
            Node node = Grid.Retrieve(i);
            if (node.Valid) {
                Collider2D hit = Physics2D.OverlapCircle(node.Position, 0.05f, obstacleMask);
                if (hit != null) {
                    node.Valid = false;
                }
            }
        }
    }

    Edge GetNodeFromDirection(int nodeIndex, Directions direction, int length) {
        Edge edge = new Edge();

        int index = -1;
        bool isStartOfRow = (nodeIndex + 1) % length == 1;
        bool isEndOfRow = (nodeIndex + 1) % length == 0;
        bool isOddRow = (((nodeIndex + 1) - Mathf.FloorToInt((nodeIndex) % length)) / length) % 2 == 0;

        switch (direction) {
            case Directions.Right:
                if (isEndOfRow) return null;
                index = nodeIndex + 1;
                break;
            case Directions.DownRight:
                if (isEndOfRow && isOddRow) return null;
                index = nodeIndex + length + (isOddRow ? 1 : 0);
                break;
            case Directions.Down:
                index = nodeIndex + length * 2;
                break;
            case Directions.DownLeft:
                if (isStartOfRow && !isOddRow) return null;
                index = nodeIndex + (length - (isOddRow ? 0 : 1));
                break;
            case Directions.Left:
                if (isStartOfRow) return null;
                index = nodeIndex - 1;
                break;
            case Directions.UpLeft:
                if (isStartOfRow && !isOddRow) return null;
                index = nodeIndex - (length + (isOddRow ? 0 : 1));
                break;
            case Directions.Up:
                index = nodeIndex - length * 2;
                break;
            case Directions.UpRight:
                if (isEndOfRow && isOddRow) return null;
                index = nodeIndex - (length - (isOddRow ? 1 : 0));
                break;
        }

        if (index >= 0 && index < Grid.Length) {
            Node node = Grid.Retrieve(nodeIndex);
            //return Grid[index];
            edge.Target = Grid.Retrieve(index);
            edge.Cost = node.Position.x * node.Position.x + node.Position.y * node.Position.y;

            return edge;
        } else {
            return null;
        }
    }

    public void ClearGrid() {
        Grid.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grid == null) {
            GenerateNewGrid();
        }

        //We update the bad nodes constantly, so as objects or enemies move, the grid automatically adjusts itself.
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0) {
            updateTimer = updateInterval;
            CheckForBadNodes();
        }
    }

    void OnDrawGizmosSelected() {
        if (Grid == null) {
            return;
        }
        for (int i = 0; i < Grid.Length; i++) {
            Node node = Grid.Retrieve(i);
            for (int j = 0; j < Grid.Retrieve(i).Edges.Count; j++) {
                if (node.Edges[j] != null && node.Edges[j].Target != null) {
                    Gizmos.color = node.Valid && node.Edges[j].Target.Valid ? Color.green : Color.red;
                    Gizmos.DrawLine(node.Position, node.Edges[j].Target.Position);
                }
            }
        }
        for (int i = 0; i < Grid.Length; i++) {
            Gizmos.color = Grid.Retrieve(i).Valid ? Color.blue : Color.red;
            Gizmos.DrawCube(Grid.Retrieve(i).Position, Vector3.one * 0.2f);
        }
    }
}
