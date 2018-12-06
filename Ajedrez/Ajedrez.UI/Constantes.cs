using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.UI
{
    public static class Constantes
    {
        public static class TiposPieza
        {
            public const string P = "Peon";
            public const string T = "Torre";
            public const string A = "Alfil";
            public const string C = "Caballo";
            public const string K = "King";
            public const string Q = "Queen";

        }

        public static Dictionary<int, string> colorCasilleros = new Dictionary<int, string>()
        {
            {0, "Blanco" },
            {1, "Negro" }
        };


        public static class Equipos
        {
            public const string BLANCO = "Blanco";
            public const string NEGRO = "Negro";

        }

    }
}
