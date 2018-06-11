using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Sudoku
{
    class FileValidator
    {
        public int SudokuSize { get; set; }

        public FileValidator(int sudokuSize)
        {
            SudokuSize = sudokuSize;
        }

        public void ValidateGrid(int[,] sudokuGrid)
        {
            if (sudokuGrid == null)
                throw new ArgumentNullException("Not a valid grid for Sudoku");

            for (int i = 0; i < SudokuSize; i++)
            {
                var row = GetRowValues(sudokuGrid, i);

                if (!((row.Count(p => p < 1) == 0) || (row.Count(p => p > SudokuSize) == 0)))
                    throw new FileValidatorException($"Row values are not between 1 and 9");

                if (!(row.Distinct().Count() == SudokuSize))
                    throw new FileValidatorException($"Duplicate Values in row { i } ");
            }

            for (int i = 0; i < SudokuSize; i++)
            {
                var col = GetColumnValues(sudokuGrid, i);

                if (!(col.Distinct().Count() == SudokuSize))
                        throw new FileValidatorException($"Duplicate Values in column { i } ");
            }

            var squareGrid = Convert.ToInt32(Math.Floor(Math.Sqrt(SudokuSize)));

            for (int x = 0; x < squareGrid; x++)
            {
                for (int y = 0; y < squareGrid; y++)
                {
                    var index = 0;
                    var squareArea = new int[squareGrid * squareGrid];

                    for (int row = 0; row < squareGrid; row++)
                        for (int col = 0; col < squareGrid; col++)
                            squareArea[index++] = sudokuGrid[x * squareGrid + row, y * squareGrid + col];

                    if (!(squareArea.Distinct().Count() == SudokuSize))
                            throw new FileValidatorException($"Duplicate Values in block { x } & { y } ");
                }
            }
        }

        public static int[] GetRowValues(int[,] array, int row)
        {
            int cols = array.GetUpperBound(1) + 1;
            int[] result = new int[cols];
            int size = sizeof(int);

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }

        public static int[] GetColumnValues(int[,] array, int col)
        {
            var rows = array.GetLength(1);
            int[] result = new int[rows];

            for (int i = 0; i < rows; i++)
                result[i] = array[i, col];

            return result;
        }
    }
}
