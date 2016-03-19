﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Checkers
{
    class Cell
    {
        enum Color { white, black, none }
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
        private bool queen;

        public bool Queen
        {
            get { return queen; }
            set { queen = value; }
        }
        private bool border;

        public bool Border
        {
            get { return border; }
            set { border = value; }
        }
        #region Diagonals;
        // A1 - H8
        private bool goldWay;

        public bool GoldWay
        {
            get { return goldWay; }
            set { goldWay = value; }
        }
        // G1 - A7
        private bool doubleWay1;

        public bool DoubleWay1
        {
            get { return doubleWay1; }
            set { doubleWay1 = value; }
        }
        // H2 - A8
        private bool  doubleWay2;

        public bool DoubleWay2
        {
            get { return doubleWay2; }
            set { doubleWay2 = value; }
        }
        #region tripleWay
        // C1 - A3
        private bool tripleWay1;

        public bool TripleWay1
        {
            get { return tripleWay1; }
            set { tripleWay1 = value; }
        }
        // A3 - F8
        private bool tripleWay2;

        public bool TripleWay2
        {
            get { return tripleWay2; }
            set { tripleWay2 = value; }
        }
        // F8 - H6
        private bool tripleWay3;

        public bool TripleWay3
        {
            get { return tripleWay3; }
            set { tripleWay3 = value; }
        }
        // H6 - C1
        private bool tripleWay4;

        public bool TripleWay4
        {
            get { return tripleWay4; }
            set { tripleWay4 = value; }
        }
        #endregion
        #region ultraWay
        // A5 - D8
        private bool ultraWay1;

        public bool UltraWay1
        {
            get { return ultraWay1; }
            set { ultraWay1 = value; }
        }
        // D8 - H4
        private bool ultraWay2;

        public bool UltraWay2
        {
            get { return ultraWay2; }
            set { ultraWay2 = value; }
        }
        // H4 - E1
        private bool ultraWay3;

        public bool UltraWay3
        {
            get { return ultraWay3; }
            set { ultraWay3 = value; }
        }

        // E1 - A5
        private bool ultraWay4;

        public bool UltraWay4
        {
            get { return ultraWay4; }
            set { ultraWay4 = value; }
        }

        #endregion

        #endregion
        public void Update ()
        {
            switch(color)
            {
                case Color.white :
                    Ocupied = 1;
                    break;
                case Color.black:
                    Ocupied = 2; 
                    break;
                case Color.none:
                    Ocupied = 0;
                    break;
            }
        }
        public void OnMove(string element)
        {
            if (element == "Graphics\\white")//WhitePlayer Ocupied
            {
                color = Color.white;
            }
            if (element == "Graphics\\black")//BlackOcupied
            {
                color = Color.black;
            }
            if (element == null)//No one
            {
                color = Color.none;
            }
        }

    }
}
