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



        public void makeMove(Player p, float dx, float dy)
        {
            idPlayerTable[p.Id].dX = dx;
            idPlayerTable[p.Id].dY = dy;
        }

        public void addPlayer(Player p)
        {
            computers.Add(p);
        }

        public void addModelListener(ModelListener view, Player player)
        {
            views.Add(view);
            humans.Add(player);

            idPlayerTable[player.Id] = player;
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
    }
}
