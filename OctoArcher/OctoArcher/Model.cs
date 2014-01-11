using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoArcher
{
    class Model : ViewListener
    {
        private Dictionary<int, Player> idPlayerTable;
        private List<Player> humans;
        private List<Player> computers;

        private List<ModelListener> views;

        public Dictionary<int, Player> IdPlayerTable { get { return idPlayerTable; } }

        public Model()
        {
            this.idPlayerTable = new Dictionary<int, Player>();
            this.humans = new List<Player>();
            this.computers = new List<Player>();
            this.views = new List<ModelListener>();
        }

        public void makeMove(Player p, float dx, float dy)
        {
            idPlayerTable[p.Id].dX = dx;
            idPlayerTable[p.Id].dY = dy;

            foreach (ModelListener view in views)
            {
                view.playerMoving(p);
            }
        }

        public void addPlayer(Player p)
        {
            computers.Add(p);
            idPlayerTable[p.Id] = p;

            foreach (ModelListener view in views)
            {
                view.playerMoving(p);
            }
        }

        public void addModelListener(ModelListener view, Player player)
        {
            views.Add(view);
            humans.Add(player);

            idPlayerTable[player.Id] = player;

            foreach (ModelListener v in views)
            {
                v.playerMoving(player);
            }
        }

        public void removePlayer(Player p)
        {
            if (humans.Contains(p))
            {
                humans.Remove(p);
                foreach (ModelListener view in views)
                {
                    view.playerRemoved(p);
                }
            }
        }

        public int getNextPlayerId()
        {
            int min = 0;
            while (true)
            {
                if (!idPlayerTable.ContainsKey(min))
                {
                    return min;
                }
                min++;
            }
        }

        internal void updateComputers()
        {
            Random rand = new Random();
            
            foreach (Player p in computers)
            {
                if (rand.NextDouble() > .15)
                {
                    p.updateFromServer(Environment.TickCount);
                    //p.dX = rand.Next(-1, 1);
                    //p.dY = rand.Next(-1, 1);

                    foreach (ModelListener view in views)
                    {
                        //view.playerMoving(p);
                    }
                }
            }
        }
    }
}
