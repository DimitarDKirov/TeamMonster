using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Catan.Interfaces;

namespace Catan.GameObjects
{
    public abstract class MapObject : IMapObject

    {
        //constants
        protected const uint TOP = 1;
        protected const uint BOTTOM = 6;
        protected const uint LEFT = 5;
        protected const uint RIGHT = 15;

        // fields
        protected byte playerID;    // 0..4
        protected bool isActive;    // whether the object has been used through the current turn
        private Rectangle rectangle;
        private Texture2D texture;

        //constructor(s)

        //Направих конструктор с необходимите неща за рисуване. Не съм махнал старите конструктори. :: Цветослав
        public MapObject()
        {
            this.PlayerID = 0;
            this.IsActive = true;
        }

        public MapObject(byte playerID)
        {
            this.PlayerID = playerID;
            this.IsActive = true;
        }

        protected MapObject(byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
        {
            this.PlayerID = playerID;
            this.IsActive = true;
            this.Texture = content.Load<Texture2D>(texture);
            this.Rectangle = new Rectangle(x, y, width, height);
            this.ScreenX = x;
            this.ScreenY = y;
        }

        // properties
        public byte PlayerID
        {
            get
            {
                return this.playerID;
            }
            protected set
            {
                this.playerID = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            private set
            {
                this.isActive = value;
            }
        }
        public uint DX { get; private set; }

        public uint DY { get; private set; }

        public int ScreenY { get; private set; }

        public int ScreenX { get; private set;}

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

        // methods
        public abstract void Build(IPlayer playerOnTurn, bool buildWithDevCard);

        public abstract void Destroy(IPlayer playerOnTurn);

        protected abstract bool CheckTerrainCompatability();

        public abstract bool CheckNeighbours(IPlayer playerOnTurn);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }

        public void Enable()
        {
            this.IsActive = true;
        }
        public void Disable()
        {
            this.IsActive = false;
        }

        protected static LandType CheckLandType(uint x, uint y)
        {
            if (x > LEFT && x < RIGHT && y > TOP && y < BOTTOM &&
                x + y > (TOP + BOTTOM + 1) && x + y < (LEFT + RIGHT) && x - y < (LEFT + RIGHT) / 2 && x - y > 2)
                return LandType.Mainland;
            else if (((x == LEFT || x == RIGHT) && y >= (TOP + BOTTOM) / 2 && y <= (TOP + BOTTOM + 1) / 2) ||
                     ((y == TOP || y == BOTTOM) && x >= LEFT + 2 && x <= RIGHT - 2) ||
                     (x > LEFT && x <= LEFT + 2 && ((x + y) == (LEFT + RIGHT) / 2 || (x + y) == (LEFT + RIGHT) / 2 - 1 || (x >= y && (x - y) / 2 == 0))) ||
                     (x >= RIGHT - 2 && x < RIGHT && (((x - y) - (LEFT + RIGHT) / 2) / 2 == 0 || ((x + y) - (LEFT + RIGHT)) / 2 == 0)))
                return LandType.Shore;
            else
                return LandType.Sea;
        }

        public virtual bool CLickBelongToObject(int clickedX, int clickedY) 
        {
            return (this.ScreenX <= clickedX) && (this.ScreenX+this.DX >= clickedX) &&
                   (this.ScreenY <= clickedY) && (this.ScreenY + this.DY >= clickedY);
        }
    }
}
