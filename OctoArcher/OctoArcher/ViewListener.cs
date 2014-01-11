using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoArcher
{
    /// <summary>
    /// This is a model object
    /// </summary>
    interface ViewListener
    {
        void makeMove(Player p, float dx, float dy);
        void addPlayer(Player p);
        void removePlayer(Player p);
    }
}
