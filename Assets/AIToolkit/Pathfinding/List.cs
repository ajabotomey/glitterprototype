using UnityEngine;
using System.Collections;
using System;

namespace Pathfinding {
    public class List : IEnumerable {

        private ArrayList nodes = new ArrayList();

        public int Length {
            get { return this.nodes.Count; }
        }

        public bool Contains(object node) {
            return this.nodes.Contains(node);
        }

        public Node First() {
            if (this.nodes.Count > 0) {
                return (Node)this.nodes[0];
            }
            return null;
        }

        public Node Last() {
            if (this.nodes.Count > 0)
                return (Node)this.nodes[Length - 1];

            return null;
        }

        public void Add(Node node) {
            this.nodes.Add(node);
            //this.nodes.Sort(new ListOrderComparer());
            //this.nodes.Sort ();
        }

        public void AddFirst(Node node) {
            this.nodes.Insert(0, node);
        }

        public void Remove(Node node) {
            this.nodes.Remove(node);
            //this.nodes.Sort(new ListOrderComparer());
            //this.nodes.Sort ();
        }

        public void RemoveFirst() {
            this.nodes.RemoveAt(0);
        }

        public void Clear() {
            this.nodes.Clear();
        }

        public Node Retrieve(int index) {
            return (Node)this.nodes[index];
        }

        public void SortGScore() {
            this.nodes.Sort(new ListGScoreOrderComparer());
        }

        public void SortFScore() {
            this.nodes.Sort(new ListFScoreOrderComparer());
        }

        public IEnumerator GetEnumerator() {
            return this.nodes.GetEnumerator();
        }

        // this function is need so the list will sort by gn
        public class ListGScoreOrderComparer : IComparer {
            static Node n1;
            static Node n2;

            public int Compare(object a, object b) {
                n1 = a as Node;
                n2 = b as Node;

                if (n1.GScore > n2.GScore) return 1;
                if (n1.GScore < n2.GScore) return -1;
                return 0;
            }
        }

        // this function is need so the list will sort by fn
        public class ListFScoreOrderComparer : IComparer {
            static Node n1;
            static Node n2;

            public int Compare(object a, object b) {
                n1 = a as Node;
                n2 = b as Node;

                if (n1.FScore > n2.FScore) return 1;
                if (n1.FScore < n2.FScore) return -1;
                return 0;
            }
        }
    }
}