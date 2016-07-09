using Catan.Interfaces;
using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.GameObjects
{
    public class Town : Settlement
    {
        private const uint ALLOWED_TOWNS = 4;
       


        //properties
        protected override uint Productivity { get { return 2; } }

        // methods
        public override void Build(IPlayer playerOnTurn)
        {
            base.Build(playerOnTurn);  // TODO: try-catch block here or where it is called?
            if (playerOnTurn.Towns.Count == ALLOWED_TOWNS)
            {
                throw new Exception("Maximum number of towns reached!");  //TODO: custom exception
            }
            uint ironAvailable = playerOnTurn.GetResourceValue(ResourceType.Iron),
                 grainAvailable = playerOnTurn.GetResourceValue(ResourceType.Grain);

            if (ironAvailable <= 2 || grainAvailable <= 1)
            {
                throw new Exception("Not enough resources"); //TODO: custom exception
            }
            playerOnTurn.AddResourceValue(ResourceType.Iron, -3);
            playerOnTurn.AddResourceValue(ResourceType.Grain, -2);
        }

        public override void Destroy(IPlayer playerOnTurn)
        {

        }

        public override void Produce(ResourceType resource)
        {
            base.Produce(resource);           
        }
    }
}
