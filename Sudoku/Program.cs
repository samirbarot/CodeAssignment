using System;
using System.IO;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudokuSize = 9;
            var filename = Environment.CurrentDirectory + "input_sudoku.txt";

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the file name as first parameter. For example dotnet Sudoku.dll input_sudoku.txt");
                return;
            }

            if (args.Length > 0) filename = args[0];
            if (!File.Exists(filename)) throw new FileNotFoundException();

            Console.WriteLine("Reading file ...");

            try
            {
                var reader = new FileReader(filename, sudokuSize);
                var sudokuGrid = reader.ReadFile();

                var validator = new FileValidator(sudokuSize);
                validator.ValidateGrid(sudokuGrid);

                Console.WriteLine("Your file is valid!");

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: " + filename + " not found");
            }
            catch (FileValidatorException ex)
            {
                Console.WriteLine("Sudoku puzzle is invalid: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in input file: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to leave program ...");
            Console.ReadLine();
        }
    }
}

