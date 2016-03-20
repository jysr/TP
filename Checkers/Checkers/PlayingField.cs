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

       
         
            Field = new Cell[32];
            Cell cell = new Cell();
            cell.Initiazlize(Content, "a1", 1, new Vector2(0, 460));
            Field[0] = cell;

            Cell cell1 = new Cell();
            cell1.Initiazlize(Content, "a3", 1, new Vector2(120, 460));
            Field[1] = cell1;
            cell = new Cell();
            cell.Initiazlize(Content,"a5", 0, new Vector2(240, 460) );
            Field[2] = cell;
            Cell cell3 = new Cell();
            cell3.Initiazlize(Content, "a7", 2, new Vector2(360, 460));

            Field[3] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "b2", 1, new Vector2(60, 400));
            Field[4] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "b4", 0, new Vector2(180, 400));
            Field[5] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "b6", 2, new Vector2(300, 400));
            Field[6] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "b8", 2, new Vector2(420, 400));
            Field[7] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "c1", 1, new Vector2(0, 320));
            Field[8] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "c3", 1, new Vector2(120, 320));
            Field[9] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "c5", 0, new Vector2(240, 320));
            Field[10] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "c7", 2, new Vector2(360, 320));
            Field[11] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "d2", 1, new Vector2(60, 260));
            Field[12] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "d4", 0, new Vector2(180, 260));
            Field[13] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "d6", 2, new Vector2(300, 260));
            Field[14] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "d8", 2, new Vector2(420, 260));
            Field[15] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "e1", 1, new Vector2(0, 200));
            Field[16] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "e3", 1, new Vector2(120, 200));
            Field[17] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "e5", 0, new Vector2(240, 200));
            Field[18] = cell;
            cell.Initiazlize(Content, "e7", 2, new Vector2(360, 200));
            Field[19] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "f2", 1, new Vector2(60, 120));
            Field[20] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "f4", 0, new Vector2(180, 120));
            Field[21] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "f6", 2, new Vector2(300, 120));
            Field[22] = cell;
            cell.Initiazlize(Content, "f8", 2, new Vector2(420, 120));
            Field[23] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "g1", 1, new Vector2(0, 60));
            Field[24] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "g3", 1, new Vector2(120, 60));
            Field[25] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "g5", 0, new Vector2(300, 60));
            Field[26] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "g7", 2, new Vector2(360, 60));
            Field[27] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "h2", 1, new Vector2(60, 0));
            Field[28] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "h4", 0, new Vector2(180, 0));
            Field[29] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "h6", 2, new Vector2(300, 0));
            Field[30] = cell;
            cell = new Cell();
            cell.Initiazlize(Content, "h8", 2, new Vector2(420, 0));
            Field[31] = cell;
    





        }
        public void Update() { }
        public void Draw (SpriteBatch sprite)
        {
            for (int i = 0; i < Field.Length; i++)
            {
                cell.Draw(sprite);
            }
        }
    }


}

