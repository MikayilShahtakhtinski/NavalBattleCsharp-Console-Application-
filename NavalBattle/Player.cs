using System;
using System.Collections.Generic;
using System.Text;

namespace NavalBattle
{
    class Player
    {
        private string name;
        private Board board;
        private Ship[] ships;

        public string Name { get => name; set => name = value; }
        public Board Board { get => board; set => board = value; }
        public Ship[] Ships { get => ships; set => ships = value; }

        public Player(string name)
        {
            this.name = name;
            this.board = new Board(10);
            this.board.B_show();
            this.ships = new Ship[10];
            P_createShips();
        }

        public void P_createShips()
        {
            int[] ship_sizes = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
            Ship s;
            for (int i = 0;i < ship_sizes.Length; i++)
            {
                Console.WriteLine("Putting ship of size {0}", ship_sizes[i]);
                while (true)
                {
                    if (ship_sizes[i] == 1)
                    {
                        s = new Ship(P_getIntialPoint(), new ShipCell(0,0), ship_sizes[i]);
                    }
                    else
                    {
                        s = new Ship(P_getIntialPoint(), P_getDirection(), ship_sizes[i]);
                    }
                    if (board.B_canAddShip(s))
                    {
                        this.ships[i] = s;
                        this.board.B_addShip(s);
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nCan't add, change direction or initial point of this ship!\n");
                    Console.ResetColor();

                }
                this.board.B_show();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have successfully placed all the ships\n");
            Console.ResetColor();
        }

        public ShipCell P_getIntialPoint()
        {
            int x, y;
            string input;
            int[] output = new int[2];
            while (true)
            {
                Console.WriteLine("Dear {0}, please select initial position of ship on the board", this.name);
                Console.Write("In this order: column(letter) and row (A7): ");
                input = Console.ReadLine();
                output = new int[2];
                if (P_checkInput(input, output))
                {
                    y = output[0];
                    x = output[1];
                    break;
                }
            }
            return new ShipCell(x, y);
        }

        public bool P_checkInput(string input,int[] output)
        {
            char[] letters = input.ToCharArray();
            if (letters.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nMore or less than 1 input! Only 1(one char and one digit number) input must be written\n");
                Console.ResetColor();
                return false;
            }
            if (Char.IsDigit(letters[0]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou wrote a number in column part instead of character!\n");
                Console.ResetColor();
                return false;
            }
            if(!Char.IsDigit(letters[1]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThere is no number in row part in your input!\n");
                Console.ResetColor();
                return false;
            }
            int letter = P_convertToIndex(letters[0]);
            if (letter == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThere is no such letter on boards columns!\n");
                Console.ResetColor();
                return false;
            }
            output[0] = letter;
            output[1] = int.Parse(letters[1].ToString());
            return true;
        }

        public int P_convertToIndex(char ch)
        {
            for (int i = 0; i < this.board.GetBoard.GetLength(0); i++)
            {
                if (Convert.ToChar(65 + i) == Char.ToUpper(ch))
                {
                    return i;
                }
            }
            return -1;
        }

        public ShipCell P_getDirection()
        {
            string direction;
            while (true)
            { 
                Console.Write("Dear {0}, choose direction of placement of your ship(left,right,up,down): ", this.name);

                direction = Console.ReadLine().ToLower();

                if (direction.Equals("left"))
                {
                    return new ShipCell(0, -1);
                }
                else if (direction.Equals("right"))
                {
                    return new ShipCell(0, 1);
                }
                else if (direction.Equals("up"))
                {
                    return new ShipCell(-1, 0);
                }
                else if (direction.Equals("down"))
                {
                    return new ShipCell(1, 0);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong input of direction!\n");
                    Console.ResetColor();
                }
            }
        }

        public bool P_checkHit(Player p,int x,int y)
        {
            return p.Board.B_canHit(x, y);
        }

        public void P_hit(Player p)
        {
            int x, y;
            string input;
            int[] output = new int[2];
            while (true)
            {
                Console.Write("Dear {0}, please select coordinates on board to attack (A7): ",this.name);
                input = Console.ReadLine();
                output = new int[2];
                if (P_checkInput(input, output))
                {
                    y = output[0];
                    x = output[1];
                }
                else { continue; }
                if (P_checkHit(p, x, y)) {
                    p.board.B_hit(x, y);
                    p.P_revealShips();
                    p.board.B_show(false);
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCan't hit on this coordinates!\n");
                Console.ResetColor();
            }
        }

        public void P_revealShips()
        {
            foreach(Ship ship in this.ships)
            {
                this.board.B_modifyIfSunk(ship);
            }
        }

        public bool P_isLostGame()
        {
            foreach (Ship ship in this.ships)
            {
                if (!ship.S_isSink())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
