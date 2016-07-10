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

        protected virtual uint Productivity { get { return 0; } }

        // methods
        public virtual void Produce(ResourceType resource)
        {
            /*
            IPlayer owner = GameClass.Players[this.playerID];
            owner.SetResourceValue(resource, (owner.GetResourceValue(resource) + this.Productivity));  //TODO: implement method on Palyer to decerement with amount
            */
        }

        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            if (this.PlayerID!=0 && CheckTerrainCompatability())
            {
                throw new Exceptions.IllegalBuildPositionException("Can not build here!");  
            }
        }

        public override void Destroy(IPlayer playerOnTurn) 
        {
        }
        public override bool CheckTerrainCompatability()
        {
            LandType land = MapObject.CheckLandType(this.NodeX, this.NodeY);
            return (land == LandType.Mainland || land == LandType.Shore);
        }
    }


}
