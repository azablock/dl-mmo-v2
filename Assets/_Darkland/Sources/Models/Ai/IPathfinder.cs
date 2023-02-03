using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Ai.AStar;
using _Darkland.Sources.Models.World;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public interface IPathfinder {
        List<Vector3Int> Path(Vector3Int start, Vector3Int dest, IDarklandWorld world);
    }

    public class AStarContext {
        public AStarGrid grid;
        public Vector3Int dest;
        public AStarNode currentNode;
        public IDarklandWorld world;
    }

    public class AStarPathfinder : IPathfinder {

        private int _VER_HOR_COST = 10;

        public List<Vector3Int> Path(Vector3Int start, Vector3Int dest, IDarklandWorld world) {
            if (start.Equals(dest)) return new List<Vector3Int>();

            var ctx = new AStarContext {
                currentNode = AStarNode.New(start, dest, null),
                grid = new AStarGrid(),
                world = world,
                dest = dest
            };

            ctx.grid.MarkAsOpen(ctx.currentNode);
            AddNeighbours(ctx);
            ctx.grid.MarkAsClosed(ctx.currentNode);

            while (!ctx.grid.openNodes.ContainsKey(dest)) {
                ctx.currentNode = SmallestOverallCostNode(ctx);
                ctx.grid.MarkAsClosed(ctx.currentNode);
                AddNeighbours(ctx);

                Neighbours(ctx)
                    .ToList()
                    .ForEach(it => {
                        var neighbour = ctx.grid.openNodes[it];

                        if (neighbour.Parent == null || MustBeReparented(ctx, it)) {
                            neighbour.SetParent(ctx.currentNode);
                        }
                    });
            }

            ctx.currentNode = ctx.grid.openNodes[dest];

            var path = new List<Vector3Int>();
            var currentPathPos = Vector3Int.zero + ctx.currentNode.Pos;

            while (!currentPathPos.Equals(start)) {
                path.Insert(0, currentPathPos);
                ctx.currentNode = ctx.grid.closedNodes[ctx.currentNode.Parent.Pos];
                currentPathPos = ctx.currentNode.Pos;
            }

            path.Insert(0, currentPathPos);

            return path;
        }

        private void AddNeighbours(AStarContext ctx) {
            Neighbours(ctx)
                .ForEach(it => {
                    var newNode = AStarNode.New(it, ctx.dest, ctx.currentNode);
                    newNode.AddMoveCost(_VER_HOR_COST + ctx.currentNode.MoveCost);
                    ctx.grid.openNodes.Add(it, newNode);
                });
        }

        private List<Vector3Int> Neighbours(AStarContext ctx) {
            var neighbours = new HashSet<Vector3Int>();
            var pos = ctx.currentNode.Pos;

            neighbours.Add(pos + Vector3Int.up);
            neighbours.Add(pos + Vector3Int.right);
            neighbours.Add(pos + Vector3Int.down);
            neighbours.Add(pos + Vector3Int.left);

            //todo tutaj zamienilem (negate) wzgledem githuba
            return neighbours.Where(it => {
                    var isInWorld = ctx.world.allFieldPositions.Contains(it);
                    var isNotObstacle = !ctx.world.obstaclePositions.Contains(it);
                    var isNotClosed = !ctx.grid.closedNodes.ContainsKey(it);
                    var isNotOpen = !ctx.grid.openNodes.ContainsKey(it);

                    return isInWorld && isNotObstacle && isNotClosed && isNotOpen;
                })
                .ToList();
        }

        private bool MustBeReparented(AStarContext ctx, Vector3Int pos) {
            return ctx.currentNode.OverallCost + _VER_HOR_COST < ctx.grid.openNodes[pos].OverallCost;
        }

        private AStarNode SmallestOverallCostNode(AStarContext ctx) {
            var aStarNodes = ctx.grid.openNodes.Values.ToList();
            
            //todo nr2 -> pytanie czy w dobra strone sortujemy
            aStarNodes.Sort((x, y) => x.OverallCost.CompareTo(y.OverallCost));

            return aStarNodes.First();
        }

    }

}