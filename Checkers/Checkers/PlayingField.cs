using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Checkers
{
    class PlayingField
    {
        enum Color { white, black, none}
        Color color;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int ocupied;

        public int Ocupied
        {
            get { return ocupied; }
            set { ocupied = value; }
        }

        

    }
}
