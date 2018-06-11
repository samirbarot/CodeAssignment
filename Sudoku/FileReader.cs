using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoku
{
    public class FileReader
    {
        public string FileName { get; set; }
        public int SudokuSize { get; set; }

        public FileReader(string fileName, int sudokuSize)
        {
            FileName = fileName;
            SudokuSize = sudokuSize;
        }

        public int[,] ReadFile()
        {
            int rowCounter = 0;
            string line = string.Empty;
            int[,] result = new int[SudokuSize, SudokuSize];

            StreamReader file = new StreamReader(FileName);

            while ((line = file.ReadLine()) != null)
            {
                if (line == string.Empty)
                    continue;

                if (rowCounter == SudokuSize)
                    throw new Exception($"Number of lines in file is more than { SudokuSize }");

                if (line.Length != SudokuSize)
                    throw new Exception($"Numbers per row are not equal to { SudokuSize }");

                var colCounter = 0;
                foreach (var character in line.ToCharArray())
                {
                    if (!Char.IsNumber(character))
                    {
                        throw new Exception("Input should be a number. Invalid input ... ");
                    }

                    result[rowCounter, colCounter] = (int)Char.GetNumericValue(character);
                    colCounter++;
                }

                rowCounter++;
            }

            if (rowCounter != SudokuSize)
                throw new Exception($"Number of lines in file is less or more than { SudokuSize }");

            file.Close();

            return result;
        }
    }
}
