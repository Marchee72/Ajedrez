using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Tablero tab = new Tablero();
            tab.GenerarTablero();
            tab.MostrarTablero();
        }
    }
}
