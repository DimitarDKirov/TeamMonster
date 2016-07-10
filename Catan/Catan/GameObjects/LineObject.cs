using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Catan.Common;
using Microsoft.Xna.Framework.Content;
using Catan.Interfaces;

namespace Catan.GameObjects
{
    public class LineObject : MapObject
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

        public LineObject(byte playerID)
            : this(0, 0, 0, 0, playerID)
        {
        }
        public LineObject(uint startX, uint startY, uint endX, uint endY, byte playerID)
            : base(playerID)
        {
            this.StartPointX = startX;
            this.StartPointY = startY;
            this.EndPointX = endX;
            this.EndPointY = endY;
        }
        public LineObject(byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
            : this(0, 0, 0, 0, playerID, content, texture, x, y, width, height)
        {
        }
        public LineObject(uint startX, uint startY, uint endX, uint endY,
                          byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
                          : base(playerID, content, texture, x, y, width, height)
        {
            this.StartPointX = startX;
            this.StartPointY = startY;
            this.EndPointX = endX;
            this.EndPointY = endY;
        }

        //



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
        
        //methods
        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            if (this.PlayerID != playerOnTurn.Id && this.PlayerID != 0 && CheckTerrainCompatability())
            {
                throw new Exceptions.IllegalBuildPositionException("Can not build here!");
            }
        }
        public override void Destroy(IPlayer playerOnTurn)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
        public override bool CheckTerrainCompatability()
        {
            throw new NotImplementedException();
        }
    }
}
