using System;
using Catan;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;

namespace Catan.GameObjects
{
    public class Harbour : LineObject
    {
        private ResourceType tradingResource;


        public override bool CheckTerrainCompatability()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        // properties
        public ResourceType TradingResource
        {
            get
            {
                return this.tradingResource;
            }
            private set
            {
                this.tradingResource = value;
            }
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
