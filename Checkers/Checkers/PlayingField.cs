using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Checkers
{
    class PlayingField
    {
         Cell[,] Field;
        
        public void Initialize(ContentManager Content)
        {
             Field = new Cell[,] {
            { new Cell(Content, "11", 1, new Vector2(0, 420)),
            new Cell(Content, "13", 1, new Vector2(120, 420)),
            new Cell(Content, "15", 1, new Vector2(240, 420)),
            new Cell(Content, "17", 1, new Vector2(360, 420)) },
          {  new Cell(Content, "22", 1, new Vector2(60, 360)) ,
             new Cell(Content, "24", 1, new Vector2(180, 360)),
            new Cell(Content, "26", 1, new Vector2(300, 360)),
            new Cell(Content, "28", 1, new Vector2(420, 360)) },
           { new Cell(Content, "31", 1, new Vector2(0, 300)) ,
            new Cell(Content, "33", 1, new Vector2(120, 300)) ,
            new Cell(Content, "35", 1, new Vector2(240, 300)),
            new Cell(Content, "37", 1, new Vector2(360, 300)) },
           { new Cell(Content, "42", 0, new Vector2(60, 240)),
            new Cell(Content, "44", 0, new Vector2(180, 240)),
            new Cell(Content, "46", 0, new Vector2(300, 240)),
            new Cell(Content, "48", 0, new Vector2(420, 240)) },
           { new Cell(Content, "51", 0, new Vector2(0, 180)),
            new Cell(Content, "53", 0, new Vector2(120, 180)),
            new Cell(Content, "55", 0, new Vector2(240, 180)),
            new Cell(Content, "57", 0, new Vector2(360, 180)) },
           { new Cell(Content, "62", 2, new Vector2(60, 120)),
            new Cell(Content, "64", 2, new Vector2(180, 120)),
            new Cell(Content, "66", 2, new Vector2(300, 120)),
            new Cell(Content, "68", 2, new Vector2(420, 120)) },
           { new Cell(Content, "71", 2, new Vector2(0, 60)),
            new Cell(Content, "73", 2, new Vector2(120, 60)),
            new Cell(Content, "75", 2, new Vector2(240, 60)),
            new Cell(Content, "77", 2, new Vector2(360, 60)) },
           { new Cell(Content, "82", 2, new Vector2(60, 0)),
            new Cell(Content, "84", 2, new Vector2(180, 0)),
            new Cell(Content, "86", 2, new Vector2(300, 0)),
            new Cell(Content, "88", 2, new Vector2(420, 0)) }

            };

           



        }
        public void LoadContent(ContentManager content) {
            foreach (Cell c in Field)
            {
                c.LoadContent(content);
            }
        }
        public void Update() {
            foreach (Cell c in Field)
            {
                c.Update(Field);
            }
        }
        public void Draw (SpriteBatch sprite, ContentManager cm)
        {
            foreach (Cell cell in Field)
            {
                cell.Draw(sprite, cm);
            } 
        }
    }


}

