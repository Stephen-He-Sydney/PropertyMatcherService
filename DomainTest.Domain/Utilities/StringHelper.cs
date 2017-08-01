using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DomainTest.Domain.Utilities
{
    public static class StringHelper
    {
        /// <summary>
        /// Remove punctuation, symtbol and whitespace from an input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemovePunctuation(string input)
        {
            var result = Regex.Replace(input, @"[^\w]", string.Empty);

            return result;
        }

        /// <summary>
        /// Reverse the word sequence
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReverseWordSequence(string input)
        {
            var result = String.Join(" ", input.Split(' ').Reverse());
            return result;
        }

        /// <summary>
        /// Remove any whitespace with a single space
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(string input)
        {
            var result = Regex.Replace(input, @"\s+", " ");
            return result;
        }
    }
}
