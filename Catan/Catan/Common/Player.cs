using Catan.Constants;
using Catan.DevelopmentCards;
using Catan.GameObjects;
using Catan.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Common
{
    public class Player : IPlayer, IDrawableCustom
    {
        //Fields
        private readonly byte id;
        private string userName;
        private Color color;
        private IList<Unit> units;
        private IList<Village> villages;
        private IList<Town> towns;
        private IList<Harbour> harbours;
        private IList<DevelopmentCard> devCardsPossesed;
        private int points;
        private uint[] resources;
        private static byte serialNumber;

        //Constructors
        static Player()
        {
            serialNumber = 0;
        }

        public Player(string userName, Color color, ContentManager content, int x, int y, int width, int height)
        {
            this.UserName = userName;
            this.Color = color;
            this.Units = new List<Unit>();
            this.Villages = new List<Village>();
            this.Towns = new List<Town>();
            this.Harbours = new List<Harbour>();
            this.DevCardsPossedsed = new List<DevelopmentCard>(3);
            this.points = 0;
            this.resources = new uint[CommonConstants.ResourceTypesNumber];
            serialNumber++;
            this.id = serialNumber;
            //For visualization
            this.Texture = content.Load<Texture2D>("screenplayer" + this.id);
            this.Rectangle = new Rectangle(x, y, width, height);

        }

        //Properties
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public IList<Unit> Units
        {
            get { return this.units; }
            set { this.units = value; }
        }

        public IList<Village> Villages
        {
            get { return this.villages; }
            set { this.villages = value; }
        }

        public IList<Town> Towns
        {
            get { return this.towns; }
            set { this.towns = value; }
        }

        public IList<Harbour> Harbours
        {
            get { return this.harbours; }
            set { this.harbours = value; }
        }

        public IList<DevelopmentCard> DevCardsPossedsed
        {
            get { return this.devCardsPossesed; }
            set { this.devCardsPossesed = value; }
        }

        public int Points
        {
            get { return this.points; }
            private set { this.points = value; }
        }

        public byte Id
        {
            get { return this.id; }
        }

        public Rectangle Rectangle { get; private set; }

        public Texture2D Texture { get; private set; }

        //Methods
        public void AddPoints(int points)
        {
            this.Points += points;
        }

        public void RemovePoints(int points)
        {
            this.Points -= points;
        }

        public uint GetResourceValue(ResourceType resource)
        {
            return this.resources[(int)resource];
        }

        public void SetResourceValue(ResourceType resource, uint value)
        {
            this.resources[(int)resource] = value;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }
    }
}
