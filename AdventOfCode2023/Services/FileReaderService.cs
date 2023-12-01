using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2023.Services
{
    public class FileReaderService
    {
        public static List<string> ReadFile(string filePath)
        {
            var lines = new List<string>();
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    var currentLine = "";
                    while (currentLine != null)
                    {
                        currentLine = streamReader.ReadLine();
                        if (currentLine != null)
                        {
                            lines.Add(currentLine);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occured when reading file {filePath}: ");
                Console.WriteLine(ex);
            }

            return lines;
        }
    }
}
