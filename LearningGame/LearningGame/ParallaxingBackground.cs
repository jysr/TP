﻿// ParallaxingBackground.cs

using System;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;

namespace LearningGame
{
    class ParallaxingBackground
    {
        // The image representing the parallaxing background

        Texture2D texture;

        // An array of positions of the parallaxing background

        Vector2[] positions;

        // The speed which the background is moving

        int speed;
        int bgHeight;
        int bgWidth;

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight, int speed)

        {
            

            bgHeight = screenHeight;

            bgWidth = screenWidth;



            // Load the background texture we will be using

            texture = content.Load<Texture2D>(texturePath);

            // Set the speed of the background

            this.speed = speed;

            // If we divide the screen with the texture width then we can determine the number of tiles need.

            // We add 1 to it so that we won’t have a gap in the tiling

            positions = new Vector2[screenWidth / texture.Width + 1];

            // Set the initial positions of the parallaxing background

            for (int i = 0; i < positions.Length; i++)

            {

                // We need the tiles to be side by side to create a tiling effect

                positions[i] = new Vector2(i * texture.Width, 0);

            }

        }




        public void Update(GameTime gametime)

        {

            // Update the positions of the background
            // Update the positions of the background
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed;

                // If the speed has the background moving to the left.
                //                if (_speed <= 0)
                //                {
                //                    // Check if the texture is out of view and then put that texture at the end of the screen.
                //                    if (_positions[i].X <= -_texture.Width)
                //                    {
                //                        _positions[i].X = _texture.Width * (_positions.Length - 1);
                //                    }
                //                }
                //                else
                //                {
                //                    // Check if the texture is out of view then position it to the start of the screen
                //                    if (_positions[i].X >= _texture.Width * (_positions.Length - 1))
                //                    {
                //                        _positions[i].X = -_texture.Width;
                //                    }
                //                }
            }

            for (int i = 0; i < positions.Length; i++)
            {
                if (speed <= 0)
                {
                    // Check if the texture is out of view and then put that texture at the end of the screen.
                    if (positions[i].X <= -texture.Width)
                    {
                        WrapTextureToLeft(i);
                    }
                }
                else
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1))
                    {
                        WrapTextureToRight(i);
                    }
                }
            }
        }




        private void WrapTextureToLeft(int index)
        {
            // If the textures are scrolling to the left, when the tile wraps, it should be put at the
            // one pixel to the right of the tile before it.
            int prevTexture = index - 1;
            if (prevTexture < 0)
                prevTexture = positions.Length - 1;

            positions[index].X = positions[prevTexture].X + texture.Width;
        }

        private void WrapTextureToRight(int index)
        {
            // If the textures are scrolling to the right, when the tile wraps, it should be placed to the left
            // of the tile that comes after it.
            int nextTexture = index + 1;
            if (nextTexture == positions.Length)
                nextTexture = 0;

            positions[index].X = positions[nextTexture].X - texture.Width;
        }


        public void Draw(SpriteBatch spriteBatch)

        {

            for (int i = 0; i < positions.Length; i++)

            {

                Rectangle rectBg = new Rectangle((int)positions[i].X, (int)positions[i].Y, bgWidth, bgHeight);

                spriteBatch.Draw(texture, rectBg, Color.White);
             

                
            }

        }




    }
}
