using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public abstract class NodeObject:MapObject
    {
        protected uint positionX;
        protected uint positionY;

        // constructor
        public NodeObject()
        {

        }
        public NodeObject(uint x, uint y)
        {

        }
        //properties
        public uint PositionX
        {
            get
            {
                return this.positionX;
            }
            private set
            {
                this.positionX = value;
            }
        }
        public uint PositionY
        {
            get
            {
                return this.positionY;
            }
            private set
            {
                this.positionY = value;
            }
        }
    }
}
