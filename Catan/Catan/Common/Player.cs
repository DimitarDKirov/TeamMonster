﻿using Catan.Constants;
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
        private ICollection<Unit> units;
        private ICollection<Village> villages;
        private ICollection<Town> towns;
        private IEnumerable<Harbour> harbours;
        private ICollection<IDevelopmentCard> devCardsPossesed;
        private int points;
        private uint[] resources;
        private static byte serialNumber;
        private IEnumerable<Settlement> settlements;
        private IList<LineObject> lineObjects;

        private readonly SpriteFont font;
        //event raised if a player wins 10 points and wins
        public event EventHandler WinPointsReached;

        //Constructors
        static Player()
        {
            serialNumber = 0;
        }

        public Player(string userName, Color color, ContentManager content, int x, int y, int width, int height)
        {
            this.UserName = userName;
            this.Color = color;
            this.units = new List<Unit>();
            this.villages = new List<Village>();
            this.towns = new List<Town>();
            this.harbours = new List<Harbour>();
            this.devCardsPossesed = new List<IDevelopmentCard>(3);
            this.points = 0;
            this.resources = new uint[CommonConstants.ResourceTypesNumber];
            serialNumber++;
            this.id = serialNumber;
            //For visualization
            this.Texture = content.Load<Texture2D>("screenplayer" + this.id);
            this.Rectangle = new Rectangle(x, y, width, height);
            this.settlements = new List<Settlement>();
            this.lineObjects = new List<LineObject>();
            this.font = content.Load<SpriteFont>("Arial");
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

        public IEnumerable<Unit> Units
        {
            get { return this.units; }
            //set { this.units = value; }
        }

        public ICollection<Village> Villages
        {
            get { return this.villages; }
            //set { this.villages = value; }
        }

        public ICollection<Town> Towns
        {
            get { return this.towns; }
            // set { this.towns = value; }
        }

        public IEnumerable<Harbour> Harbours
        {
            get { return this.harbours; }
            // set { this.harbours = value; }
        }

        public ICollection<IDevelopmentCard> DevCardsPossedsed
        {
            get { return this.devCardsPossesed; }
            //set { this.devCardsPossesed = value; }
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

        public ICollection<LineObject> LineObjects
        {
            get { return this.lineObjects; }
        }

        public IEnumerable<Settlement> Settlements
        {
            get { return this.settlements; }
        }

        //Methods
        public void AddPoints(int points)
        {
            this.Points += points;
            if (this.points >= CommonConstants.PointsToWin)
            {
                this.OnWinPointsReached();
            }
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

        public void AddResourceValue(ResourceType resource, int value)
        {
            uint oldValue = this.GetResourceValue(resource);
            int newValue = (int)oldValue + value;
            if (newValue < 0) newValue = 0;
            this.SetResourceValue(resource, (uint)newValue);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
            spriteBatch.DrawString(this.font, GetResourceValue(ResourceType.Brick).ToString(), new Vector2(132, 509), Color.Brown);
            spriteBatch.DrawString(this.font, GetResourceValue(ResourceType.Lumber).ToString(), new Vector2(193, 509), Color.Green);
            spriteBatch.DrawString(this.font, GetResourceValue(ResourceType.Iron).ToString(), new Vector2(254.3F, 509), Color.Black * 0.3F);
            spriteBatch.DrawString(this.font, GetResourceValue(ResourceType.Wool).ToString(), new Vector2(316.3F, 509), Color.DarkOliveGreen);
            spriteBatch.DrawString(this.font, GetResourceValue(ResourceType.Grain).ToString(), new Vector2(377.6F, 509), Color.SaddleBrown);
        }

        protected virtual void OnWinPointsReached()
        {
            if (this.WinPointsReached != null)
            {
                this.WinPointsReached(this, EventArgs.Empty);
            }
        }
    }
}
