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
        void playerMoving(Player p);
        void startGame();
        void endGame();
    }
}
