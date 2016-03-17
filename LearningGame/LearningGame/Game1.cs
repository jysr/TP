﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using LearningGame;

namespace LearningGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;
        // Enemies

        Texture2D enemyTexture;

        List<Enemy> enemies;

        // The rate at which the enemies appear

        TimeSpan enemySpawnTime;

        TimeSpan previousSpawnTime;

        // A random number generator

        Random random;


        // texture to hold the laser.
        Texture2D laserTexture;
        // govern how fast our laser can fire.
        TimeSpan laserSpawnTime;
        TimeSpan previousLaserSpawnTime;
        List<Laser> laserBeams;
        // Image used to display the static background

        Texture2D mainBackground;
        Texture2D endGame;
        Rectangle rectBackground;

        float scale = 1f;
        // Represents the player

        Player player;
        // Keyboard states used to determine key presses

        KeyboardState currentKeyboardState;

        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses

        GamePadState currentGamePadState;

        GamePadState previousGamePadState;

        //Mouse states used to track Mouse button press

        MouseState currentMouseState;

        MouseState previousMouseState;

        // A movement speed for the player

        float playerMoveSpeed;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            player = new Player();
            
            bgLayer1 = new ParallaxingBackground();

             bgLayer2 = new ParallaxingBackground();
            // Set a constant player move speed

            playerMoveSpeed = 8.0f;

            //Background
            rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width,
                
               GraphicsDevice.Viewport.Height);

            // Initialize the enemies list

            enemies = new List<Enemy>();

            // Set the time keepers to zero

            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns

            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator





            random = new Random();
            // init our laser
            laserBeams = new List<Laser>();
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            previousLaserSpawnTime = TimeSpan.Zero;


            //Enable the FreeDrag gesture.

            TouchPanel.EnabledGestures = GestureType.FreeDrag;


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

            enemyTexture = Content.Load<Texture2D>("Graphics/mineAnimation");
            // load th texture to serve as the laser
            laserTexture = Content.Load<Texture2D>("Graphics\\laser");


            // Load the player resources

            Animation playerAnimation = new Animation();

            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);



            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,

            GraphicsDevice.Viewport.TitleSafeArea.Y

            + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player.Initialize(playerAnimation, playerPosition);
            // Load the parallaxing background

            bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);

            bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);
            mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");
            endGame = Content.Load<Texture2D>("Graphics/endMenu");
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
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses

            previousGamePadState = currentGamePadState;

            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it

            currentKeyboardState = Keyboard.GetState();

            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            //Update the player

            UpdatePlayer(gameTime);
            //UpdateEnemies
            UpdateEnemies(gameTime);
            // Update the parallaxing background
            UpdateCollision();

            // update laserbeams
            for (var i = 0; i < laserBeams.Count; i++)
            {
                laserBeams[i].Update(gameTime);
                // Remove the beam when its deactivated or is at the end of the screen.
                if (!laserBeams[i].Active || laserBeams[i].Position.X > GraphicsDevice.Viewport.Width)
                {
                    laserBeams.Remove(laserBeams[i]);
                }
            }


            if (currentKeyboardState.IsKeyDown(Keys.Space) || currentGamePadState.Buttons.X == ButtonState.Pressed)
            {
                FireLaser(gameTime);
            }

            bgLayer1.Update(gameTime);

            bgLayer2.Update(gameTime);
          


            base.Update(gameTime);
        }
        private void UpdatePlayer(GameTime gameTime)

        {
            player.Update(gameTime);
            previousMouseState = currentMouseState;

            currentMouseState = Mouse.GetState();


            // Windows 8 Touch Gestures for MonoGame

            while (TouchPanel.IsGestureAvailable)

            {

                GestureSample gesture = TouchPanel.ReadGesture();



                if (gesture.GestureType == GestureType.FreeDrag)

                {

                    player.Position += gesture.Delta;



                }

            }
            // Get Mouse State then Capture the Button type and Respond Button Press

            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed)

            {

                Vector2 posDelta = mousePosition - player.Position;

                posDelta.Normalize();

                posDelta = posDelta * playerMoveSpeed;

                player.Position = player.Position + posDelta;

            }


            // Get Thumbstick Controls

            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;

            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

          

            // Use the Keyboard / Dpad

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)

            {

                player.Position.X -= playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)

            {

                player.Position.X += playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)

            {

                player.Position.Y -= playerMoveSpeed;

            }



            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)

            {

                player.Position.Y += playerMoveSpeed;

            }

            // Make sure that the player does not go out of bounds

            player.Position.X = MathHelper.Clamp(player.Position.X, player.Width * player.PlayerAnimation.scale / 2, GraphicsDevice.Viewport.Width - player.Width * player.PlayerAnimation.scale / 2);

            player.Position.Y = MathHelper.Clamp(player.Position.Y, player.Height * player.PlayerAnimation.scale / 2, GraphicsDevice.Viewport.Height - player.Height * player.PlayerAnimation.scale / 2);


        }

        private void AddEnemy()

        {

            // Create the animation object

            Animation enemyAnimation = new Animation();

            // Initialize the animation with the correct animation information

            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy

            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,

            random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy

            Enemy enemy = new Enemy();

            // Initialize the enemy

            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list

            enemies.Add(enemy);

        }
        private void UpdateEnemies(GameTime gameTime)

        {

            // Spawn a new enemy enemy every 1.5 seconds

            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)

