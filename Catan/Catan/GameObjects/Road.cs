using Catan.Common;
using Catan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public class Road : LineObject
    {
        private const uint ALLOWED_ROADS = 15;

        public override bool CheckTerrainCompatability()
        {
            LandType landStartPoint = MapObject.CheckLandType(this.StartPointX, this.StartPointY);
            LandType landEndPoint = MapObject.CheckLandType(this.EndPointX, this.EndPointY);
            return ((landStartPoint == LandType.Mainland || landStartPoint == LandType.Shore) &&
                    (landEndPoint == LandType.Mainland || landEndPoint == LandType.Shore));
        }

        // methods
        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            base.Build(playerOnTurn, buildWithDevCard);  // TODO: try-catch block here or where it is called?
            if (playerOnTurn.Villages.Count == ALLOWED_ROADS)
            {
                throw new Exception("Maximum number of raods reached!");  //TODO: custom exception
            }
            if (!buildWithDevCard)
            {
                uint brickAvailable = playerOnTurn.GetResourceValue(ResourceType.Brick),
                lumberAvailable = playerOnTurn.GetResourceValue(ResourceType.Lumber);

                if (brickAvailable == 0 || lumberAvailable == 0)
                {
                    throw new Exception("Not enough resources"); //TODO: custom exception
                }
                playerOnTurn.AddResourceValue(ResourceType.Brick, -1);
                playerOnTurn.AddResourceValue(ResourceType.Lumber, -1);
            }
        }

        public override void Destroy(IPlayer playerOnTurn)
        {

        }
    }
}
