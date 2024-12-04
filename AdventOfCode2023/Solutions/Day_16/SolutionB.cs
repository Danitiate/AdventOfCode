using AdventOfCode.Core.Models;
using System;
using System.Linq;

namespace AdventOfCode2023.Solutions.Day_16
{
    /**
     *  PART 2
     *  1. Continue on the logic created in part 1 and try out every possible starting point and direction
     *  2. Grid needs to be recreated for each iteration as the original grid has been altered after traversing
     *  3. Traverse and mark each cell along the way
     *      2a. If we encounter a '.', continue the same direction
     *      2b. If we encounter a '-', split the direction if the current direction is north or south. Otherwise, continue the same direction
     *      2c. If we encounter a '|', split the direction if the current direction is west or east. Otherwise, continue the same direction
     *      2d. There is no need to continue traversing if we are entering a cell we have already entered before from the same direction.
     *  4. After all cells are traversed and marked, count the amount of energized cells and return the results
     **/
    public class SolutionB : Solution
    {
        protected override string GetSolutionOutput()
        {
            var energizedTiles = CountEnergizedTiles();
            return energizedTiles.ToString();
        }

        private int CountEnergizedTiles()
        {
            var firstRow = stringInputs[0];
            var maxAmountOfEnergizedTiles = 0;
            for (int i = 0; i < firstRow.Length; i++)
            {
                var grid = ParseInputIntoGrid();
                TraverseGrid(grid, grid[i, 0], Direction.SOUTH);
                maxAmountOfEnergizedTiles = Math.Max(maxAmountOfEnergizedTiles, CountAmountOfEnergizedTiles(grid));
            }

            for (int i = 0; i < stringInputs.Count; i++)
            {
                var grid = ParseInputIntoGrid();
                TraverseGrid(grid, grid[0, i], Direction.EAST);
                maxAmountOfEnergizedTiles = Math.Max(maxAmountOfEnergizedTiles, CountAmountOfEnergizedTiles(grid));
            }

            return maxAmountOfEnergizedTiles;
        }

        private Cell[,] ParseInputIntoGrid()
        {
            var firstRow = stringInputs[0];
            var grid = new Cell[firstRow.Length, stringInputs.Count];
            for (int i = 0; i < stringInputs.Count; i++)
            {
                var currentRow = stringInputs[i];
                for (int j = 0; j < currentRow.Length; j++)
                {
                    var newCell = new Cell
                    {
                        X = j,
                        Y = i,
                        Tile = currentRow.ElementAt(j)
                    };

                    grid[j, i] = newCell;
                }
            }

            return grid;
        }

        private void TraverseGrid(Cell[,] grid, Cell currentCell, Direction currentDirection)
        {
            currentCell.Energized = true;
            if (currentCell.PreviousDirections.HasFlag(currentDirection))
            {
                return;
            }
            currentCell.PreviousDirections |= currentDirection;

            currentDirection = DetermineNextDirection(currentDirection, currentCell);
            if (currentDirection.HasFlag(Direction.NORTH) && currentCell.Y > 0)
            {
                TraverseGrid(grid, grid[currentCell.X, currentCell.Y - 1], Direction.NORTH);
            }

            if (currentDirection.HasFlag(Direction.EAST) && currentCell.X < grid.GetLength(0) - 1)
            {
                TraverseGrid(grid, grid[currentCell.X + 1, currentCell.Y], Direction.EAST);
            }

            if (currentDirection.HasFlag(Direction.SOUTH) && currentCell.Y < grid.GetLength(1) - 1)
            {
                TraverseGrid(grid, grid[currentCell.X, currentCell.Y + 1], Direction.SOUTH);
            }

            if (currentDirection.HasFlag(Direction.WEST) && currentCell.X > 0)
            {
                TraverseGrid(grid, grid[currentCell.X - 1, currentCell.Y], Direction.WEST);
            }
        }

        private Direction DetermineNextDirection(Direction currentDirection, Cell currentCell)
        {
            if (currentCell.Tile == '|' && (currentDirection == Direction.EAST || currentDirection == Direction.WEST)) 
            {
                return Direction.NORTH | Direction.SOUTH;
            }

            if (currentCell.Tile == '-' && (currentDirection == Direction.NORTH || currentDirection == Direction.SOUTH))
            {
                return Direction.EAST | Direction.WEST;
            }

            if (currentCell.Tile == '/')
            {
                if (currentDirection == Direction.NORTH)
                {
                    return Direction.EAST;
                }
                if (currentDirection == Direction.EAST)
                {
                    return Direction.NORTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    return Direction.WEST;
                }
                if (currentDirection == Direction.WEST)
                {
                    return Direction.SOUTH;
                }
            }

            if (currentCell.Tile == '\\')
            {
                if (currentDirection == Direction.NORTH)
                {
                    return Direction.WEST;
                }
                if (currentDirection == Direction.WEST)
                {
                    return Direction.NORTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    return Direction.EAST;
                }
                if (currentDirection == Direction.EAST)
                {
                    return Direction.SOUTH;
                }
            }

            return currentDirection;
        }

        private int CountAmountOfEnergizedTiles(Cell[,] grid)
        {
            var amountOfEnergizedTiles = 0;
            foreach (var cell in grid)
            {
                if (cell.Energized)
                {
                    amountOfEnergizedTiles++;
                }
            }

            return amountOfEnergizedTiles;
        }
    }
}