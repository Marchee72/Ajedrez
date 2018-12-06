using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez.UI
{
    public class Tablero
    {
        int cantPeones = 8;
        int cantTorres = 2;
        int cantAlfiles = 2;
        int cantCaballos = 2;
        int cantRey = 1;
        int cantReinas = 1;


        private List<Casillero> casilleros;
        string casillerosX = "abcdefgh";
        public void GenerarTablero()
        {
            casilleros = new List<Casillero>();
            for (int x = 0; x < casillerosX.Length; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    casilleros.Add(new Casillero(casillerosX[x], y, Constantes.colorCasilleros[(y+x)%2]));
                }
            }
            this.LlenarTablero();
        }

        private void LlenarTablero()
        {
            var random = new Random(Environment.TickCount);

            #region Equipo blanco
            #region Crear peones
            UbicarPiezas(cantPeones, Constantes.TiposPieza.P, random, Constantes.Equipos.BLANCO);
            #endregion

            #region Crear torres
            UbicarPiezas(cantTorres, Constantes.TiposPieza.T ,random, Constantes.Equipos.BLANCO);
            #endregion

            #region Crear caballos
            UbicarPiezas(cantCaballos, Constantes.TiposPieza.C, random, Constantes.Equipos.BLANCO);
            #endregion

            #region Crear alfiles
            UbicarPiezas(cantAlfiles, Constantes.TiposPieza.A, random, Constantes.Equipos.BLANCO);
            #endregion

            #region Crear reina
            UbicarPiezas(cantReinas, Constantes.TiposPieza.Q, random, Constantes.Equipos.BLANCO);
            #endregion

            #region Crear rey
            UbicarPiezas(cantRey, Constantes.TiposPieza.K, random, Constantes.Equipos.BLANCO);
            #endregion
            #endregion

            #region Equipo Negro
            #region Crear peones
            UbicarPiezas(cantPeones, Constantes.TiposPieza.P, random, Constantes.Equipos.NEGRO);
            #endregion

            #region Crear torres
            UbicarPiezas(cantTorres, Constantes.TiposPieza.T, random, Constantes.Equipos.NEGRO);
            #endregion

            #region Crear caballos
            UbicarPiezas(cantCaballos, Constantes.TiposPieza.C, random, Constantes.Equipos.NEGRO);
            #endregion

            #region Crear alfiles
            UbicarPiezas(cantAlfiles, Constantes.TiposPieza.A, random, Constantes.Equipos.NEGRO);
            #endregion

            #region Crear reina
            UbicarPiezas(cantReinas, Constantes.TiposPieza.Q, random, Constantes.Equipos.NEGRO);
            #endregion

            #region Crear rey
            UbicarPiezas(cantRey, Constantes.TiposPieza.K, random, Constantes.Equipos.NEGRO);
            #endregion
            #endregion
        }

        private void UbicarPiezas(int cant, string tipo, Random random, string color)
        {
            for (int i = 0; i < cant; i++)
            {
                bool ocupado;
                bool lugarValido;
                Tuple<char, int> coord;
                do
                {
                    lugarValido = true;
                    coord = this.GenerarCoordenadas(random);
                    ocupado = casilleros.Where(c => c.coordX == coord.Item1 && c.coordY == coord.Item2).Select(x => x.ocupado).First();

                    //Valido que haya un alfil de cada equipo en colores de casilleros diferentes
                    if(tipo == Constantes.TiposPieza.A && !ocupado)
                    {
                        string colorCasilleroAlfil;
                        try
                        {
                            Casillero casillero = casilleros.Where(c => c.ocupado && c.pieza.equipo == color && c.pieza.tipo == tipo).FirstOrDefault();
                            if (casillero != null) colorCasilleroAlfil = casillero.colorCasillero;
                            else colorCasilleroAlfil = String.Empty;
                        }
                        catch (Exception)
                        {
                            colorCasilleroAlfil = String.Empty;
                        }


                        if (!String.IsNullOrEmpty(colorCasilleroAlfil))
                        {
                            string colorCasilleroAColocar = casilleros.Where(c => c.coordX == coord.Item1 && c.coordY == coord.Item2).Select(x => x.colorCasillero).First();
                            lugarValido = (colorCasilleroAlfil == colorCasilleroAColocar) ? false : true;
                        }
                        else lugarValido = true;
                    }
                } while (ocupado || !lugarValido);

                casilleros.Where(c => c.coordX == coord.Item1 && c.coordY == coord.Item2).First().pieza = new Pieza(tipo, color);
                casilleros.Where(c => c.coordX == coord.Item1 && c.coordY == coord.Item2).First().ocupado = true;
            }
        }

        private Tuple<char, int> GenerarCoordenadas(Random random)
        {

            char x = this.casillerosX[random.Next(0, 8)];
            int y = random.Next(1, 9);

            return Tuple.Create(x, y);
        }

        public void MostrarTablero()
        {
            //foreach (Casillero c in casilleros)
            //{
            //    string info = String.Format("Coordenadas: ({0},{1}), Color del casillero: {2}", c.coordX, c.coordY, c.colorCasillero);
            //    string info2 = c.ocupado ? String.Format("|| Pieza: {0}, Equipo: {1}", c.pieza.tipo, c.pieza.equipo) : "|| El casillero esta libre";

            //    Console.WriteLine(info+info2);
            //}

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Equipo NEGRO mueve para ABAJO");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Equipo BLANCO mueve para ARRIBA");

            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (char letra in casillerosX) Console.Write(" " + letra + " ");
            Console.Write("\n");

            for (int i = 1; i < 9; i++)
            {
                List<Casillero> paraMostrar = casilleros.Where(c => c.coordY == i).OrderBy(x => x.coordX).ToList();
                foreach (Casillero c in paraMostrar)
                {
                    Console.BackgroundColor = (c.colorCasillero == Constantes.colorCasilleros[0])?  ConsoleColor.White :  ConsoleColor.Black;
                           
                    if (c.pieza != null)
                    {
                        //Equipo blanco = Piezas Rojas
                        //Equipo negro = Piezas Verdes
                        Console.ForegroundColor = (c.pieza.equipo == Constantes.Equipos.BLANCO) ? ConsoleColor.Red : ConsoleColor.Green;
                        Console.Write(c.pieza.tipo[0] + "  ");
                    }
                    else Console.Write("   ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                
                Console.Write(" " + i.ToString() + "\n");
            }
            this.SeleccionarPieza();
        }

        public void SeleccionarPieza()
        {
            bool coordsValidas;
            do
            {
                Console.Write("Seleccione las coordenadas de origen: ");
                string coord = Console.ReadLine().Trim();
                coordsValidas = true;
                try
                {
                    char coordX = coord[0];
                    int coordY = int.Parse(coord[1].ToString());
                    if (coord.Length > 2 || coordY > 8 || coordY < 0 || !casillerosX.Contains(coordX))
                        throw new Exception("Las coordenadas no son validas");

                    var casillero = casilleros.Where(c => c.coordX == coordX && c.coordY == coordY).First();

                    if (!casillero.ocupado)
                        throw new Exception("No hay ninguna pieza en el casillero");

                    this.SeleccionarDestino(casillero);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    coordsValidas = false;
                }
            } while (!coordsValidas);

        }



        private void SeleccionarDestino(Casillero origen)
        {
            bool coordsValidas;
            do
            {
                Console.Write("Seleccione las coordenadas de destino: ");
                string coord = Console.ReadLine().Trim();
                coordsValidas = true;
                try
                {
                    char coordX = coord[0];
                    int coordY = int.Parse(coord[1].ToString());
                    if (coord.Length > 2 || coordY > 8 || coordY < 0 || !casillerosX.Contains(coordX))
                        throw new Exception("Las coordenadas no son validas");

                    Casillero destino = casilleros.Where(c => c.coordX == coordX && c.coordY == coordY).First();

                    switch (origen.pieza.tipo)
                    {
                        case Constantes.TiposPieza.P:
                            ValidarMovimientosPeon(origen, destino);
                            this.Mover(origen, destino);

                            break;

                        case Constantes.TiposPieza.T:
                            ValidarMovimientosTorre(origen, destino);
                            this.Mover(origen, destino);
                            break;

                        case Constantes.TiposPieza.C:
                            ValidarMovimientosCaballo(origen, destino);
                            this.Mover(origen, destino);
                            break;

                        case Constantes.TiposPieza.A:
                            ValidarMovimientosAlfil(origen, destino);
                            this.Mover(origen, destino);
                            break;

                        case Constantes.TiposPieza.K:
                            ValidarMovimientosRey(origen, destino);
                            this.Mover(origen, destino);
                            break;

                        case Constantes.TiposPieza.Q:
                            this.ValidarMovimientosReina(origen, destino);
                            this.Mover(origen, destino);
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("El movimiento no es valido");
                    coordsValidas = false;
                }
            } while (!coordsValidas);
        }

        private void ValidarMovimientosPeon(Casillero origen, Casillero destino)
        {
            if (origen.pieza.equipo == Constantes.Equipos.BLANCO)
            {
                if (!(origen.coordY == (destino.coordY + 1))) throw new Exception();
            }
            else if (!(origen.coordY == (destino.coordY - 1))) throw new Exception();

            if (!((origen.coordX + 1) == destino.coordX && origen.coordX != casillerosX[7] || (origen.coordX - 1) == destino.coordX && origen.coordX != casillerosX[0] || origen.coordX == destino.coordX))
                throw new Exception();

            if (origen.coordX != destino.coordX)
            {
                if (!(destino.ocupado && destino.pieza.equipo != origen.pieza.equipo))
                    throw new Exception();
            }
            else if (destino.ocupado)
                throw new Exception();
        }
        private void ValidarMovimientosTorre(Casillero origen, Casillero destino)
        {
            if(!((origen.coordX == destino.coordX && origen.coordY != destino.coordY) || (origen.coordX != destino.coordX && origen.coordY == destino.coordY)))
                throw new Exception();

            this.ValidarMovimientosLaterales(origen, destino);

            if (destino.ocupado)
            {
                if (destino.pieza.equipo == origen.pieza.equipo) throw new Exception();
            }
        }
        private void ValidarMovimientosCaballo(Casillero origen, Casillero destino)
        {
            if (!((((destino.coordY == (origen.coordY + 1) || destino.coordY == (origen.coordY - 1)) && (destino.coordX == (origen.coordX + 2) || destino.coordX == (origen.coordX - 2)))) ||
                ((destino.coordY == (origen.coordY + 2) || destino.coordY == (origen.coordY - 2)) && (destino.coordX == (origen.coordX + 1) || destino.coordX == (origen.coordX - 1)))))
                throw new Exception();

            if (destino.ocupado)
            {
                if (destino.pieza.equipo == origen.pieza.equipo) throw new Exception();
            }                
        }
        private void ValidarMovimientosAlfil(Casillero origen, Casillero destino)
        {
            ValidarMovimientosDiagonales(origen, destino);
            if (destino.ocupado)
            {
                if (destino.pieza.equipo == origen.pieza.equipo) throw new Exception();
            }
        }
        private void ValidarMovimientosRey(Casillero origen, Casillero destino)
        {
            int difX = Math.Abs(origen.coordX - destino.coordX);
            int difY = Math.Abs(origen.coordY - destino.coordY);
            if (!(difX <= 1 && difY <= 1))
                throw new Exception();

            if(!(difX == 0 || difY == 0))
                this.ValidarMovimientosDiagonales(origen, destino);

            this.ValidarMovimientosLaterales(origen, destino);

            if (destino.ocupado)
            {
                if (destino.pieza.equipo == origen.pieza.equipo) throw new Exception();
            }

        }
        private void ValidarMovimientosReina(Casillero origen, Casillero destino)
        {
            int difX = Math.Abs(origen.coordX - destino.coordX);
            int difY = Math.Abs(origen.coordY - destino.coordY);

            if (!(difX == 0 || difY == 0))
                this.ValidarMovimientosDiagonales(origen, destino);

            this.ValidarMovimientosLaterales(origen, destino);

            if (destino.ocupado)
            {
                if (destino.pieza.equipo == origen.pieza.equipo) throw new Exception();
            }
        }
        private void ValidarMovimientosDiagonales(Casillero origen, Casillero destino)
        {
            if (!(Math.Abs(origen.coordX - destino.coordX) == Math.Abs(origen.coordY - destino.coordY)))
                throw new Exception();

            int difX = origen.coordX - destino.coordX;
            int difY = origen.coordY - destino.coordY;
            for (int i = 1; i < Math.Abs(difX); i++)
            {
                if (difX < 0 && difY < 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX + i && c.coordY == origen.coordY + i && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX < 0 && difY > 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX + i && c.coordY == origen.coordY - i && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX > 0 && difY < 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX - i && c.coordY == origen.coordY + i && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX > 0 && difY > 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX - i && c.coordY == origen.coordY - i && c.ocupado).Any())
                        throw new Exception();
                }
            }
        }
        private void ValidarMovimientosLaterales(Casillero origen, Casillero destino)
        {
            int difX = origen.coordX - destino.coordX;
            int difY = origen.coordY - destino.coordY;

            int cant = difX > difY ? difX : difY;
            for (int i = 1; i < Math.Abs(cant); i++)
            {
                if (difX < 0 && difY == 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX + i && c.coordY == origen.coordY  && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX < 0 && difY == 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX + i && c.coordY == origen.coordY && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX == 0 && difY < 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX && c.coordY == origen.coordY + i && c.ocupado).Any())
                        throw new Exception();
                }

                if (difX == 0 && difY > 0)
                {
                    if (casilleros.Where(c => c.coordX == origen.coordX && c.coordY == origen.coordY - i && c.ocupado).Any())
                        throw new Exception();
                }
            }
        }
        private void Mover(Casillero origen, Casillero destino)
        {
            Pieza piezaMovida = casilleros.Where(c => c.coordX == origen.coordX && c.coordY == origen.coordY).Select(x => x.pieza).First();
            //Guardo la pieza en el nuevo casillero
            casilleros.Where(c => c.coordX == destino.coordX && c.coordY == destino.coordY).First().pieza = piezaMovida;
            casilleros.Where(c => c.coordX == destino.coordX && c.coordY == destino.coordY).First().ocupado = true;
            //Y la saco del casilliero anterior
            casilleros.Where(c => c.coordX == origen.coordX && c.coordY == origen.coordY).First().pieza = null;
            casilleros.Where(c => c.coordX == origen.coordX && c.coordY == origen.coordY).First().ocupado = false;

            Console.WriteLine("Buen movimiento");
            this.MostrarTablero();
        }

    }
}
