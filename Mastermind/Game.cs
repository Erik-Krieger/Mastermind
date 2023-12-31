﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public enum GameType
    {
        None,
        Numbers,
        Words,
    }

    internal class Game
    {
        public string m_TargetValue;
        private readonly Dictionary<char, int> m_LookupTable;
        private readonly GameType m_GameType;

        public Game(GameType theType, int theLength) {
            m_GameType = theType;
            m_TargetValue = GenerateTargetValue(theLength);
            m_LookupTable = CountChars(m_TargetValue);
        }

        /// <summary>
        ///     Provides external access to the Target Value.
        /// </summary>
        /// <returns></returns>
        public string GetTargetValue()
        {
            return m_TargetValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theLength"></param>
        /// <returns></returns>
        public string GenerateTargetValue(int theLength)
        {
            if (m_GameType == GameType.Words)
            {
                return WordService.GetWord(theLength);
            }

            Random aRng = new();
            string aGeneratedValue = string.Empty;

            for (int aIdx = 0; aIdx < theLength; aIdx++)
            {
                int aValue = aRng.Next(0, 10);
                aGeneratedValue += aValue.ToString();
            }

            return aGeneratedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theAttempt"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool Parse(string theAttempt)
        {
            if (theAttempt == null)
            {
                throw new ArgumentNullException(nameof(theAttempt), "The 'theAttempt' parameter cannot be null");
            }

            if (theAttempt.Length != m_TargetValue.Length)
            {
                throw new ArgumentException($"The length of the guess has to be equal to the number of characters provided in the beginning. ({m_TargetValue.Length})");
            }

            // Make sure that the input is valid for the selected game type.
            ValidateInput(ref theAttempt);

            // Create a local copy of the character count dict.
            Dictionary<char, int> aLocalCopyOfDict = new(m_LookupTable);
            bool isCorrectGuess = true;
            theAttempt = theAttempt.ToUpper();

            for (int aIdx = 0; aIdx<theAttempt.Length; aIdx++)
            {
                char aCurrentChar = theAttempt[aIdx];

                // Print the character grey, if it's not in the solution or appeared in the guess more often that in the solution.
                if (!aLocalCopyOfDict.TryGetValue(aCurrentChar, out int aCharCount) || aCharCount < 1)
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.DarkGray);
                    isCorrectGuess = false;
                    continue;
                }

                if (aCurrentChar == m_TargetValue[aIdx])
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.Green);
                }
                else
                {
                    PrintColoredChar(aCurrentChar, ConsoleColor.Yellow);
                    isCorrectGuess = false;
                }

                // Decrement the remaining character count.
                aLocalCopyOfDict[aCurrentChar]--;
            }

            Console.WriteLine();

            return isCorrectGuess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theInput"></param>
        /// <returns></returns>
        private void ValidateInput(ref string theInput)
        {
            if (theInput == null)
            {
                throw new ArgumentNullException(nameof(theInput), "theInput cannot be null, something has gone terribly wrong.");
            }

            if (m_GameType == GameType.Numbers)
            {
                // Make sure that all Characters are digits.
                bool isAllNum = theInput.All(char.IsAsciiDigit);
                if (!isAllNum)
                {
                    throw new ArgumentException("You're only allowed to input digits.");
                }
            }
            else if (m_GameType == GameType.Words)
            {
                // Make sure that all Characters are letters.
                bool isAllLetters = theInput.All(char.IsLetter);
                if (!isAllLetters)
                {
                    throw new ArgumentException("You're only allowed to input words.");
                }
            }
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
