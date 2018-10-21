using PlantsVsZombies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsVsZombies.Factory
{
   public class EnemyFactory
    {

        public INPC CreateNPC(string Tipo, int turno, int x, int y, int soles)
        {
            INPC Cosa = new Girasol(turno, 1, 0, 2, 0, x, y); ;
            if (Tipo == "Girasol")
            {
                Cosa = new Girasol(turno, 1, 0, 2, 0, x, y);
            }
            else if (Tipo == "Planta")
            {
                Cosa = new Planta(turno, 1, 0, 1, 1, x, y);

            }
            else
            {
                Cosa = new Zombie(turno, 2, 1, 2, 1, x, y);

            }
            if (Cosa.Coste > soles && Cosa.Tipo!="Zombie")
                {
                    Console.WriteLine("No tienes soles suficientes para comprar esta planta");
                    Cosa = null;
                    return Cosa;
                }
                else
                {

                    return Cosa;

                }
         
           
        }
       
    }
}
