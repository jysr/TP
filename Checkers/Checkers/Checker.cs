using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Checkers
{
    class Checker
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rect;
        private bool alive;
        private Color _color;

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

        public bool Alive
        {
            get
            {
                return alive;
            }

            set
            {
                alive = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return rect;
            }

            set
            {
                rect = value;
            }
        }

        public void Initialize(ContentManager content ,string texturePath, Vector2 position, Color color)
        {
            this.position = position;
            this.rect = new Rectangle((int)position.X, (int)position.Y, 60, 60);
            this.texture = content.Load<Texture2D>(texturePath);
            this._color = color;

        }
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }

        public Color _Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public void Update (Vector2 position)
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

        }
        public void Draw (SpriteBatch sprite)
        {
            sprite.Draw(texture, rect, _color);
        }
    }
}
