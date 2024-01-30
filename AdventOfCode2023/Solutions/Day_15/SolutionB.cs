using AdventOfCode2023.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day_15
{
    /**
     *  PART 2
     *  1. Input is a single line of comma seperated strings. Parse input into a list of strings using string.split(",")
     *  2. For each string in the list created in step 1, determine whether we need to add or remove a lens (find the first (and only) non-word character)
     *  3. Split the string again on the location of the operator, giving us both the label and its focal length
     *  4. Use the hash function created in Part 1 to determine the box value of the label
     *  5. If the operator is a '-':
     *      a. Remove the lens with the current label in the current box number
     *      b. Do nothing if not exist
     *  6. If the operator is a '=':
     *      a. Add the lens to the box if not exist
     *      b. Replace the lens in the box at its current index if it already exists
     *  7. After all strings in the input are handled, calculate the lens focusing power by summing all lenses:
     *      a. boxValue * lensIndex * focalLength
     *      b. boxValue is equal to the boxIndex + 1
     *      c. lensIndex is the index of the given lens in the box list
     *      d. focalLength is the value assigned to lens
     **/
    public class SolutionB : Solution
    {
        private record Lens(string Label, int FocalLength);

        private Dictionary<int, List<Lens>> Boxes = new Dictionary<int, List<Lens>>();

        protected override string GetSolutionOutput()
        {
            var hashSum = GetSumOfHashAlgorithm();
            return hashSum.ToString();
        }

        private int GetSumOfHashAlgorithm()
        {
            var strings = ParseInputIntoStrings();
            foreach(var s in strings) 
            {
                AddOrRemoveLens(s);
            }

            return CalculateLensFocusingPower();
        }

        private List<string> ParseInputIntoStrings()
        {
            var allStrings = stringInputs[0];
            return new List<string>(allStrings.Split(','));
        }

        private void AddOrRemoveLens(string currentString)
        {
            var operation = Regex.Match(currentString, "\\W").Value; // Gets either '-' or '=' from currentString
            var labelAndLens = currentString.Split(operation);
            var boxNumber = GetHashValueOfString(labelAndLens[0]);

            if (operation == "-")
            {
                RemoveLensFromBox(boxNumber, labelAndLens[0]);
            }
            else if (operation == "=")
            {
                var lens = new Lens(labelAndLens[0], Int32.Parse(labelAndLens[1]));
                AddLensToBox(boxNumber, lens);
            }
        }

        private int GetHashValueOfString(string currentString)
        {
            var hashValue = 0;
            foreach (var character in currentString)
            {
                hashValue += (int)character;
                hashValue *= 17;
                hashValue %= 256;
            }

            return hashValue;
        }

        private void RemoveLensFromBox(int boxNumber, string label)
        {
            if (Boxes.TryGetValue(boxNumber, out var box)) 
            {
                foreach(var lens in box)
                {
                    if (lens.Label == label)
                    {
                        box.Remove(lens);
                        break;
                    }
                }
            }
        }

        private void AddLensToBox(int boxNumber, Lens newLens)
        {
            if (Boxes.TryGetValue(boxNumber, out var box))
            {
                var inserted = false;
                for (int i = 0; i < box.Count; i++)
                {
                    var lens = box[i];
                    if (lens.Label == newLens.Label)
                    {
                        box.RemoveAt(i);
                        box.Insert(i, newLens);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                {
                    box.Add(newLens);
                }
            }
            else
            {
                Boxes.Add(boxNumber, new List<Lens> { newLens });
            }
        }

        private int CalculateLensFocusingPower()
        {
            var sum = 0;
            foreach(var box in Boxes)
            {
                var boxValue = box.Key + 1;
                var lensIndex = 1;
                foreach(var lens in box.Value)
                {
                    sum += boxValue * lensIndex * lens.FocalLength;
                    lensIndex++;
                }
            }
            return sum;
        }
    }
}