using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
    public class Node {
        public enum eFlags {
            MEDKIT = (1 << 0),
        }

        public Node() {
            Flags = 0;
            Edges = new List<Edge>();
        }

        public Node(Vector2 position) {
            Flags = 0;
            Edges = new List<Edge>();
            Position = position;
        }

        public void Reset() {
            Valid = true;
            GScore = 0;
            HScore = 0;
            FScore = 0;
        }

        public bool Valid { get; set; }

        public int Flags { get; set; }

        public List<Pathfinding.Edge> Edges { get; set; }

        public float GScore { get; set; }

        public float HScore { get; set; }

        public float FScore { get; set; }

        public Node Previous { get; set; }

        public Vector2 Position { get; set; }

        static bool CompareGScore(Node a, Node b) {
            return a.GScore < b.GScore;
        }

        //public bool AnyConnectionsBad() {
        //    for (int i = 0; i < connections.Length; i++) {
        //        if (connections[i] == null || !connections[i].valid) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}