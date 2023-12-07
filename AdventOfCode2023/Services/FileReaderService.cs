using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    var fileContent = streamReader.ReadToEnd();
                    lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
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
