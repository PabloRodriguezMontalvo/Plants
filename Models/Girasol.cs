using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsVsZombies.Models
{
    class Girasol : INPC
    {
        private int _vida;
        private int _movimiento;
        private int _daño;
        private int _x;
        private int _y;
        private int _frecuencia;
        private int _turnoColocado;

        private readonly int _coste = 20;

        public int Vida { get => _vida; set => _vida = value; }
        public int Movimiento { get => _movimiento; set => _movimiento = value; }
        public int Daño { get => _daño; set => _daño = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Coste { get => _coste; set => throw new NotImplementedException(); }
        public int Frecuencia { get => _frecuencia; set => _frecuencia = value; }
        public int TurnoColocado { get => _turnoColocado; set => _turnoColocado = value; }
        public string Tipo { get => "Girasol"; set => throw new NotImplementedException(); }

        public Girasol(int turno, int vida, int movimiento, int frecuencia, int daño, int x, int y)
        {
            _vida = vida;
            _movimiento = movimiento;
            _daño = daño;
            _x = x;
            _y = y;
            _frecuencia = frecuencia;
            _turnoColocado = turno;

        }

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
