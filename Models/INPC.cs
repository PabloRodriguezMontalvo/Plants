using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsVsZombies.Models
{
    public abstract class INPC
    {
       public string Tipo { get; set; }
       public int Vida { get; set; }
       public int Movimiento { get; set; }
        public int Daño { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Frecuencia { get; set; }
        public int TurnoColocado { get; set; }
        public int Coste { get; set; }

        public bool RecibirDaño(int daño)
         {
          Vida -= daño;
            if (Vida > 0)
                return false;
            else
                return true;
            }
      
    }
}
