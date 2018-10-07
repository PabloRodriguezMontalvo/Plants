using PlantsVsZombies.Factory;
using PlantsVsZombies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsVsZombies
{

    public class GameBoard
    {
        EnemyFactory Factoria = new EnemyFactory();
        bool alive = true;
        public List<INPC> Tablero = new List<INPC>();


        private int countDeaths = 0;
        private int turno = 1;
        private int _soles = 50;
        private float _refresco = 0.2f;
        public GameBoard(int dificultad, float refresco)
    {
            _refresco = refresco;
    }
        public int AcumularSol()
        {
            foreach (var item in Tablero.Where(o => o.Tipo == "Girasol"))
            {
                if(turno!=1)
                { 
                if ((turno - item.TurnoColocado) % 2 == 0)
                {
                    _soles += 10;
                }
                }
            }
            return _soles;
        }
        public int Disparar()
        {
            foreach (Planta item in Tablero.Where(o => o.Tipo == "Planta"))
            {
                if (turno != 1)
                {
                 var casillas=  item.Disparar();
                    var zombies = Tablero.Where(o => o.Tipo == "Zombie" && o.Y == item.Y && casillas.Contains(o.X));
                    foreach (var zombi in zombies)
                    {
                       var muerto= zombi.RecibirDaño(item.Daño);
                        if(muerto)
                        {
                            Tablero.Remove(zombi);
                        }
                    }
                }
            }
            return _soles;
        }
        public bool Fin()
        {
            if (countDeaths == 0 || alive == false)
                return true;
            else return false;
        }
        public void SiguienteTurno()
        {
            turno++;
        }
        public List<INPC> GetTablero()
        { return Tablero; }
        public void GenerarNPC(string planta, int x, int y)
        {
            INPC npc;
            switch (planta.ToUpper())
            {
                case "S":
                case "SUNFLOWER":
                  npc= Factoria.CreateNPC("Girasol",turno, x, y, _soles);
                    Tablero.Add(npc);

                    break;
                case "P":
                case "PEASHOOTER":
                    npc = Factoria.CreateNPC("Planta", turno, x, y, _soles);
                    Tablero.Add(npc);
                  
                    break;
                default:
                    var rand = new Random();

                    var porcentaje = rand.Next(1, 100);
                    if(porcentaje<= _refresco*100 )
                    { 
                    npc = Factoria.CreateNPC("Zombie", turno,x,y, _soles);
                    Tablero.Add(npc);
                    }
                    break;
            }




        }


    }

}