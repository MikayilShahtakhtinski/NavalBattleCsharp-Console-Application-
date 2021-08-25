using System;
using System.Collections.Generic;
using System.Text;

namespace NavalBattle
{
    class Ship
    {

        private int size;
        private ShipCell[] shipcells;

        public int Size { get => size; set => size = value; }
        public ShipCell[] Shipcells { get => shipcells; set => shipcells= value; }

        public Ship(ShipCell startpoint,ShipCell direction, int size)
        {
            this.shipcells = new ShipCell[size];
            S_createCoordinates( startpoint,  direction,  size);
        }

        public void S_createCoordinates(ShipCell startpoint, ShipCell direction, int size)
        {
            for (int i = 0; i < size; i++)
            {
                this.shipcells[i] = new ShipCell(startpoint.X + direction.X * i, startpoint.Y + direction.Y * i);
                this.shipcells[i].Letter = 'S';
            }
        }

       public bool S_isSink()
        {
            foreach(ShipCell point in this.shipcells)
            {
                if (!point.IsHit)
                {
                    return false;
                }
            }
            return true;
        } 

        
    }
}
