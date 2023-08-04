using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    internal class Game
    {
        public string m_TargetValue;
        private readonly Dictionary<char, int> m_LookupTable;

        public Game(int theLength) { 
            m_TargetValue = GenerateTargetValue(theLength);
            m_LookupTable = CountChars(m_TargetValue);
        }

        public static Dictionary<char, int> CountChars(string theString)
        {
            if (theString == null)
            {
                return new Dictionary<char, int>();
            }

            Dictionary<char, int> aDict = new(theString.Length);

            for (int aIdx = 0; aIdx < theString.Length; aIdx++)
            {
                if (aDict.ContainsKey(theString[aIdx])) { 
                    aDict[theString[aIdx]]++;
                } else
                {
                    aDict.Add(theString[aIdx], 1);
                }
            }

            return aDict;
        }

        public static string GenerateTargetValue(int theLength)
        {
            Random aRng = new();
            string aGeneratedValue = string.Empty;

            for (int aIdx = 0; aIdx < theLength; aIdx++)
            {
                int aValue = aRng.Next(0, 10);
                aGeneratedValue += aValue.ToString();
            }

            return aGeneratedValue;
        }

        public bool Parse(string theAttempt)
        {
            if (theAttempt == null)
            {
                throw new ArgumentNullException("The 'theAttempt' parameter cannot be null");
            }

            if (theAttempt.Length != m_TargetValue.Length)
            {
                throw new ArgumentException($"The length of the guess has to be equal to the number of characters provided in the beginning. ({m_TargetValue.Length})");
            }

            // Make sure that all Characters are digits.
            bool isAllNum = theAttempt.All(char.IsAsciiDigit);
            if (!isAllNum) {
                throw new ArgumentException("You're only allowed to input digits");
            }

            Dictionary<char, int> aLocalCopyOfDict = new(m_LookupTable); 

            for (int aIdx = 0; aIdx<theAttempt.Length; aIdx++)
            {
                char aCurrentChar = theAttempt[aIdx];

                // Print the character grey, if it's not in the solution or appeared in the guess more often that in the solution.
                if (!aLocalCopyOfDict.TryGetValue(aCurrentChar, out int aCharCount) || aCharCount < 1)
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.DarkGray);
                    continue;
                }

                if (aCurrentChar == m_TargetValue[aIdx])
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.Green);
                }
                else
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.Yellow);
                }

                // Decrement the remaining character count.
                aLocalCopyOfDict[aCurrentChar]--;
            }

            Console.WriteLine();
            Console.WriteLine(m_TargetValue);

            return false;
        }

        /// <summary>
        ///     Prints the designated char in the designated color.
        /// </summary>
        /// <param name="theChar"></param>
        /// <param name="theColor"></param>
        public static void PrintColoredChar(char theChar, ConsoleColor theColor = ConsoleColor.White)
        {
            Console.ForegroundColor = theColor;
            Console.Write(theChar);
            Console.ResetColor();
        }
    }
}
