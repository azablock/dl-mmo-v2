using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai.AStar {

    public class AStarGrid {

        public Dictionary<Vector3Int, AStarNode> openNodes { get; } = new();
        public Dictionary<Vector3Int, AStarNode> closedNodes { get; } = new();

        public void MarkAsOpen(in AStarNode node) {
            openNodes.Add(node.Pos, node);
        }

        public void MarkAsClosed(in AStarNode node) {
            if (!openNodes.ContainsValue(node))
                throw new Exception("cannot mark node as closed, because node wasn't in openNodes collection");

            openNodes.Remove(node.Pos);
            closedNodes.Add(node.Pos, node);
        }

    }

}