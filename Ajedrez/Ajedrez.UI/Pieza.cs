using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.UI
{
    public class Pieza
    {
        public Pieza(string tipo, string equipo)
        {
            this.tipo = tipo;
            this.equipo = equipo;
        }
        public string tipo { get; set; }
        public string equipo { get; set; }
    }
}
