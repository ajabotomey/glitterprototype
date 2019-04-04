using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
    public class Edge {
        public enum eFlags {
            CLOSED = (1 << 0),
            RIVER = (1 << 1)
        };

        private uint flags;
        private Node target;
        private float cost;

        public Edge() {
            flags = 0;
        }

        public uint Flags {
            get {
                return flags;
            }

            set {
                flags = value;
            }
        }

        public Node Target {
            get {
                return target;
            }

            set {
                target = value;
            }
        }

        public float Cost {
            get {
                return cost;
            }

            set {
                cost = value;
            }
        }
    }
}