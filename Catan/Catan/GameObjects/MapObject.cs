using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Catan.GameObjects
{
    public abstract class MapObject : Catan.Interfaces.IDrawableCustom
    {

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
            this.LocationX = x;
            this.LocationY = y;

        }

        // properties
        public byte PlayerID
        {
            get
            {
                return this.playerID;
            }
            private set
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

        public int LocationX { get; private set; }
        public int LocationY { get; private set; }

        // methods
        public abstract void Build();
        public abstract void Destroy();

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


    }
}
