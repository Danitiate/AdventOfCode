using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_10
{
    /**
     *  PART 2
     *  1. Find the coordinates of every corner in the largest loop found in part 1
     *  2. Consider every tile in the space between (LoopXMin, LoopYMin) to (LoopXMax, LoopYMax)
     *  3. Perform the Even-Odd algorithm on every tile in this space (https://en.wikipedia.org/wiki/Even%E2%80%93odd_rule)
     *  4. If there are an odd number of edges crossed, the tile is inside the loop
     *  5. Sum amount of odd numbered edges crossed
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var amountOfTilesEnclosedInLoop = GetAmountOfTilesEnclosedInLoop();
            return amountOfTilesEnclosedInLoop.ToString();
        }

        private int GetAmountOfTilesEnclosedInLoop()
        {
            var amountOfTilesEnclosedInLoop = 0;
            var cornersInLoop = GetCornersInLoop();
            var loopXMin = cornersInLoop.MinBy(xy => xy.Item1).Item1;
            var loopXMax = cornersInLoop.MaxBy(xy => xy.Item1).Item1;
            var loopYMin = cornersInLoop.MinBy(xy => xy.Item2).Item2;
            var loopYMax = cornersInLoop.MaxBy(xy => xy.Item2).Item2;

            for (int i = loopYMin; i < loopYMax; i++)
            {
                for(int j = loopXMin; j < loopXMax; j++)
                {
                    var tile = stringInputs[i].ElementAt(j);
                    if (TileIsInsideOfLoop((j, i), cornersInLoop))
                    {
                        amountOfTilesEnclosedInLoop++;
                    } 
                }
            }

            return amountOfTilesEnclosedInLoop;
        }

        private List<(int, int)> GetCornersInLoop()
        {
            var cornerShapes = "LJ7FS";
            List<(int, int)> points = new List<(int, int)>();
            var loop = GetLoop();
            points.Add(loop.Coordinates);
            var previousPipe = loop.PreviousPipe;
            while (previousPipe!.PipeShape != 'S')
            {
                if (cornerShapes.Contains(previousPipe!.PipeShape))
                {
                    points.Add((previousPipe.Coordinates));
                }
                previousPipe = previousPipe.PreviousPipe;
            }

            return points;
        }

        private Pipe GetLoop()
        {
            var startingPoint = FindStartingPoint();
            var connectedPipes = FindConnectedPipes(startingPoint);
            var nextPipe = connectedPipes[0];
            while (nextPipe.PipeShape != 'S')
            {
                nextPipe = FindNextPipe(nextPipe);
            }
            startingPoint.PreviousPipe = nextPipe.PreviousPipe;
            return startingPoint;
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
            catch (Exception) { }
            return value;
        }

        private Pipe FindNextPipe(Pipe pipe)
        {
            var adjacentPipes = FindConnectedPipes(pipe);
            var beforeCompareCoordinates = adjacentPipes.Where(p => p.Coordinates != pipe.PreviousPipe!.Coordinates).ToList();
            var nextPipe = adjacentPipes.First(p => p.Coordinates != pipe.PreviousPipe!.Coordinates);
            nextPipe.PreviousPipe = pipe;
            return nextPipe;
        }

        private bool TileIsInsideOfLoop((int, int) coordinates, List<(int, int)> cornersInLoop)
        {
            var tileIsInsideOfPolygon = false;
            for (int i = 0, j = cornersInLoop.Count - 1; i < cornersInLoop.Count; j = i++)
            {
                var x0y0 = cornersInLoop[i];
                var x1y1 = cornersInLoop[j];
                if (CoordinatesIsOnACorner(coordinates, x0y0))
                {
                    return false;
                }
                if (CoordinatesIsVerticallyBetweenTwoPoints(coordinates.Item2, x0y0.Item2, x1y1.Item2))
                {
                    var deltaX = x1y1.Item1 - x0y0.Item1;
                    var deltaY = x1y1.Item2 - x0y0.Item2;
                    var deltaPX = coordinates.Item1 - x0y0.Item1;
                    var deltaPY = coordinates.Item2 - x0y0.Item2;
                    var slope = (deltaPX * deltaY) - (deltaX * deltaPY);
                    if (CoordinatesIsOnAnEdge(slope))
                    {
                        return false;
                    }
                    else if(CoordinatesIsToTheLeftOfVector(slope) != VectorIsMovingDownwards(deltaY))
                    {
                        tileIsInsideOfPolygon = !tileIsInsideOfPolygon;
                    }
                }
                else if (CoordinatesIsBetweenAlignedVerticalPoints(coordinates, x0y0, x1y1))
                {
                    return false;
                }
            }

            return tileIsInsideOfPolygon;
        }

        private bool CoordinatesIsOnACorner((int, int) coordinates, (int, int) x0y0)
        {
            return coordinates == x0y0;
        }

        private bool CoordinatesIsVerticallyBetweenTwoPoints(int coordinatesY, int y0, int y1)
        {
            return y0 > coordinatesY != y1 > coordinatesY;
        }

        private bool CoordinatesIsOnAnEdge(int slope)
        {
            return slope == 0;
        }

        private bool CoordinatesIsToTheLeftOfVector(int slope)
        {
            return slope < 0;
        }

        private bool VectorIsMovingDownwards(int deltaY)
        {
            return deltaY < 0;
        }

        private bool CoordinatesIsBetweenAlignedVerticalPoints((int, int) coordinates, (int, int) x0y0, (int, int) x1y1)
        {
            var coordinatesIsOnTheSameLine = x0y0.Item2 == coordinates.Item2 && x1y1.Item2 == coordinates.Item2;
            var xCoordinatesIsBetweenTwoPoints = (x0y0.Item1 > coordinates.Item1) != (x1y1.Item1 > coordinates.Item1);
            return coordinatesIsOnTheSameLine && xCoordinatesIsBetweenTwoPoints;
        }
    }
}