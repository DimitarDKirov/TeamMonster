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

        public static TerrainType GenerateHexTerrain()
        {

            Array values = Enum.GetValues(typeof(TerrainType));

            TerrainType randomTerrain = (TerrainType)values.GetValue(random.Next(1, values.Length));

            return randomTerrain;
        }

        public static int GenerateHexProducingNumber()
        {
            var tempValue = 7;
            while (true)
            {
                tempValue = random.Next(1, 13);
                if (tempValue != 7)
                {
                    return tempValue;
                }
            }
        }
    }
}
