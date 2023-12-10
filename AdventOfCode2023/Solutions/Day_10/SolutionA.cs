using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_10
{
    /**
     *  PART 1
     *  1. Parse input into a list of strings
     *  2. Find the starting point "S" and all possible connected pipes
     *  3. Considering every pipe only has an entry point and an exit, we can create a nested object that points to each connected pipe
     *  4. Using the position of previous object, we can figure out where the next object is
     *  5. We can continue down the path until we end up back at "S", counting the amount of steps on the way
     *  6. Once a path is found, do the same path in reverse and count the amount of steps again. The smallest number determines its real distance
     *  7. Return the tile with the highest number
     **/
    public class SolutionA : Solution
    {
        protected override string GetSolutionOutput()
        {
            var tileDistance = GetDistanceOfTileFurthestAwayFromStartingPoint();
            return tileDistance.ToString();
        }

        private int GetDistanceOfTileFurthestAwayFromStartingPoint()
        {
            var startingPoint = FindStartingPoint();
            var connectedPipes = FindConnectedPipes(startingPoint);
            var nextPipe = connectedPipes[0];
            while(nextPipe.PipeShape != 'S') 
            {
                nextPipe = FindNextPipe(nextPipe);
            }
            startingPoint.PreviousPipe = nextPipe.PreviousPipe;

            var previousPipe = startingPoint.PreviousPipe;
            var currentDistance = 1;
            var currentLargestDistance = -1;
            while (previousPipe!.PipeShape != 'S')
            {
                previousPipe.DistanceFromStartingPoint = Math.Min(currentDistance, previousPipe.DistanceFromStartingPoint);
                currentLargestDistance = Math.Max(currentLargestDistance, previousPipe.DistanceFromStartingPoint);
                previousPipe = previousPipe.PreviousPipe;
                currentDistance++;
            }
            return currentLargestDistance;
        }

        private Pipe FindStartingPoint()
        {
            var startingPoint = stringInputs.Where(s => s.Contains("S")).First();
            var xCoordinates = startingPoint.IndexOf("S");
            var yCoordinates = stringInputs.IndexOf(startingPoint);
            var startingPointPipe = new Pipe
            {
                DistanceFromStartingPoint = 0,
                PipeShape = 'S',
                Coordinates = (xCoordinates, yCoordinates)
            };

            return startingPointPipe;
        }

        private List<Pipe> FindConnectedPipes(Pipe pipe)
        {
            var northTargetDirections = "|7FS";
            var eastTargetDirections = "-J7S";
            var southTargetDirections = "S|LJ";
            var westTargetDirections = "-LFS";

            var north = TryGetCharFromInput((pipe.Coordinates.Item1, pipe.Coordinates.Item2 - 1));
            var east = TryGetCharFromInput((pipe.Coordinates.Item1 + 1, pipe.Coordinates.Item2));
            var south = TryGetCharFromInput((pipe.Coordinates.Item1, pipe.Coordinates.Item2 + 1));
            var west = TryGetCharFromInput((pipe.Coordinates.Item1 - 1, pipe.Coordinates.Item2));

            var pipes = new List<Pipe>();
            if (northTargetDirections.Contains(north) && southTargetDirections.Contains(pipe.PipeShape))
            {
                pipes.Add(CreateNewPipeFromPreviousPipe(pipe, (pipe.Coordinates.Item1, pipe.Coordinates.Item2 - 1), north));
            }
            if (eastTargetDirections.Contains(east) && westTargetDirections.Contains(pipe.PipeShape))
            {
                pipes.Add(CreateNewPipeFromPreviousPipe(pipe, (pipe.Coordinates.Item1 + 1, pipe.Coordinates.Item2), east));
            }
            if (southTargetDirections.Contains(south) && northTargetDirections.Contains(pipe.PipeShape))
            {
                pipes.Add(CreateNewPipeFromPreviousPipe(pipe, (pipe.Coordinates.Item1, pipe.Coordinates.Item2 + 1), south));
            }
            if (westTargetDirections.Contains(west) && eastTargetDirections.Contains(pipe.PipeShape))
            {
                pipes.Add(CreateNewPipeFromPreviousPipe(pipe, (pipe.Coordinates.Item1 - 1, pipe.Coordinates.Item2), west));
            }

            return pipes;
        }

        private Pipe CreateNewPipeFromPreviousPipe(Pipe previousPipe, (int, int) newCoordinates, char newPipeShape)
        {
            return new Pipe
            {
                Coordinates = newCoordinates,
                PipeShape = newPipeShape,
                DistanceFromStartingPoint = previousPipe.DistanceFromStartingPoint + 1,
                PreviousPipe = previousPipe,
            };
        }

        private char TryGetCharFromInput((int, int) coordinates)
        {
            var value = '.';
            try
            {
                value = stringInputs[coordinates.Item2].ElementAt(coordinates.Item1);
            }
            catch (Exception ex) { }
            return value;
        }

        private Pipe FindNextPipe(Pipe pipe)
        {
            var adjacentPipes = FindConnectedPipes(pipe);
            var beforeCompareCoordinates = adjacentPipes.Where(p => p.Coordinates != pipe.PreviousPipe.Coordinates).ToList();
            var nextPipe = adjacentPipes.First(p => p.Coordinates != pipe.PreviousPipe.Coordinates);
            nextPipe.PreviousPipe = pipe;
            return nextPipe;
        }

        internal class Pipe()
        {
            public (int, int) Coordinates { get; set; }
            public char PipeShape { get; set; }
            public int DistanceFromStartingPoint { get; set; }
            public Pipe? PreviousPipe { get; set; }
            public Pipe? NextPipe { get; set; }
        }
    }
}