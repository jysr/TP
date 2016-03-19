using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Checkers
{
    class Background
    {
        private Texture2D texture;
        private Vector2 position;
        private int bgWidth;
        private int bgHeight;
        Rectangle rectBack;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int BgWidth
        {
            get
            {
                return bgWidth;
            }

            set
            {
                bgWidth = value;
            }
        }

        public int BgHeight
        {
            get
            {
                return bgHeight;
            }

            set
            {
                bgHeight = value;
            }
        }

        public Background()
        {

        }
        public void Initialze(ContentManager content, String texturePath, int screenWidth, int screenHeight, Vector2 position, Rectangle rect)
        {
            this.bgHeight = screenHeight;
            this.bgWidth = screenWidth;
            this.texture = content.Load<Texture2D>(texturePath);
            this.position = position;
            this.rectBack = rect;

        }
        public void Update ()
        {

        }
        public void Draw (SpriteBatch sprite)
        {
            sprite.Draw(texture, rectBack, Color.White);
        }
    }
}
