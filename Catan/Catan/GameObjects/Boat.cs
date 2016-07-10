using Catan.Common;
using Catan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public class Boat : LineObject
    {
        private const uint ALLOWED_BOATS = 16;
        protected override bool CheckTerrainCompatability()
        {
            LandType landStartPoint = MapObject.CheckLandType(this.StartPointX, this.StartPointY);
            LandType landEndPoint = MapObject.CheckLandType(this.EndPointX, this.EndPointY);
            return ((landStartPoint == LandType.Sea || landStartPoint == LandType.Shore) &&
                    (landEndPoint == LandType.Sea || landEndPoint == LandType.Shore));
        }

        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            base.Build(playerOnTurn, buildWithDevCard);  // TODO: try-catch block here or where it is called?
            if (playerOnTurn.Villages.Count == ALLOWED_BOATS)
            {
                throw new Exception("Maximum number of boats reached!");  //TODO: custom exception
            }
            if (!buildWithDevCard)
            {
                uint woolAvailable = playerOnTurn.GetResourceValue(ResourceType.Wool),
                lumberAvailable = playerOnTurn.GetResourceValue(ResourceType.Lumber);

                if (woolAvailable == 0 || lumberAvailable == 0)
                {
                    throw new Exception("Not enough resources"); //TODO: custom exception
                }
                playerOnTurn.AddResourceValue(ResourceType.Wool, -1);
                playerOnTurn.AddResourceValue(ResourceType.Lumber, -1);
            }           
        }

        public override void Destroy(IPlayer playerOnTurn)
        {

        }
    }
}
