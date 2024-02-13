using AdventOfCode2023.Models;
using System.Collections.Generic;

namespace AdventOfCode2023.Solutions.Day_17
{
    /**
     *  PART 1
     *  1. Parse input into a grid of integers (matrix)
     *  2. Use a modified version of Dijkstras Algorithm to find the shortest path
     *      a. Use a PriorityQueue collection such that the current shortest path is returned
     *      b. For every step, find all possible paths
     *          I. A path is possible if it does not exceed the Matrix indexes
     *          II. A path is possible if it does not exceed 3 steps in the same direction
     *          III. A path is possible if it does not go back the same direction
     *      c. For every path that is returned, cache the result as to indicate there is a shortest path that has already reached this point
     *      d. Return once the current step is on the bottom right corner of the matrix. No need to check other paths as we already know we are on the current shortest path.
     **/
    public class SolutionA : Solution
    {
        private Dictionary<string, bool> VisitedCache = new Dictionary<string, bool>();
        private const int MAX_NUMBER_OF_STEPS = 3;

        protected override string GetSolutionOutput()
        {
            var minimumAmountOfHeatLoss = FindMinimumHeatLossPath();
            return minimumAmountOfHeatLoss.ToString();
        }

        private int FindMinimumHeatLossPath()
        {
            var graph = ParseInputIntoWeightedGraph();
            return TraverseGraphUsingDijkstrasAlgorithm(graph);
        }

        private int[,] ParseInputIntoWeightedGraph()
        {
            var firstRow = stringInputs[0];
            var graph = new int[firstRow.Length, stringInputs.Count];
            for (int i = 0; i < stringInputs.Count; i++)
            {
                var currentRow = stringInputs[i];
                for (int j = 0; j < currentRow.Length; j++)
                {
                    graph[j, i] = currentRow[j] - '0'; // Offset digit ASCII value to int;
                }
            }

            return graph;
        }

        private int TraverseGraphUsingDijkstrasAlgorithm(int[,] graph)
        {
            var endX = graph.GetLength(0) - 1;
            var endY = graph.GetLength(1) - 1;

            var priorityQueue = new PriorityQueue<Step, int>();                
            var initialSteps = GetNextSteps(graph, new Step
            {
                X = 0,
                Y = 0,
                Distance = 0,
                Steps = 0,
                Direction = Direction.SOUTH
            });

            foreach (var step in initialSteps)
            {
                VisitedCache.Add(GetCacheKey(step), true);
                priorityQueue.Enqueue(step, step.Distance);
            }
            
            while (priorityQueue.Count > 0)
            {
                var currentStep = priorityQueue.Dequeue();
                if (currentStep.X == endX && currentStep.Y == endY)
                {
                    // If we've reached the bottom right corner we have found the shortest path and can return early. No need to test other paths.
                    return currentStep.Distance;
                }

                var nextSteps = GetNextSteps(graph, currentStep);
                foreach(var nextStep in nextSteps)
                {
                    var cacheKey = GetCacheKey(nextStep);
                    if (!VisitedCache.TryGetValue(cacheKey, out _))
                    {
                        VisitedCache.Add(cacheKey, true);
                        priorityQueue.Enqueue(nextStep, nextStep.Distance);
                    }
                }
            }

            return -1;
        }

        private string GetCacheKey(Step step)
        {
            return $"x:{step.X}_y:{step.Y}_direction:{step.Direction}_steps:{step.Steps}";
        }

        private List<Step> GetNextSteps(int[,] graph, Step step)
        {
            var nextSteps = new List<Step>();
            var directions = new List<Direction>()
            {
                GetDirectionToTheLeft(step.Direction),
                GetDirectionToTheRight(step.Direction)
            };

            if (step.Steps < MAX_NUMBER_OF_STEPS)
            {
                directions.Add(step.Direction);
            }

            foreach (var direction in directions)
            {
                var nextStep = GetStepInDirection(graph, step, direction);
                if (nextStep != null)
                {
                    nextSteps.Add(nextStep);
                }
            }

            return nextSteps;
        }


        private Direction GetDirectionToTheLeft(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return Direction.WEST;
                case Direction.EAST:
                    return Direction.NORTH;
                case Direction.SOUTH:
                    return Direction.EAST;
                case Direction.WEST:
                    return Direction.SOUTH;
                default:
                    return Direction.UNKNOWN;
            }
        }

        private Direction GetDirectionToTheRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return Direction.EAST;
                case Direction.EAST:
                    return Direction.SOUTH;
                case Direction.SOUTH:
                    return Direction.WEST;
                case Direction.WEST:
                    return Direction.NORTH;
                default:
                    return Direction.UNKNOWN;
            }
        }

        private Step? GetStepInDirection(int[,] graph, Step step, Direction direction)
        {
            var nextX = step.X;
            var nextY = step.Y;
            if (direction == Direction.EAST || direction == Direction.WEST)
            {
                nextX += direction == Direction.EAST ? 1 : -1;
            }
            else
            {
                nextY += direction == Direction.SOUTH ? 1 : -1;
            }

            if (nextX < 0 || nextY < 0 || nextX >= graph.GetLength(0) || nextY >= graph.GetLength(1))
            {
                return null;
            }

            return new Step
            {
                Direction = direction,
                Steps = step.Direction == direction ? step.Steps + 1 : 1,
                X = nextX,
                Y = nextY,
                Distance = graph[nextX, nextY] + step.Distance,
            };
        }
    }
}