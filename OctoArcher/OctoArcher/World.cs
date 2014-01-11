using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoArcher
{
    class World : ModelListener
    {

        private Dictionary<int, Player> idPlayerTable;

        public World()
        {
            this.idPlayerTable = new Dictionary<int, Player>();

        }


        public void playerMoving(Player p)
        {

            //ensure that this player exists
            //if (!this.idPlayerTable.ContainsKey(p.Id))
            //{
            //    Player newPlayer = new Player(p.Id);
            //    this.idPlayerTable.Add(p.Id, p);
            //}

            ////sync the player
            //this.idPlayerTable[p.Id].X = x;
            //this.idPlayerTable[p.Id].Y = y;
            //this.idPlayerTable[p.Id].dX = dx;
            //this.idPlayerTable[p.Id].dY = dy;

           // this.idPlayerTable[p.Id] = p;
            this.idPlayerTable.Add(p.Id, p);
        }

        public void playerRemoved(Player p)
        {
            if (this.idPlayerTable.ContainsKey(p.Id))
            {
                this.idPlayerTable.Remove(p.Id);
            }
        }

        public void startGame()
        {
            //Start game
        }

        public void endGame()
        {
            //End game
        }

        /// <summary>
        /// Update all players position
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            foreach (Player p in this.idPlayerTable.Values)
            {
                p.update(gameTime);
            }
        }

        /// <summary>
        /// Draw all players
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Player p in this.idPlayerTable.Values)
            {
                p.draw(spriteBatch);
            }
            spriteBatch.End();


        }

    }
}
