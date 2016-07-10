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
using Catan.Utilities;


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
        private SpriteFont font;

        private IList<NodeObject> nodeObject;
        private IList<LineObject> lineObject;
        private static Texture2D robberTexture;

        public HexField(TerrainType terrain, ContentManager content, int x, int y, int width, int height,              
                        IList<NodeObject> nodeObject, IList<LineObject> lineObject)
        {
            this.Terrain = terrain;
            this.Resource = DataGenerator.GenerateHexResource(this.Terrain);
            this.ProduceAtNumber = (uint)DataGenerator.GenerateHexProducingNumber(this.Terrain);
            this.IsRobbed = false;
            this.TradeInAct = false;

            this.NodeObject = nodeObject;
            this.LineObject = lineObject;

            //Drawable
            this.Texture = content.Load<Texture2D>(DataGenerator.GenerateHexTextureName(this.Terrain));
            this.font = content.Load<SpriteFont>("Arial");
            this.Rectangle = new Rectangle(x, y, width, height);
            this.ScreenX = x;
            this.ScreenY = y;
            this.DX = (uint)width;
            this.DY = (uint)height;
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

        public IList<NodeObject> NodeObject
        {
            get { return this.nodeObject; }
            private set { this.nodeObject = value; }
        }

        public IList<LineObject> LineObject
        {
            get { return this.lineObject; }
            private set { this.lineObject = value; }
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
            Vector2 textSize = this.font.MeasureString(this.ProduceAtNumber.ToString());
            Vector2 textCenter = new Vector2(this.ScreenX + 35, this.ScreenY + 40);

            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
            spriteBatch.DrawString(this.font, this.ProduceAtNumber.ToString(), textCenter - (textSize / 2), Color.SaddleBrown); 
        }


        public int ScreenX
        {
            get;
            private set;
        }

        public int ScreenY
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

        public bool CLickBelongToObject(int clickedX, int clickedY)
        {
            return (this.ScreenX <= clickedX) && (this.ScreenX + this.DX >= clickedX) &&
                   (this.ScreenY <= clickedY) && (this.ScreenY + this.DY >= clickedY);
        }
    }
}

