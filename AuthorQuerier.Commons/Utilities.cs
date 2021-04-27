using System;
using System.Collections.Generic;
using System.Text;
namespace AuthorQuerier
{
    public class Utilities
    {
        /// <summary>
        /// Parses a string to an integer.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ParseInput(string input)
        {
            bool success = int.TryParse(input, out int threshold);
            if (!success)
            {
                throw new FormatException("Invalid input value. Please enter a numeric value");
            }
            return threshold;
        }
    }
}
