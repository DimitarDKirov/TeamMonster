using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public abstract class LineObject : MapObject
    {
        protected uint startPointX;
        protected uint startPointY;
        protected uint endPointX;
        protected uint endPointY;

        //constructors
        public LineObject():base()
        {
            this.StartPointX = 0;
            this.StartPointY = 0;
            this.EndPointX = 0;
            this.EndPointY = 0;
        }
        public LineObject(uint startX, uint startY, uint endX, uint endY):base()
        {
            this.StartPointX = startX;
            this.StartPointY = startY;
            this.EndPointX = endX;
            this.EndPointY = endY;
        }

        public LineObject(byte playerID, uint startX, uint startY, uint endX, uint endY):base(playerID)
        {
            this.StartPointX = startX;
            this.StartPointY = startY;
            this.EndPointX = endX;
            this.EndPointY = endY;
        }

f        //

        // properties
        public uint StartPointX
        {
            get
            {
                return this.startPointX;
            }
            private set
            {
                this.startPointX = value;
            }
        }
        public uint StartPointY
        {
            get
            {
                return this.startPointY;
            }
            private set
            {
                this.startPointY = value;
            }
        }
        public uint EndPointX {
            get
            {
                return this.endPointX;
            }
            private set
            {
                this.endPointX = value;
            }
        }
        public uint EndPointY {
            get
            {
                return this.endPointY;
            }
            private set
            {
                this.endPointY = value;
            }
        }
        
    }
}
