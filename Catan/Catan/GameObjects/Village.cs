using Catan.Interfaces;
using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public class Village : Settlement
    {
        private const uint ALLOWED_VILLAGES = 5;



        // propeties
        protected override uint Productivity { get { return 1; } }

        // methods
        public override void Build(IPlayer playerOnTurn)
        {
            base.Build(playerOnTurn);  // TODO: try-catch block here or where it is called?
            if (playerOnTurn.Villages.Count == ALLOWED_VILLAGES)
            {
                throw new Exception("Maximum number of Villages reached!");  //TODO: custom exception
            }
            uint brickAvailable = playerOnTurn.GetResourceValue(ResourceType.Brick),
                 lumberAvailable = playerOnTurn.GetResourceValue(ResourceType.Lumber),
                 grainAvailable = playerOnTurn.GetResourceValue(ResourceType.Grain),
                 woolAvailable = playerOnTurn.GetResourceValue(ResourceType.Wool);

            if (brickAvailable == 0 || lumberAvailable == 0 || grainAvailable == 0 || woolAvailable == 0)
            {
                throw new Exception("Not enough resources"); //TODO: custom exception
            }
            playerOnTurn.SetResourceValue(ResourceType.Brick, --brickAvailable);
            playerOnTurn.SetResourceValue(ResourceType.Lumber, --lumberAvailable);
            playerOnTurn.SetResourceValue(ResourceType.Grain, --grainAvailable);
            playerOnTurn.SetResourceValue(ResourceType.Wool, --woolAvailable);
        }

        public override void Destroy(IPlayer playerOnTurn)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override void Produce(ResourceType resource)
        {
            base.Produce(resource);
        }
    }
}
