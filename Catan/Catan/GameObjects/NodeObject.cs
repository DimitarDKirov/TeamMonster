using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public abstract class NodeObject : MapObject
    {
        protected uint nodeX;
        protected uint nodeY;

        // constructors
       public NodeObject():base()
        {
            this.nodeX = 0;
            this.nodeY = 0;
        }

        public NodeObject(byte playerID)
            : this(0, 0, playerID)
        {
        }
        public NodeObject(uint nX, uint nY, byte playerID)
            : base(playerID)
        {
            this.nodeX = nX;
            this.nodeY = nY;
        }
        public NodeObject(byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
            : this(0, 0,playerID, content, texture, x, y, width, height)
        {
        }
        public NodeObject(uint nX, uint nY, 
                          byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
                          : base(playerID, content, texture, x, y, width, height)
        {
            this.NodeX = nX;
            this.NodeY = nY;
        }

        //
        //properties
        public uint NodeX
        {
            get
            {
                return this.nodeX;
            }
            private set
            {
                this.nodeX = value;
            }
        }
        public uint NodeY
        {
            get
            {
                return this.nodeY;
            }
            private set
            {
                this.nodeY = value;
            }
        }
    }
}
