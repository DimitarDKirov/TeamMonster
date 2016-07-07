using Catan.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Catan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Catan.Common
{
    public class HexField : IClickable, IDrawableCustom
    {
        private TerrainType terrain;
        private ResourceType resourse;
        private uint produceAtNumber;
        private bool isRobbed;
        private bool tradeInAct;

        private Rectangle rectangle;
        private Texture2D texture;


        public HexField()
        {
            this.Terrain = terrain;
            this.Resource = resourse;
            this.ProduceAtNumber = produceAtNumber;
            this.IsRobbed = false;
            this.TradeInAct = false;
            this.Rectangle = rectangle;
            this.Texture = texture;
        }

        public TerrainType Terrain
        {
            get { return this.terrain; }
            set { this.terrain = value; }
        }

        public ResourceType Resource
        {
            get { return this.resourse; }
            set { this.resourse = value; }
        }

        public uint ProduceAtNumber
        {
            get { return this.produceAtNumber; }
            set { this.produceAtNumber = value; }
        }

        public bool IsRobbed
        {
            get { return this.isRobbed; }
            set { this.isRobbed = value; }
        }

        public bool TradeInAct
        {
            get { return this.tradeInAct; }
            set { this.tradeInAct = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
            private set
            {
                this.rectangle = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            private set
            {
                this.texture = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }

    }
}

