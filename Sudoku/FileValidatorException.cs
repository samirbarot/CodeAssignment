using System;
using System.Runtime.Serialization;

namespace Sudoku
{
    [Serializable]
    internal class FileValidatorException : Exception
    {
        public FileValidatorException()
        {
        }

        public FileValidatorException(string message) : base(message)
        {
        }

        public FileValidatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}