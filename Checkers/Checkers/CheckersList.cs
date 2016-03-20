using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    class CheckersList
    {
        List<Checker> whitecheckers;
        List<Checker> blackcheckers;
        public void Initialize(ContentManager Content)
        {
            whitecheckers = new List<Checker>();
            blackcheckers = new List<Checker>();
            int w1 = 0, w2 = 0; int h1 = 0, h2 = 0;
            for (int i = 0; i < 12; i++)
            {
                Checker checkerw = new Checker();
                Checker checkerb = new Checker();
                if (i < 4)
                {
                    w2 = 60 + i * 120;
                    w1 = 0 + i * 120;
                    h1 = 420;
                    h2 = 0;
                }
                if ((4 <= i) && (i < 8))
                {
                    w2 = -480 + i * 120;
                    w1 = -420 + i * 120;
                    h1 = 360;
                    h2 = 60;

                }
                if ((i < 12) && (8 <= i))
                {
                    w2 = -900 + i * 120;
                    w1 = -960 + i * 120;
                    h1 = 300;
                    h2 = 120;
                }
                checkerw.Initialize(Content, "Graphics\\white", new Vector2(w1, h1), 1);
                whitecheckers.Add(checkerw);
                checkerb.Initialize(Content, "Graphics\\black", new Vector2(w2, h2), 2);
                blackcheckers.Add(checkerb);
            }
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch sprite)
        {
            for (int i = 0; i < whitecheckers.Count; i++)
            {
                whitecheckers[i].Draw(sprite, Color.White);

            }
            for (int i = 0; i < blackcheckers.Count; i++)
            {
                blackcheckers[i].Draw(sprite, Color.White);
            }
        }
    }
}
