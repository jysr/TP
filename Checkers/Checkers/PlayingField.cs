using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Checkers
{
    class PlayingField
    {
         Cell[] Field;
        
        public void Initialize(ContentManager Content)
        {
             Field = new Cell[] {
            new Cell(Content, "a1", 1, new Vector2(0, 420)),
            new Cell(Content, "a3", 1, new Vector2(120, 420)),
            new Cell(Content, "a5", 0, new Vector2(240, 420)),
            new Cell(Content, "a7", 2, new Vector2(360, 420)),
            new Cell(Content, "b2", 1, new Vector2(60, 360)),
            new Cell(Content, "b4", 0, new Vector2(180, 360)),
            new Cell(Content, "b6", 2, new Vector2(300, 360)),
            new Cell(Content, "b8", 2, new Vector2(420, 360)),
            new Cell(Content, "c1", 1, new Vector2(0, 300)),
            new Cell(Content, "c3", 1, new Vector2(120, 300)),
            new Cell(Content, "c5", 0, new Vector2(240, 300)),
            new Cell(Content, "c7", 2, new Vector2(360, 300)),
            new Cell(Content, "d2", 1, new Vector2(60, 240)),
            new Cell(Content, "d4", 0, new Vector2(180, 240)),
            new Cell(Content, "d6", 2, new Vector2(300, 240)),
            new Cell(Content, "d8", 2, new Vector2(420, 240)),
            new Cell(Content, "e1", 1, new Vector2(0, 180)),
            new Cell(Content, "e3", 1, new Vector2(120, 180)),
            new Cell(Content, "e5", 0, new Vector2(240, 180)),
            new Cell(Content, "e7", 2, new Vector2(360, 180)),
            new Cell(Content, "f2", 1, new Vector2(60, 120)),
            new Cell(Content, "f4", 0, new Vector2(180, 120)),
            new Cell(Content, "f6", 2, new Vector2(300, 120)),
            new Cell(Content, "f8", 2, new Vector2(420, 120)),
            new Cell(Content, "g1", 1, new Vector2(0, 60)),
            new Cell(Content, "g3", 1, new Vector2(120, 60)),
            new Cell(Content, "g5", 0, new Vector2(240, 60)),
            new Cell(Content, "g7", 2, new Vector2(360, 60)),
            new Cell(Content, "h2", 1, new Vector2(60, 0)),
            new Cell(Content, "h4", 0, new Vector2(180, 0)),
            new Cell(Content, "h6", 2, new Vector2(300, 0)),
            new Cell(Content, "h8", 2, new Vector2(420, 0)),

            };





        }
        public void Update() { }
        public void Draw (SpriteBatch sprite)
        {
            foreach (Cell cell in Field)
            {
                cell.Draw(sprite);
            } 
        }
    }


}

