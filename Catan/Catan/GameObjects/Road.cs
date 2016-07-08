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

        public override bool CheckTerrainCompatability()
        {
            LandType landStartPoint = MapObject.CheckLandType(this.StartPointX, this.StartPointY);
            LandType landEndPoint = MapObject.CheckLandType(this.EndPointX, this.EndPointY);
            return ((landStartPoint == LandType.Mainland || landStartPoint == LandType.Shore) &&
                    (landEndPoint == LandType.Mainland || landEndPoint == LandType.Shore));
        }

        // methods
        public override void Build(IPlayer playerOnTurn)
        {

        }

        public override void Destroy(IPlayer playerOnTurn)
        {

        }
    }
}
