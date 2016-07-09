using System;
using Catan;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;
using Catan.Interfaces;

namespace Catan.GameObjects
{
    public class Harbour
    {
        private ResourceType tradingResource;


       
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
     
    }
}
