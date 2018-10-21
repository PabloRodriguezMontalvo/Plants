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


        public int countDeaths = 0;
        public int turno = 1;
        public int _soles = 50;
        private float _refresco = 0.2f;
        public GameBoard(int dificultad, float refresco)
    {
            _refresco = refresco;
            countDeaths = dificultad;
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
        public void Disparar()
        {
            var zombiesFueraDelTablero = new List<INPC>();
            foreach (Planta item in Tablero.Where(o => o.Tipo == "Planta" && turno!=o.TurnoColocado))
            {
                if (turno != 1)
                {
                 var casillas=  item.Disparar();
                    var zombi = Tablero.Where(o => o.Tipo == "Zombie" && o.Y == item.Y && casillas.Contains(o.X)).FirstOrDefault();
               if(zombi != null)
                    {
                       var muerto= zombi.RecibirDaño(item.Daño);
                        if(muerto)
                        {
                            countDeaths--;

                            zombiesFueraDelTablero.Add(zombi);
                        }
                    }
                }
            }
            foreach (var item in zombiesFueraDelTablero)
            {
                Tablero.Remove(item);

            }
        }
        public void MoverEnemigos()
        {
            var PlantasFueraDelTablero = new List<INPC>();
            foreach (Zombie item in Tablero.Where(o => o.Tipo == "Zombie"))
            {
                if ((turno - item.TurnoColocado) % 2 == 0)
                {
                    var planta = Tablero.Find(o => o.X == item.X - 1);
                    if (planta == null)
                        item.X = item.X - item.Movimiento;
                    else
                        PlantasFueraDelTablero= AtaqueEnemigo(item, planta);
                }
                // un zombie ha llegado al final, has perdido
                if (item.X == 0)
                    alive = false;
            }
            foreach (var item in PlantasFueraDelTablero)
            {
                Tablero.Remove(item);
            }
           
        }
        public List<INPC> AtaqueEnemigo(INPC Enemy,INPC Planta)
        {
            var PlantasFueraDelTablero = new List<INPC>();
            Planta.Vida = Planta.Vida - Enemy.Daño;
            if (Planta.Vida == 0)
            {
                PlantasFueraDelTablero.Add(Planta);
            }
            return PlantasFueraDelTablero;
        }
            public int Fin()
        {
            if (countDeaths == 0)
                return 1;
            else if (alive == false)
                return 2;
            else return 0;
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
                    if (npc != null)
                    { 
                        Tablero.Add(npc);
                   

                        _soles -= npc.Coste;
                    }
                    break;
                case "P":
                case "PEASHOOTER":
                    npc = Factoria.CreateNPC("Planta", turno, x, y, _soles);
                    if(npc!=null)
                    {
                        Tablero.Add(npc);
                        _soles -= npc.Coste;

                    }

                    break;
                default:
                    Random rand = new Random(Guid.NewGuid().GetHashCode());

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