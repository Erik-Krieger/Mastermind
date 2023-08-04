namespace Mastermind
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Mastermind V1");

            int aCharacterCount = UserInterface.GetCharacterCount();
            Game aGame = new(aCharacterCount);
            UserInterface.RunGameLoop(aGame);
        }

    }
}