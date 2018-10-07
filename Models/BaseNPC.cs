using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsVsZombies.Models
{
    public interface INPC
    {
         string Tipo { get; set; }
         int Vida { get; set; }
         int Movimiento { get; set; }
         int Daño { get; set; }
         int X { get; set; }
         int Y { get; set; }
         int Frecuencia{ get; set; }
         int TurnoColocado { get; set; }
         int Coste { get; set; }

         bool RecibirDaño(int daño);
    }
}
