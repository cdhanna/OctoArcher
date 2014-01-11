using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OctoArcher
{
    class Player
    {

        private static Texture2D image = null;
        public static void initContent(ContentManager content)
        {
            image = content.Load<Texture2D>("whiteBox");
        }

        public int Id   { get; set; }
        public float X  { get; set; }
        public float Y  { get; set; }
        public float dX { get; set; }
        public float dY { get; set; }

        private double lastMilliUpdate = 0;


        public Player(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Move the player by its velocity. The movement is timed by the gametime.
        /// </summary>
        /// <param name="time"></param>
        public void update(GameTime time)
        {
            double currentMilliUpdate = time.TotalGameTime.TotalMilliseconds;
            double deltaMilliTime = (currentMilliUpdate - lastMilliUpdate) / 100;
            Console.WriteLine(deltaMilliTime);

            update((float)deltaMilliTime);

            this.lastMilliUpdate = time.TotalGameTime.TotalMilliseconds;
        }


        int lastElapsedUpdate = 0;
        public void updateFromServer(int elapsed)
        {
            int msDelta = elapsed - lastElapsedUpdate;
            update(msDelta / 100f);
            lastElapsedUpdate = elapsed;
        }

        public void update(float msDelta)
        {
            X += dX * msDelta;
            Y += dY * msDelta;
        }

        /// <summary>
        /// Draw the player
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            if (image != null)
            {
                spriteBatch.Draw(image, new Vector2(X, Y), null, Color.Black, 0, Vector2.Zero, .2f * Vector2.One, SpriteEffects.None, 0);
            }
        }

    }
}
