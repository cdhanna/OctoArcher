﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace OctoArcher
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;
        ModelProxy modelProxy;
        Server server;
        KeyboardHelper keyboard;
        Player player;

        public Game1()
            : base()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Player.initContent(Content);
            keyboard = new KeyboardHelper();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            server = new Server();


            modelProxy = new ModelProxy(NetProp.SERVER_IP, NetProp.PORT);
            world = new World();

            modelProxy.View = world;


            
            //p.X = 100;
            //p.Y = 300;
            //p.dX = 2;
            //p.dY = 0;
            //modelProxy.addPlayer(p);

            player = world.waitForPlayer();

            Console.WriteLine("I GOT A PLAYER!!!: {0} ", player.Id);
            player.X = 50;
            modelProxy.putPlayer(player, 50, 50);
            
            
            modelProxy.makeMove(player, 0, 1);
            //modelProxy.makeMove(player, 0, -1);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            modelProxy.shutdown();
            server.shutdown();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            world.update(gameTime);

            float dx = 0, dy = 0;
            if (keyboard.NewKeyDown(Keys.Up))
                dy += 1;
            if (keyboard.NewKeyDown(Keys.Down))
                dy -= 1;
            if (keyboard.NewKeyDown(Keys.Right))
                dx += 1;
            if (keyboard.NewKeyDown(Keys.Left))
                dx -= 1;
            if (dx != 0 || dy != 0)
            {
                player.dX = dx;
                player.dY = dy;
                modelProxy.makeMove(player, dx, dy);
            }
            keyboard.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            world.draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
