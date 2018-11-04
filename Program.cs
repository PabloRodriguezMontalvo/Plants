using PlantsVsZombies.Factory;
using PlantsVsZombies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlantsVsZombies
{
    class Program
    {


        static void  Main(string[] args)
        {
            // Gameboard contiene las reglas del juego
            GameBoard Juego = new GameBoard(3, .1f);

            Console.WriteLine("Qué nivel quieres jugar? (1. Facil, 2. Normal, 3.Dificil)");
            var respuesta= Console.ReadLine();
            //Aquí metemos la frecuencia de spawn y los enemigos a eliminar para ganar la partida
            if (respuesta == "1")
            {
              Juego= new GameBoard(3, .1f);
            }
            else if (respuesta == "2")
            {
                Juego = new GameBoard(5, .2f);

            }
            else if (respuesta == "3")
            {
                Juego = new GameBoard(10, .3f);

            }
            else
            {
                Console.WriteLine("Qué nivel quieres jugar? -1. Facil, 2. Normal, 3.Dificil-");
            }
            while (Juego.Fin()!=0)
            {
                var AccionCorrecta = true;
                var accion = UserComand().ToUpper();
                //Esto se podría mejorar
                if (accion.StartsWith("ADD"))
                {


                    string LaPlanta = accion.Split('<', '>')[1];
                    string LaX = accion.Split('<', '>')[3];
                    string LaY = accion.Split('<', '>')[5];
                    if (int.TryParse(LaX, out int X) && int.TryParse(LaY, out int Y))
                    {
                        var posicionOcupada = Juego.PosicionOcupada(X, Y);
                        if (posicionOcupada == false)
                        {
                            Juego.GenerarNPC(LaPlanta, int.Parse(LaX), int.Parse(LaY));


                        }
                        else
                        {
                            AccionCorrecta = false;
                            Console.WriteLine("Posición ocupada");
                        }
                    }
                    else
                    {
                        AccionCorrecta = false;

                        Console.WriteLine("Las coordenadas son incorrectas");

                    }
                }
                else if (accion.StartsWith("LIST"))
                {

                    foreach (var item in Juego.Tablero)
                    {
                        Console.Write(item.Tipo + "- (" + item.X + "," + item.Y + ")");

                    }

                }
                else if (string.IsNullOrWhiteSpace(accion))
                { }

                // No he puesto las demás acciones pero son triviales de hacer
                else
                {
                    AccionCorrecta = false;
                }
                if (AccionCorrecta == true)
                { 
                Update(Juego);
                Draw(Juego);
                    var HaTerminado = Juego.Fin();
                    if (HaTerminado!=0)
                    {
                        GameOver(HaTerminado);
                    }
                    else
                    Juego.SiguienteTurno();
                }
            }
        }
        /// <summary>
        /// Esto es la función que actualiza el juego entre turnos
        /// </summary>
        /// <param name="Juego"></param>
        public static void Update(GameBoard Juego)
        {
          var soles=  Juego.AcumularSol();
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            Juego.GenerarNPC("Zombie", 7, rand.Next(1, 4));

            Juego.Disparar();
            Juego.MoverEnemigos();

        }
        /// <summary>
        /// Esto es para pintar el tablero
        /// </summary>
        /// <param name="Juego"></param>
        public static void Draw(GameBoard Juego)
        {
            var tablero = Juego.Tablero;
            Console.WriteLine("Turno: " + Juego.turno);
            Console.WriteLine("Soles: " + Juego._soles);
            Console.WriteLine("Enemigos restantes: " + Juego.countDeaths);


            for (int y = 0; y < 4; y++)
            {
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ");

                for (int x = 0; x < 8; x++)
                {
                   var npc= tablero.Find(o => o.X == x && o.Y == y);
                    if (npc != null)
                    { 
                        if(npc.Tipo.ToUpper() == "ZOMBIE")
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                        }
                    if (npc.Tipo.ToUpper() == "GIRASOL")
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    if (npc.Tipo.ToUpper() == "PLANTA")
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    Console.Write("|" + npc.Tipo + "[" + npc.Vida + "]|");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                      
                    Console.Write("|          |");

                }
                Console.WriteLine("\n");


            }
            Console.WriteLine("\n");
        }

        public static string UserComand()
        {
            Console.WriteLine("Add <plant><x><y>\nList\nReset\nHelp\nExit");
            return Console.ReadLine();

        }
        public static string GameOver(int WinOrLose)
        {
            if(WinOrLose==1)
            Console.WriteLine("Has ganado");
            else
                Console.WriteLine("Has perdido");

            return Console.ReadLine();

        }

    }
}

