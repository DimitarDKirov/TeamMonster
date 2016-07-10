using Catan.Interfaces;
using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Catan.GameObjects
{
    public class Town : Settlement
    {
        private const uint ALLOWED_TOWNS = 4;
       
        // constructors
        public Town()
        {

        }
         public Town(uint nX, uint nY,
                           byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
            : base(nX, nY, playerID, content, texture, x, y, width, height)
        {
        }


        //properties
        protected override uint Productivity { get { return 2; } }
        public static int VictoryPointsRewarded { get { return 2; } }

        // methods
        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            base.Build(playerOnTurn, buildWithDevCard);  // TODO: try-catch block here or where it is called?
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
            playerOnTurn.AddPoints(VictoryPointsRewarded - Village.VictoryPointsRewarded);
        }

        public override void Destroy(IPlayer playerOnTurn)
        {
            playerOnTurn.AddPoints(-VictoryPointsRewarded + Village.VictoryPointsRewarded);

        }

        public override void Produce(ResourceType resource)
        {
            base.Produce(resource);           
        }
    }
}
