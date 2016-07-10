using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;
using Catan.Interfaces;
using Microsoft.Xna.Framework.Content;


namespace Catan.GameObjects
{
    public class Settlement : NodeObject
    {
        protected bool isHarbour;

        // constructors
        public Settlement()
        {
            //TODO isHarbour -> from harbors list coordinates
        }

        public Settlement(uint nX, uint nY,
                           byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
            : base(nX, nY, 0, content, texture, x, y, width, height)
        {
        }

        // properties
        protected bool IsHarbour
        {
            get { return isHarbour; }
        }

        protected virtual int Productivity { get { return 0; } }

        // methods
        public virtual void Produce(ResourceType resource)
        {
            IPlayer owner = GameClass.Game.players[this.playerID-1];
            owner.AddResourceValue(resource, this.Productivity);            
        }

        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            if (this.PlayerID != 0 || !CheckTerrainCompatability() || CheckNeighbours(playerOnTurn))
            {
                throw new Exceptions.IllegalBuildPositionException("Can not build here!");
            }
        }

        public override void Destroy(IPlayer playerOnTurn)
        {
        }
        protected override bool CheckTerrainCompatability()
        {
            LandType land = MapObject.CheckLandType(this.NodeX, this.NodeY);
            return (land == LandType.Mainland || land == LandType.Shore);
        }

        public override bool CheckNeighbours(IPlayer playerOnTurn)
        {
            uint x = this.NodeX,
                 y = this.NodeY;

            if (GameClass.Game.Settlements[x - 1, y] != null && GameClass.Game.Settlements[x - 1, y].PlayerID != 0)
            { return true; }
            if (GameClass.Game.Settlements[x + 1, y] != null && GameClass.Game.Settlements[x + 1, y].PlayerID != 0)
            { return true; }
            if ((x + y) % 2 == 0)
            {
                if (GameClass.Game.Settlements[x, y + 1] != null && GameClass.Game.Settlements[x, y + 1].PlayerID != 0)
                { return true; }
            }
            else
            {
                if (GameClass.Game.Settlements[x, y - 1] != null && GameClass.Game.Settlements[x, y - 1].PlayerID != 0)
                { return true; }
            }
            return false;
        }
    }


}
