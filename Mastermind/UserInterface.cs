using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mastermind
{
    internal class UserInterface
    {
        public UserInterface() { }

        public static GameType QueryGameType()
        {
            Console.WriteLine("Which gamemode would you like to play?");
            Console.WriteLine("Your options: 'numbers', 'words', 'quit'");

            while (true)
            {
                var aLine = Console.ReadLine();

                if (aLine == null)
                {
                    continue;
                }

                aLine = aLine.ToLower();

                if (aLine == "quit" || aLine == "exit")
                {
                    return GameType.None;
                }

                if (aLine == "numbers" || aLine == "n")
                {
                    return GameType.Numbers;
                }

                if (aLine == "words" || aLine == "w")
                {
                    return GameType.Words;
                }

                Console.WriteLine("Invalid Input. Only use 'words', 'numbers' or 'quit'");
                Console.WriteLine("Which gamemode would you like to play?");
            }
        }

        /// <summary>
        ///     Queries the user for the amount of characters to use.
        /// </summary>
        /// <returns></returns>
        public static int QueryCharacterCount(GameType theGameType)
        {
            Console.WriteLine("Enter the amount of Characters you want:");

            if (theGameType == GameType.Words)
            {
                Console.WriteLine("The word gamemode is limited to the range of 3-22 letters.");
            }

            while (true)
            {
                var aLine = Console.ReadLine();

                // Check for null
                if (aLine == null)
                {
                    continue;
                }

                // Check for termination
                if (aLine == "quit")
                {
                    return -1;
                }

                // Check for actual value
                if (int.TryParse(aLine, out var aValue) && aValue > 0) {
                    if (theGameType == GameType.Words && (aValue < 3 || aValue > 22))
                    {
                        Console.WriteLine("You have to choose a length between 3 and 22.");
                        continue;
                    }

                    Console.WriteLine();
                    return aValue;
                }

                Console.WriteLine("You're only allowed to enter positive non-zero integers.");
            }
        }

        /// <summary>
        ///     Runs the Main Loop of the Game
        /// </summary>
        /// <param name="theGame"></param>
        public static void RunGameLoop(Game theGame)
        {
            Console.WriteLine("Enter your guess:");

            while (true)
            {
                var aLine = Console.ReadLine();
                if (aLine == null)
                {
                    continue;
                }
                
                if (aLine == "quit")
                {
                    return;
                }

                try
                {
                    if (theGame.Parse(aLine))
                    {
                        break;
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Something went terribly wrong.");
                    Console.WriteLine(e);
                    Console.WriteLine();
                    return;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nYou got it, the correct answer was {theGame.GetTargetValue()}");
        }
    }
}
