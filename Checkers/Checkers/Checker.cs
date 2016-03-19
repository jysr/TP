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
        private Vector2[] position;
        private bool alive;


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

        public Vector2[] Position
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
    }
}
