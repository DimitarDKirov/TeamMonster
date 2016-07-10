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

        public static void Trade(IPlayer playerOnTurn, ResourceType offerredResource, ResourceType targetResource)
        {
            int tradeRate=4;
            if (!HarbourExists(playerOnTurn, targetResource))
            {
                tradeRate = 2;
            }
            else if (!HarbourExists(playerOnTurn, ResourceType.None))
            {
                tradeRate = 3;
            }
            uint offeredResourceAvailable = playerOnTurn.GetResourceValue(offerredResource);
            if (offeredResourceAvailable < tradeRate)
            {
                throw new Exception("Not enough resources!"); // TOD: custime exception
            }

            playerOnTurn.AddResourceValue(offerredResource, -tradeRate);
            playerOnTurn.AddResourceValue(targetResource, 1);
        }

        private static bool HarbourExists(IPlayer playerOnTurn, ResourceType harbourResourceType)
        {
            foreach (var harbour in playerOnTurn.Harbours)
            {
                if (harbour.TradingResource == harbourResourceType)
                    return true;
            }
            return false;
        }
    }
}
