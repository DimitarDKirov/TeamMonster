using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Utilities
{
    public static class DataGenerator
    {
        private static Random random;

        static DataGenerator()
        {
            random = new Random();
        }
        public static string GenerateRoadImageType(uint x1, uint y1, uint x2, uint y2)
        {
            if (x1 < x2)
            {
                if ((x1 + y1) % 2 == 0)
                    return "roaddiagonalleft";
                else
                    return "roaddiagonalright";
            }
            else
            {
                return "roadvertical";
            }
        }

        public static ResourceType GenerateHexResource(TerrainType terrain)
        {
            switch (terrain)
            {
                case TerrainType.Desert: return ResourceType.None;

                case TerrainType.Fields: return ResourceType.Grain;

                case TerrainType.Forest: return ResourceType.Lumber;

                case TerrainType.Hills: return ResourceType.Brick;

                case TerrainType.Mountains: return ResourceType.Iron;

                case TerrainType.Pasture: return ResourceType.Wool;

                default:
                    break;
            }

            return ResourceType.None;
        }

        public static string GenerateHexTextureName(TerrainType terrain)
        {
            switch (terrain)
            {
                case TerrainType.Desert: return "desert";

                case TerrainType.Fields: return "field";

                case TerrainType.Forest: return "forest";

                case TerrainType.Hills: return "hill";

                case TerrainType.Mountains: return "mountain";

                case TerrainType.Pasture: return "pasture";

                default:
                    break;
            }

            return "desert";
        }
        public static TerrainType GenerateHexTerrain()
        {

            Array values = Enum.GetValues(typeof(TerrainType));

            TerrainType randomTerrain = (TerrainType)values.GetValue(random.Next(1, values.Length));

            return randomTerrain;
        }

        public static int GenerateHexProducingNumber(TerrainType terrain)
        {
            if (terrain == TerrainType.Desert)
            {
                return 7;
            }
            int tempValue;
            while (true)
            {
                tempValue = random.Next(2, 13);
                if (HexField.UsedProduceNumbers[tempValue] > 0)
                {
                    HexField.UsedProduceNumbers[tempValue]--;
                    return tempValue;
                }
            }
        }
    }
}
