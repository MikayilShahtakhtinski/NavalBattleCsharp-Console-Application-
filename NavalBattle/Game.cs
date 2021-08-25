using System;
using System.Collections.Generic;
using System.Text;

namespace NavalBattle
{
    class Game
    {
        private Player p1;
        private Player p2;

        public Player P1 { get => p1; set => p1 = value; }
        public Player P2 { get => p2; set => p2 = value; }

        public Game()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n#################################");
            Console.WriteLine("Welcome to the Naval Battle game!");
            Console.WriteLine("#################################\n");
            Console.ResetColor();

            Console.Write("Enter the name of first player: ");
            this.p1 = new Player(Console.ReadLine());

            Console.WriteLine("Type anything to continue");
            Console.ReadLine();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nConsole has been cleaned to prevent cheating\n");
            Console.ResetColor();

            Console.Write("Enter the name of second player: ");
            this.p2 = new Player(Console.ReadLine());

            Console.WriteLine("Type anything to continue");
            Console.ReadLine();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nConsole has been cleaned to prevent cheating\n");
            Console.ResetColor();
        }

        public void PlayGame()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n#################");
            Console.WriteLine("Game has started!");
            Console.WriteLine("#################\n");
            Console.ResetColor();
            int turn = 1;
            while (!G_gameOver())
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("######################\n");
                if (turn==1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}'s board:", p2.Name);
                    Console.ResetColor();
                    
                    p2.Board.B_show(false);
                    p1.P_hit(p2);
                }
                if (turn==-1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}'s board:", p1.Name);
                    Console.ResetColor();
                    p1.Board.B_show(false);
                    p2.P_hit(p1);
                }
                turn *= -1;
                Console.WriteLine("Type anything to continue");
                Console.ReadLine();
            }
        }

        public bool G_gameOver()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (p1.P_isLostGame())
            {         
                Console.WriteLine("\nCongratulations, {0}, you win!\n", p2.Name);
                Console.ResetColor();
                return true;
            }
            if (p2.P_isLostGame())
            {
                Console.WriteLine("\n Congratulations, {0}, you win!\n", p1.Name);
                Console.ResetColor();
                return true;
            }
            Console.ResetColor();
            return false;
        }

        
    }
}
