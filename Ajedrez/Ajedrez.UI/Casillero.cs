using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.UI
{
    public class Casillero
    {

        public Casillero(char x, int y, string color)
        {
            this.coordX = x;
            this.coordY = y;
            this.ocupado = false;
            this.colorCasillero = color;
        }
        public char coordX { get; }
        public int coordY { get; }
        public bool ocupado { get; set; }
        public Pieza pieza { get; set; }
        public string colorCasillero { get ; set; }
    }
}
