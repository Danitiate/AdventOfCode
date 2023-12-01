﻿using AdventOfCode2023.Models;
using System;

namespace AdventOfCode2023.Services
{
    public class SolutionSelectorService
    {
        public static ISolution? GetRequestedSolution(string userInput)
        {
            var inputAsInt = int.Parse(userInput);
            switch(inputAsInt)
            {
                case 1: return new Day1.Solution();
                default: return null;
            }
        }
    }
}
