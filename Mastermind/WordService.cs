using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    sealed internal class WordService
    {
        public WordService() { }

        public static string GetWord(int theLength)
        {
            if (theLength < 3 || theLength > 22)
            {
                return "";
            }

            string[] aLineArray = File.ReadAllLines($"../../../WordFiles/words_{theLength}.txt", Encoding.UTF8);

            var aRandomizer = new Random();

            int anIdx = aRandomizer.Next(0, aLineArray.Length);

            return aLineArray[anIdx].ToUpper();
        }
    }
}
