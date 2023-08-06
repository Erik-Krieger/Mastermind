using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    internal class UserInterface
    {
        public UserInterface() { }

        /// <summary>
        ///     Queries the user for the amount of characters to use.
        /// </summary>
        /// <returns></returns>
        public static int GetCharacterCount()
        {
            Console.WriteLine("Enter the amount of Characters you want:");

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
