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
using Catan.GameObjects;
using Catan.Dices;


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

        private List<NodeObject> nodeObject;
        private List<LineObject> lineObject;
        private static Texture2D robberTexture;

        public HexField()
        {
            this.Terrain = terrain;
            this.Resource = resourse;
            this.ProduceAtNumber = produceAtNumber;
            this.IsRobbed = false;
            this.TradeInAct = false;
            this.Rectangle = rectangle;
            this.Texture = texture;
            this.NodeObject = nodeObject;
            this.LineObject = lineObject;
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

        public List<NodeObject> NodeObject
        {
            get { return this.nodeObject; }
            set { this.nodeObject = value; }
        }

        public List<LineObject> LineObject
        {
            get { return this.lineObject; }
            set { this.lineObject = value; }
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


        public uint ScreenX
        {
            get;
            private set;
        }

        public uint ScreenY
        {
            get;
            private set;
        }

        public uint DX
        {
            get;
            private set;
        }

        public uint DY
        {
            get;
            private set;
        }

        public bool CLickBelongToObject(uint clickedX, uint clickedY)
        {
            return (this.ScreenX <= clickedX) && (this.ScreenX + this.DX >= clickedX) &&
                   (this.ScreenY <= clickedY) && (this.ScreenY + this.DY >= clickedY);
        }
    }
}

