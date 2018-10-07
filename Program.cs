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

            GameBoard Juego = new GameBoard(3, .1f);

            Console.WriteLine("Qué nivel quieres jugar? (1. Facil, 2. Normal, 3.Dificil)");
            var respuesta= Console.ReadLine();
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
            while (Juego.Fin())
            {
                var AccionCorrecta = true;
                var accion = UserComand().ToUpper();
                if (accion.StartsWith("ADD")) 
                {
                  

                    string LaPlanta = accion.Split('<', '>')[1];
                    string LaX = accion.Split('<', '>')[3];
                    string LaY= accion.Split('<', '>')[5];
                    var posicionOcupada = Juego.GetTablero().Any(o => o.X == int.Parse(LaX) && o.Y == int.Parse(LaY));
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
                if(AccionCorrecta == true)
                { 
                Update(Juego);
                Draw(Juego);
             
                Juego.SiguienteTurno();
                }
            }
        }

        public static void Update(GameBoard Juego)
        {
          var soles=  Juego.AcumularSol();
            Console.Write("Soles: " + soles +"\n");
            var rand = new Random();

            Juego.GenerarNPC("Zombie", 0, rand.Next(1, 4));

            Juego.Disparar();
        

        }

        public static void Draw(GameBoard Juego)
        {
        }

        public static string UserComand()
        {
            Console.WriteLine("Add <plant><x><y>\nList\nReset\nHelp\nExit");
            return Console.ReadLine();

        }
    
}
}

