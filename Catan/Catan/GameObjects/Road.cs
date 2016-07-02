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
            /*return (MapObject.CheckPointOnLand(this.StartPointX, this.StartPointY) &&
                    MapObject.CheckPointOnLand(this.EndPointX, this.EndPointY));*/
            return true;
        }

        // methods
        public override void Build()
        {

        }

        public override void Destroy()
        {

        }
    }
}
