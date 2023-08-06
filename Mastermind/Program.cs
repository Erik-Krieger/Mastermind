namespace Mastermind
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Mastermind v2\n");

            GameType aGameType = UserInterface.QueryGameType();
            if (aGameType == GameType.None)
            {
                return;
            }

            int aCharacterCount = UserInterface.QueryCharacterCount(aGameType);

            Game aGame = new(aGameType, aCharacterCount);
            UserInterface.RunGameLoop(aGame);
        }

    }
}