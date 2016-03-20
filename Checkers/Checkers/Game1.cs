using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Checkers;
using System.Collections.Generic;
using System;

namespace Checkers
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Background board;
        List<Checker> whitecheckers;
        List<Checker> blackcheckers;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight =480;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            board = new Background();
            whitecheckers = new List<Checker>();
            blackcheckers = new List<Checker>();
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

            board.Initialze(Content, "Graphics\\Field", GraphicsDevice.Viewport.Width,
            
               GraphicsDevice.Viewport.Height, new Vector2(0, 0), new Rectangle(0, 0, GraphicsDevice.Viewport.Width,

               GraphicsDevice.Viewport.Height));
            int w = 0; int h1 = 0, h2 = 0;
            for (int i = 0; i < 12; i++)
            {
                Checker checkerw = new Checker();
                Checker checkerb = new Checker();
                if (i < 4)
                {
                    w = 0 + i * 120;
                    h1 = 420;
                    h2 = 0;
                }
                if ((4<=i) && (i<8))
                {
                    w = -420 + i * 120;
                    h1 = 360;
                    h2 = 60;
                
                }
                if ((i < 12) && (8 <= i))
                {
                    w = -960 + i * 120;
                    h1 = 300;
                    h2 = 120;
                }
                checkerw.Initialize(Content, "Graphics\\white", new Vector2(w, h1), Color.White);
                whitecheckers.Add(checkerw);
                checkerb.Initialize(Content, "Graphics\\black", new Vector2(w, h2), Color.Black);
                blackcheckers.Add(checkerb);
            }
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            board.Draw(spriteBatch);
            for (int i = 0; i < whitecheckers.Count; i++)
            {
                whitecheckers[i].Draw(spriteBatch);
                
            }
            for (int i = 0; i < blackcheckers.Count; i++)
            {
                blackcheckers[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
