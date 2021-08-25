 using System;
using System.Collections.Generic;
using System.Text;

namespace NavalBattle
{
    class ShipCell
    {
        private int x;
        private int y;
        private char letter;
        private bool isHit;

        public int X { get => x; set=> x= value; }
        public int Y { get => y; set => y = value; }
        public char Letter { get => letter; set => letter = value; }
        public bool IsHit { get => isHit; set => isHit = value; }

        public ShipCell(int x ,int y)
        {
            this.x = x;
            this.y = y;
            this.letter = '~';
            this.isHit = false;
        }
    }
}
