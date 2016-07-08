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

        public override bool CheckTerrainCompatability()
        {
            LandType landStartPoint = MapObject.CheckLandType(this.StartPointX, this.StartPointY);
            LandType landEndPoint = MapObject.CheckLandType(this.EndPointX, this.EndPointY);
            return ((landStartPoint == LandType.Sea || landStartPoint == LandType.Shore) &&
                    (landEndPoint == LandType.Sea || landEndPoint == LandType.Shore));
        }

        public override void Build(IPlayer playerOnTurn)
        {
        }

        public override void Destroy(IPlayer playerOnTurn)
        {

        }
    }
}
