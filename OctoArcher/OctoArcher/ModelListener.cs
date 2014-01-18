using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoArcher
{
    /// <summary>
    /// This is a VIEW object
    /// </summary>
    interface ModelListener
    {
        void playerCreated(Player p);
        void playerMoving(Player p);
        void playerRemoved(Player p);
        void startGame();
        void endGame();
    }
}
