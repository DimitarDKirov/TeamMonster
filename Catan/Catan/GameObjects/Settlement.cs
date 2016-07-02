using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public abstract class Settlement : NodeObject
    {
        protected uint victoryPoints;

        // constructors


        public override bool CheckTerrainCompatability()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }


}
