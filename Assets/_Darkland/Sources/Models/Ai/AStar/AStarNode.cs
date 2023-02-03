using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai.AStar {

    public class AStarNode : IComparable<AStarNode> {

        public AStarNode(int heuristicCost, Vector3Int pos, AStarNode parent) {
            MoveCost = 0;
            HeuristicCost = heuristicCost;
            Pos = pos;
            Parent = parent;
        }

        public static AStarNode New(Vector3Int pos, Vector3Int dest, [CanBeNull] AStarNode parent) {
            return new AStarNode(HeuristicCostFrom(pos, dest), pos, parent);
        }

        public void SetParent(AStarNode parent) {
            Parent = parent;
        }

        public void AddMoveCost(int delta) {
            MoveCost += delta;
        }

        public static int HeuristicCostFrom(Vector3Int pos, Vector3Int dest) {
            return  Math.Abs(pos.x - dest.x) + Math.Abs(pos.y - dest.y);
        }
        
        public int MoveCost { get; private set; }
        public int HeuristicCost { get; }
        public int OverallCost => MoveCost + HeuristicCost;
        public Vector3Int Pos { get; }
        public AStarNode Parent { get; private set; }

        public int CompareTo(AStarNode other) {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var moveCostComparison = MoveCost.CompareTo(other.MoveCost);
            if (moveCostComparison != 0) return moveCostComparison;
            var heuristicCostComparison = HeuristicCost.CompareTo(other.HeuristicCost);
            if (heuristicCostComparison != 0) return heuristicCostComparison;
            return Comparer<AStarNode>.Default.Compare(Parent, other.Parent);
        }

    }

}