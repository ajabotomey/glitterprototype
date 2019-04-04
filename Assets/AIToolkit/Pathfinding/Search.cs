using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinding {
    public delegate float HeuristicCheck(Node start, Node end);

    public static class Search {
        public static bool dijkstra(Node start, Node end, List path) {
            List openList = new List();
            List closedList = new List();

            start.Previous = null;
            start.GScore = 0f;

            end.Previous = null;

            openList.Add(start);
            openList.SortGScore();

            while (openList.Length != 0) {

                Node currentNode = openList.First();

                if (currentNode == end) {
                    break;
                }

                openList.Remove(currentNode);
                openList.SortGScore();
                closedList.Add(currentNode);

                // Add connections to open list
                foreach (Pathfinding.Edge edge in currentNode.Edges) {
                    Node target = edge.Target;
                    float gScore = currentNode.GScore + edge.Cost;

                    // If the node is closed
                    if (!closedList.Contains(target)) {
                        if (!openList.Contains(target)) {
                            target.Previous = currentNode;
                            target.GScore = gScore;
                            openList.Add(target);
                            openList.SortGScore();
                        }
                        else if (gScore < target.GScore) {
                            target.Previous = currentNode;
                            target.GScore = gScore;
                        }
                    }
                }
            }

            // Did we find a path
            if (end.Previous != null) {
                path.Clear();

                while (end != null) {
                    path.AddFirst(end);
                    end = end.Previous;
                }

                return true;
            }

            return false;
        }

        //public static Func<Node, Node> heuristicCheck;
        public static bool AStar(Node start, Node end, List path, HeuristicCheck heuristic) {
            List openList = new List();
            List closedList = new List();

            start.Previous = null;
            start.GScore = 0f;
            start.HScore = heuristic(start, end);
            start.FScore = start.GScore + start.HScore;

            end.Previous = null;

            openList.Add(start);
            openList.SortFScore();

            while (openList.Length != 0) {

                Node currentNode = openList.First();

                if (currentNode == end) {
                    break;
                }

                openList.Remove(currentNode);
                openList.SortFScore();
                closedList.Add(currentNode);

                // Add connections to open list
                foreach (Pathfinding.Edge edge in currentNode.Edges) {
                    if (edge.Target == null)
                        continue;

                    Node target = edge.Target;
                    float gScore = currentNode.GScore + edge.Cost;

                    // If the node is closed
                    if (!closedList.Contains(target)) {
                        if (!openList.Contains(target)) {
                            target.Previous = currentNode;

                            target.GScore = gScore;
                            target.HScore = heuristic(target, end);
                            target.FScore = target.GScore + target.HScore;

                            openList.Add(target);
                            openList.SortFScore();
                        }
                        else if (gScore < target.GScore) {
                            target.GScore = gScore;
                            target.FScore = target.GScore + target.HScore;
                            target.Previous = currentNode;
                        }
                    }
                }
            }

            // Did we find a path
            if (end.Previous != null) {
                path.Clear();

                while (end != null) {
                    path.AddFirst(end);
                    end = end.Previous;
                }

                return true;
            }

            return false;
        }

        public static float HeuristicManhattan(Node start, Node end) {
            return (end.Position.x - start.Position.x) + (end.Position.y - start.Position.y);
        }

        public static float HeuristicDistanceSqr(Node start, Node end) {
            float x = (end.Position.x - start.Position.x);
            float y = (end.Position.y - start.Position.y);

            return x * x + y * y;
        }

        public static Node FindClosest(float x, float y, Pathfinding.List nodes) {
            Node closest = null;

            float closestDist = 2000 * 2000;

            foreach (Pathfinding.Node node in nodes) {
                Vector2 position = new Vector3(x, y);
                float dist = Vector2.Distance(node.Position, position);

                if (dist < closestDist) {
                    closest = node;
                    closestDist = dist;
                }
            }

            return closest;
        }
    }
}