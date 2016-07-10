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
    public class Village : Settlement
    {
        private const uint ALLOWED_VILLAGES = 5;

        // constructors
        public Village()
        {

        }
        public Village(uint nX, uint nY,
                           byte playerID, ContentManager content, string texture, int x, int y, int width, int height)
            : base(nX, nY, playerID, content, texture, x, y, width, height)
        {
        }


        // propeties
        protected override uint Productivity { get { return 1; } }
        public static int VictoryPointsRewarded { get { return 1; } }

        // methods
        public override void Build(IPlayer playerOnTurn, bool buildWithDevCard)
        {
            base.Build(playerOnTurn, buildWithDevCard);  // TODO: try-catch block here or where it is called?
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
            playerOnTurn.AddResourceValue(ResourceType.Brick, -1);
            playerOnTurn.AddResourceValue(ResourceType.Lumber, -1);
            playerOnTurn.AddResourceValue(ResourceType.Grain, -1);
            playerOnTurn.AddResourceValue(ResourceType.Wool, -1);
            playerOnTurn.AddPoints(VictoryPointsRewarded);
        }

        public override void Destroy(IPlayer playerOnTurn)
        {
            playerOnTurn.AddPoints(-VictoryPointsRewarded);
        }

        public override void Produce(ResourceType resource)
        {
            base.Produce(resource);
        }
    }
}
