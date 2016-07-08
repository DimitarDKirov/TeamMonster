using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;

namespace Catan.GameObjects
{
    public class Settlement : NodeObject
    {
        protected uint victoryPoints;
        protected bool isHarbour;

        // constructors
        public Settlement()
        {
            //TODO isHarbour -> from harbors list coordinates
        }


        // properies


        protected bool IsHarbour
        {
            get { return isHarbour; }
        }

        // methods

        public virtual void Produce()
        {

        }

        public virtual void Build(Player playerOnTurn) 
        {
            if (this.PlayerID!=playerOnTurn.UserName && CheckTerrainCompatability())  //TODO replace UserNAme with ID
	        {
		        
	        }
        }

        public virtual void Destroy(Player playerOnTurn) { }
        public override bool CheckTerrainCompatability()
        {
           LandType land = MapObject.CheckLandType(this.NodeX, this.NodeY);
           return (land == LandType.Mainland || land == LandType.Shore);
        }
    }


}
