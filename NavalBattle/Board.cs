using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NavalBattle
{
    class Board
    {
        private ShipCell[,] board;

        public ShipCell[,] GetBoard { get => board; set => board = value; }

        public Board(int size)
        {
            this.board = new ShipCell[size,size];
            B_initialize();
        }

        public void B_initialize() { 
            for(int i = 0; i < this.board.GetLength(0); i++)
            {
                for(int j = 0; j < this.board.GetLength(1); j++)
                {
                    this.board[i, j] = new ShipCell(i, j);
                }
            }
        }
        

        public void B_letters()
        {
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                Console.Write(Convert.ToChar(i + 65) + " ");
            }
            Console.WriteLine();
        }

        public void B_show(bool mode = true)
        {
            Console.WriteLine();
            Console.Write("   ");
            B_letters();
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                Console.Write(i+" ");
                if (i < 10) { Console.Write(" "); }
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    if (mode)
                    {
                        if(this.board[i, j].Letter == '~')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(this.board[i, j].Letter);
                        }
                        else if(this.board[i, j].Letter == 'S')
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\u25a0");
                        }
                    }
                    else
                    {
                        if(this.board[i, j].Letter == 'S' || this.board[i, j].Letter == '~')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("~");
                        }
                        else if (this.board[i, j].Letter == 'X')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("\u25cf");
                        }
                    }
                    Console.ResetColor();
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void B_addShip(Ship ship)
        {
            foreach(ShipCell point in ship.Shipcells)
            {
                this.board[point.X, point.Y] = point;
            }
        }

        public bool B_canAddShip(Ship ship)
        {
            foreach( ShipCell point in ship.Shipcells)
            {
                if (!B_belong(point) || !B_isEmpty(point)){
                    return false;
                }
            }
            return true;
        }

        public void B_hit(int x,int y)
        {
            if (this.board[x, y].Letter == 'S')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nHit!");
                Console.ResetColor();
                this.board[x, y].IsHit = true;
                this.board[x, y].Letter = 'X';
            }
            if (this.board[x, y].Letter == '~')
            {
                Console.WriteLine("\nMissed!\n");
                this.board[x, y].IsHit = true;
                this.board[x, y].Letter = 'O';
            }
        }

        public bool B_canHit(int x, int y)
        {
            if (B_belong(x,y))
            {
                if(!this.board[x, y].IsHit)
                {
                    return true;
                }
            }
            return false;
        }

        public bool B_belong(ShipCell cell)
        {
            if (cell.X < this.board.GetLength(0) && cell.X >=0 && cell.Y < this.board.GetLength(1) && cell.Y >= 0)
            {
                return true;
            }
            return false;
        }
        // overrided
        public bool B_belong(int x,int y)
        {
            if (x < this.board.GetLength(0) && x >= 0 && y < this.board.GetLength(1) && y >= 0)
            {
                return true;
            }
            return false;
        }

        public bool B_isEmpty(ShipCell point)
        {
            if (this.board[point.X, point.Y].Letter == '~')
            {
                foreach(ShipCell cell in B_checkNeighbours(point))
                {
                    if (board[cell.X, cell.Y].Letter != '~')
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public ShipCell[] B_checkNeighbours(ShipCell point)
        {
            LinkedList<ShipCell> neighbours = new LinkedList<ShipCell>();
            if(B_belong(point.X - 1, point.Y))
            {
                neighbours.AddLast(this.board[point.X - 1, point.Y]);
            }
            if (B_belong(point.X + 1, point.Y))
            {
                neighbours.AddLast(this.board[point.X + 1, point.Y]);
            }
            if (B_belong(point.X, point.Y+1))
            {
                neighbours.AddLast(this.board[point.X, point.Y+1]);
            }
            if (B_belong(point.X, point.Y-1))
            {
                neighbours.AddLast(this.board[point.X, point.Y-1]);
            }
            if (B_belong(point.X - 1, point.Y-1))
            {
                neighbours.AddLast(this.board[point.X - 1, point.Y-1]);
            }
            if (B_belong(point.X - 1, point.Y+1))
            {
                neighbours.AddLast(this.board[point.X - 1, point.Y+1]);
            }
            if (B_belong(point.X + 1, point.Y + 1))
            {
                neighbours.AddLast(this.board[point.X + 1, point.Y + 1]);
            }
            if (B_belong(point.X + 1, point.Y-1))
            {
                neighbours.AddLast(this.board[point.X + 1, point.Y-1]);
            }
            return neighbours.ToArray();
        }

        public void B_modifyIfSunk(Ship ship)
        {
            if (ship.S_isSink())
            {
                foreach(ShipCell cell in ship.Shipcells)
                {
                    foreach(ShipCell cellInNeighbour in B_checkNeighbours(cell))
                    {
                        if(this.board[cellInNeighbour.X, cellInNeighbour.Y].Letter != 'X')
                        {
                            this.board[cellInNeighbour.X, cellInNeighbour.Y].Letter = 'O';
                            this.board[cellInNeighbour.X, cellInNeighbour.Y].IsHit = true;
                        }
                    }
                }
                
            }
        }

    }
}
