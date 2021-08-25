using System;

namespace NavalBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            Game g = new Game();
            g.PlayGame();
        }
    }
}
