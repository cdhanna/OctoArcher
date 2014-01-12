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

        private Model model;

        public World()
        {
            model = new Model();
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
            //if (model.IdPlayerTable.ContainsKeyp.Id)
            this.model.IdPlayerTable[p.Id] = p;
            //this.model.IdPlayerTable.Add(p.Id, p);
        }

        public void playerRemoved(Player p)
        {
            if (this.model.IdPlayerTable.ContainsKey(p.Id))
            {
                this.model.IdPlayerTable.Remove(p.Id);
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

        public void playerCreated(Player p)
        {
            Console.WriteLine("PLAYER ADDED: {0}", p.Id);
            plr = p;
        }

        private Player plr = null;

        public Player waitForPlayer()
        {
            while (plr == null)
            {
            }
            return plr;
        }

        /// <summary>
        /// Update all players position
        /// </summary>
        /// <param name="gameTime"></param>
        public void update(GameTime gameTime)
        {
            //foreach (Player p in this.model.IdPlayerTable.Values)
            for (int i = 0; i < this.model.IdPlayerTable.Values.Count; i++ )
            {
                Player p = this.model.IdPlayerTable.Values.ToList()[i];
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
            for (int i = 0; i < this.model.IdPlayerTable.Values.Count; i++)
            {
                Player p = this.model.IdPlayerTable.Values.ToList()[i];
                p.draw(spriteBatch);
            }
            spriteBatch.End();


        }



    }
}