{

                previousSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy

                AddEnemy();

            }

            // Update the Enemies
         
            for (int i = enemies.Count - 1; i >= 0; i--)

            {

                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)

                {

                    enemies.RemoveAt(i);

                }

            }

        }
        protected void FireLaser(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
            {
                previousLaserSpawnTime = gameTime.TotalGameTime;
                // Add the laer to our list.
                AddLaser();
            }
        }

        protected void AddLaser()
        {
            Animation laserAnimation = new Animation();
            // initlize the laser animation
            laserAnimation.Initialize(laserTexture,
                player.Position,
                46,
                16,
                1,
                30,
                Color.White,
                1f,
                true);

            Laser laser = new Laser();
            // Get the starting postion of the laser.

            var laserPostion = player.Position;
            // Adjust the position slightly to match the muzzle of the cannon.
            laserPostion.Y += 37;
            laserPostion.X += 70;

            // init the laser
            laser.Initialize(laserAnimation, laserPostion);
            laserBeams.Add(laser);
            /* todo: add code to create a laser. */
            // laserSoundInstance.Play();
        }


        private void UpdateCollision()

        {

            // Use the Rectangle’s built-in intersect function to

            // determine if two objects are overlapping

            Rectangle rectangle1;

            Rectangle rectangle2;
            Rectangle laserRectangle;



            // Only create the rectangle once for the player

            rectangle1 = new Rectangle((int)player.Position.X,

            (int)player.Position.Y,

            player.Width,

            player.Height);

            // Do the collision between the player and the enemies

            for (int i = 0; i < enemies.Count; i++)

            {

                rectangle2 = new Rectangle((int)enemies[i].Position.X,

                (int)enemies[i].Position.Y,

                enemies[i].Width,

                enemies[i].Height);

                // Determine if the two objects collided with each

                // other

                if(rectangle1.Intersects(rectangle2))
            
{

                    // Subtract the health from the player based on

                    // the enemy damage

                    player.Health -= enemies[i].Damage;

                    // Since the enemy collided with the player

                    // destroy it

                    enemies[i].Health = 0;

                    // If the player health is less than zero we died

                    if (player.Health <= 0)

                        player.Active = false;

                }

            }

            // detect collisions between the player and all enemies.
            enemies.ForEach(e =>
            {
                //create a retangle for the enemy
                rectangle2 = new Rectangle(
                    (int)e.Position.X,
                    (int)e.Position.Y,
                    e.Width,
                    e.Height);

                // now see if this enemy collide with any laser shots
                laserBeams.ForEach(lb =>
                {
                    // create a rectangle for this laserbeam
                    laserRectangle = new Rectangle(
                    (int)lb.Position.X,
                    (int)lb.Position.Y,
                    lb.Width,
                    lb.Height);

                    // test the bounds of the laer and enemy
                    if (laserRectangle.Intersects(rectangle2))
                    {
                  

                       

                        // kill off the enemy
                        e.Health = 0;

                       

                        // kill off the laserbeam
                        lb.Active = false;

                       
                    }
                });
            });

        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing

            spriteBatch.Begin();





            //Draw the Main Background Texture

            spriteBatch.Draw(mainBackground, rectBackground, Color.White);



            // Draw the moving background

            bgLayer1.Draw(spriteBatch);

            bgLayer2.Draw(spriteBatch);
            rectBackground = GraphicsDevice.Viewport.TitleSafeArea;
            // Draw the Enemies

            // Draw the lasers.
            foreach (var l in laserBeams)
            {
                l.Draw(spriteBatch);
            }

            for (int i = 0; i < enemies.Count; i++)

            {

                enemies[i].Draw(spriteBatch);

            }
            // Draw the Player
           if (player.Active)
            player.Draw(spriteBatch);
            // Stop drawing

            if (!player.Active)
                spriteBatch.Draw(endGame, rectBackground, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
