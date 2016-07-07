using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeamWorkProjectOOP.HexFields
{
    public class HexField
    {
        private TerrainType terrain;
        private ResourceType resourse;
        private uint produceAtNumber;
        private bool isRobbed;
        private bool tradeInAct;

        public HexField()
        {
            this.Terrain = terrain;
            this.Resource = resourse;
            this.ProduceAtNumber = produceAtNumber;
            this.IsRobbed = false;
            this.TradeInAct = false;
        }

        public TerrainType Terrain
        {
            get { return this.terrain; }
            set { this.terrain = value; }
        }

        public ResourceType Resource
        {
            get { return this.resourse; }
            set { this.resourse = value; }
        }

        public uint ProduceAtNumber
        {
            get { return this.produceAtNumber; }
            set { this.produceAtNumber = value; }
        }

        public bool IsRobbed
        {
            get { return this.isRobbed; }
            set { this.isRobbed = value; }
        }

        public bool TradeInAct
        {
            get { return this.tradeInAct; }
            set { this.tradeInAct = value; }
        }

    }
}

