using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    class Player
    {
        private string name;
        private bool screenIsLocked;
        private int checkersColor;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public bool ScreenIsLocked
        {
            get
            {
                return screenIsLocked;
            }

            set
            {
                screenIsLocked = value;
            }
        }

        public int CheckersColor
        {
            get
            {
                return checkersColor;
            }

            set
            {
                checkersColor = value;
            }
        }
    }
}
