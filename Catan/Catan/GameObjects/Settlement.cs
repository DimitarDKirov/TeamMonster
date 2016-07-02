using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;

namespace Catan.GameObjects
{
    public abstract class Settlement : NodeObject
    {
        protected uint victoryPoints;

        // constructors


        public override bool CheckTerrainCompatability()
        {
           LandType land = MapObject.CheckLandType(this.NodeX, this.NodeY);
           return (land == LandType.Mainland || land == LandType.Shore);
        }
    }


}
