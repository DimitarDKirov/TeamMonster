using Catan.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Common
{
    class Player
    {
        private string userName;
        private Color color;
        private IList<Unit> units;
        private IList<Village> villages;
        private IList<Town> towns;
        //private IList<DevelopmentCard> devCardsPossesed;

        public Player(string userName, Color color)
        {
            this.UserName = userName;
            this.Color = color;
            this.Units = new List<Unit>();
            this.Villages = new List<Village>();
            this.Towns = new List<Town>();
            //this.DevCardsPossedsed = new List<DevelopmentCard>();
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public IList<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public IList<Village> Villages
        {
            get { return villages; }
            set { villages = value; }
        }

        public IList<Town> Towns
        {
            get { return towns; }
            set { towns = value; }
        }

        //public IList<DevelopmentCard> DevCardsPossedsed
        //{
        //    get { return devCardsPossesed; }
        //    set { devCardsPossesed = value; }
        //}

    }
}
