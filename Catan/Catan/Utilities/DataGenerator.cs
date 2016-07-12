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
        public static string GenerateRoadName(uint x, uint y)
        {
            switch (y)
            {
                case 3:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalright";
                    }
                    else
                    {
                        return "roaddiagonalleft";
                    }
                case 4: return "roadvertical";
                case 5:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalleft";
                    }
                    else
                    {
                        return "roaddiagonalright";
                    }
                case 6: return "roadvertical";
                case 7:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalright";
                    }
                    else
                    {
                        return "roaddiagonalleft";
                    }
                case 8: return "roadvertical";
                case 9:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalleft";
                    }
                    else
                    {
                        return "roaddiagonalright";
                    }
                case 10: return "roadvertical";
                case 11:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalright";
                    }
                    else
                    {
                        return "roaddiagonalleft";
                    }
                case 12: return "roadvertical";
                case 13:
                    if (x % 2 == 0)
                    {
                        return "roaddiagonalleft";
                    }
                    else
                    {
                        return "roaddiagonalright";
                    }
                default:
                    break;
            }

            return "transperent";
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
                if (HexField.UsedProduceNumbers[tempValue]>0)
                {
                    HexField.UsedProduceNumbers[tempValue]--;
                    return tempValue;
                }
            }
        }
    }
}
