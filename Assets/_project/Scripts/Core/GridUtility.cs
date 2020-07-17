using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public static class GridUtility
    {
        private struct Distance
        {
            public int Amount;
            public Tile From;
        }

        public static IEnumerable<Tile> SquaredZone(Tile from, int size)
        {
            var result = new HashSet<Tile>();

            var grid = from.Grid;
            (int x, int y) = grid.IndexToCoordinate(from.Index);

            for (int i = x - size; i <= x + size; i++)
            {
                for (int j = y - size; j <= y + size; j++)
                {
                    if (grid.IsCorrectCoordinate(i,j))
                    {
                        result.Add(grid[i,j]);
                    }
                }
            }

            return result;
        }

        public static Tile FindFallbackMove(Tile from)
        {
            Tile result = null;
            var neighbors = GetNeighbors(from);
            var minCost = from.Grid.Size;

            foreach (var neighbor in neighbors)
            {
                var cost = TravelWeight(from, neighbor);
                if (cost < minCost)
                {
                    minCost = cost;
                    result = neighbor;
                }
                else if (cost == minCost)
                {
                    result = (Random.Range(0, 1) == 1) ? result : neighbor;
                }
            }
            
            return result;
        }
        
        public static IEnumerable<Tile> FindPath(Tile from, Tile to)
        {
            if (from.Grid != to.Grid)
                return null;

            var grid = from.Grid;
            var queue = new SimplePriorityQueue<Tile, int>();
            var distances = new Distance[grid.Size];

            for (int i = 0; i < grid.Size; i++)
            {
                distances[i] = new Distance() {From = from, Amount = grid.Size};
            }

            distances[from.Index]= new Distance() {From = from, Amount = 0};

            queue.Enqueue(from, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                var neighbors = GetNeighbors(current);

                foreach (var neighbor in neighbors)
                {
                    var inQueue = queue.Contains(neighbor);

                    var g = TravelWeight(current, neighbor) + distances[current.Index].Amount;
                    var h = Heuristic(neighbor, to);
                    var f = g + h;
                    
                    if(g >= grid.Size)
                        continue;
                    
                    if (neighbor == to)
                    {
                        distances[neighbor.Index]= new Distance(){Amount = g, From = current};
                        return ReconstructPath(from,to,distances);
                    }

                    if (g < distances[neighbor.Index].Amount)
                    {
                        distances[neighbor.Index] = new Distance() {Amount = g, From = current};

                        if (inQueue)
                        {
                            queue.UpdatePriority(neighbor, f);
                        }
                        else
                        {
                            queue.Enqueue(neighbor, f);
                        }
                    }
                }
            }

            return null;
        }

        public static IEnumerable<Tile> GetNeighbors(Tile parent)
        {
            var result = new List<Tile>();
            var grid = parent.Grid;
            (int x, int y) = grid.IndexToCoordinate(parent.Index);

            if (CheckCoordinate(x + 1, y, grid))
                result.Add(grid[x + 1, y]);

            if (CheckCoordinate(x - 1, y, grid))
                result.Add(grid[x - 1, y]);

            if (CheckCoordinate(x, y + 1, grid))
                result.Add(grid[x, y + 1]);

            if (CheckCoordinate(x, y - 1, grid))
                result.Add(grid[x, y - 1]);

            return result.ToArray();
        }

        private static bool CheckCoordinate(int x, int y, Grid grid)
        {
            if (x < 0 || x >= grid.SizeX)
                return false;
            if (y < 0 || y >= grid.SizeY)
                return false;

            return grid[x, y].Height > 0;
        }

        private static IEnumerable<Tile> ReconstructPath(Tile from, Tile to, Distance[] distances)
        {
            var result = new List<Tile>();

            var current = to;
            while (current != from)
            {
                result.Add(current);
                current = distances[current.Index].From;
            }
            
            result.Reverse();
            return result;
        }

        private static int Heuristic(Tile from, Tile to)
        {
            var grid = from.Grid;
            (int x1, int y1) = grid.IndexToCoordinate(from.Index);
            (int x2, int y2) = grid.IndexToCoordinate(to.Index);

            return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
        }

        private static int TravelWeight(Tile from, Tile to)
        {
            var grid = from.Grid;
            if (to.Height == 0)
                return grid.Size;

            var dif = Mathf.Max(1, to.Height - from.Height);

            if (to.GridObject == null)
                return dif;

            return Mathf.Max(dif, to.GridObject.PathScore);
        }
    }
}