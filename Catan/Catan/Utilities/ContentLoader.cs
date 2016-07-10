using Catan.Common;
using Catan.Constants;
using Catan.DevelopmentCards;
using Catan.GameObjects;
using Catan.Interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Utilities
{
    public static class ContentLoader
    {
        private const int NumberOfKnightCard = 14;
        private const int NumberOfVictoryPointCard = 5;
        private const int NumberOfRoadBuildCard = 3;
        private const int NumberOfResourceGetCard = 3;

        public static void LoadSettlements(Settlement[,] settlement, ContentManager content)
        {
            //20, 9
            settlement[7, 1] = new Settlement(7, 1, 0, content, "transperent", 270, 125, 20, 20);
            settlement[8, 1] = new Settlement(8, 1, 0, content, "transperent", 310, 105, 20, 20);
            settlement[9, 1] = new Settlement(9, 1, 0, content, "transperent", 350, 125, 20, 20);
            settlement[10, 1] = new Settlement(10, 1, 0, content, "transperent", 390, 105, 20, 20);
            settlement[11, 1] = new Settlement(11, 1, 0, content, "transperent", 430, 125, 20, 20);
            settlement[12, 1] = new Settlement(12, 1, 0, content, "transperent", 470, 105, 20, 20);
            settlement[13, 1] = new Settlement(13, 1, 0, content, "transperent", 510, 125, 20, 20);

            settlement[6, 2] = new Settlement(6, 2, 0, content, "transperent", 230, 195, 20, 20);
            settlement[7, 2] = new Settlement(7, 2, 0, content, "transperent", 270, 175, 20, 20);
            settlement[8, 2] = new Settlement(8, 2, 0, content, "transperent", 310, 195, 20, 20);
            settlement[9, 2] = new Settlement(9, 2, 0, content, "transperent", 350, 175, 20, 20);
            settlement[10, 2] = new Settlement(10, 2, 0, content, "transperent", 390, 195, 20, 20);
            settlement[11, 2] = new Settlement(11, 2, 0, content, "transperent", 430, 175, 20, 20);
            settlement[12, 2] = new Settlement(12, 2, 0, content, "transperent", 470, 195, 20, 20);
            settlement[13, 2] = new Settlement(13, 2, 0, content, "transperent", 510, 175, 20, 20);
            settlement[14, 2] = new Settlement(12, 2, 0, content, "transperent", 550, 195, 20, 20);

            settlement[5, 3] = new Settlement(5, 3, 0, content, "transperent", 190, 265, 20, 20);
            settlement[6, 3] = new Settlement(6, 3, 0, content, "transperent", 230, 245, 20, 20);
            settlement[7, 3] = new Settlement(7, 3, 0, content, "transperent", 270, 265, 20, 20);
            settlement[8, 3] = new Settlement(8, 3, 0, content, "transperent", 310, 245, 20, 20);
            settlement[9, 3] = new Settlement(9, 3, 0, content, "transperent", 350, 265, 20, 20);
            settlement[10, 3] = new Settlement(10, 3, 0, content, "transperent", 390, 245, 20, 20);
            settlement[11, 3] = new Settlement(11, 3, 0, content, "transperent", 430, 265, 20, 20);
            settlement[12, 3] = new Settlement(12, 3, 0, content, "transperent", 470, 245, 20, 20);
            settlement[13, 3] = new Settlement(13, 3, 0, content, "transperent", 510, 265, 20, 20);
            settlement[14, 3] = new Settlement(14, 3, 0, content, "transperent", 550, 245, 20, 20);
            settlement[15, 3] = new Settlement(15, 3, 0, content, "transperent", 590, 265, 20, 20);

            settlement[5, 4] = new Settlement(5, 4, 0, content, "transperent", 190, 315, 20, 20);
            settlement[6, 4] = new Settlement(6, 4, 0, content, "transperent", 230, 335, 20, 20);
            settlement[7, 4] = new Settlement(7, 4, 0, content, "transperent", 270, 315, 20, 20);
            settlement[8, 4] = new Settlement(8, 4, 0, content, "transperent", 310, 335, 20, 20);
            settlement[9, 4] = new Settlement(9, 4, 0, content, "transperent", 350, 315, 20, 20);
            settlement[10, 4] = new Settlement(10, 4, 0, content, "transperent", 390, 335, 20, 20);
            settlement[11, 4] = new Settlement(11, 4, 0, content, "transperent", 430, 315, 20, 20);
            settlement[12, 4] = new Settlement(12, 4, 0, content, "transperent", 470, 335, 20, 20);
            settlement[13, 4] = new Settlement(13, 4, 0, content, "transperent", 510, 315, 20, 20);
            settlement[14, 4] = new Settlement(14, 4, 0, content, "transperent", 550, 335, 20, 20);
            settlement[15, 4] = new Settlement(15, 4, 0, content, "transperent", 590, 315, 20, 20);

            settlement[6, 5] = new Settlement(6, 5, 0, content, "transperent", 230, 385, 20, 20);
            settlement[7, 5] = new Settlement(7, 5, 0, content, "transperent", 270, 405, 20, 20);
            settlement[8, 5] = new Settlement(8, 5, 0, content, "transperent", 310, 385, 20, 20);
            settlement[9, 5] = new Settlement(9, 5, 0, content, "transperent", 350, 405, 20, 20);
            settlement[10, 5] = new Settlement(10, 5, 0, content, "transperent", 390, 385, 20, 20);
            settlement[11, 5] = new Settlement(11, 5, 0, content, "transperent", 430, 405, 20, 20);
            settlement[12, 5] = new Settlement(12, 5, 0, content, "transperent", 470, 385, 20, 20);
            settlement[13, 5] = new Settlement(13, 5, 0, content, "transperent", 510, 405, 20, 20);
            settlement[14, 5] = new Settlement(14, 5, 0, content, "transperent", 550, 385, 20, 20);

            settlement[7, 6] = new Settlement(7, 6, 0, content, "transperent", 270, 455, 20, 20);
            settlement[8, 6] = new Settlement(8, 6, 0, content, "transperent", 310, 475, 20, 20);
            settlement[9, 6] = new Settlement(9, 6, 0, content, "transperent", 350, 455, 20, 20);
            settlement[10, 6] = new Settlement(10, 6, 0, content, "transperent", 390, 475, 20, 20);
            settlement[11, 6] = new Settlement(11, 6, 0, content, "transperent", 430, 455, 20, 20);
            settlement[12, 6] = new Settlement(12, 6, 0, content, "transperent", 470, 475, 20, 20);
            settlement[13, 6] = new Settlement(13, 6, 0, content, "transperent", 510, 455, 20, 20);
        }

        public static void LoadroadsAndBoats(LineObject[,] lineObject, ContentManager content)
        {
            //20, 17
            lineObject[7, 3] = new LineObject(7, 1, 8, 1, 0, content, "transperent", 290, 110, 20, 30);
            lineObject[8, 3] = new LineObject(8, 1, 9, 1, 0, content, "transperent", 330, 110, 20, 30);
            lineObject[9, 3] = new LineObject(9, 1, 10, 1, 0, content, "transperent", 370, 110, 20, 30);
            lineObject[10, 3] = new LineObject(10, 1, 11, 1, 0, content, "transperent", 410, 110, 20, 30);
            lineObject[11, 3] = new LineObject(11, 1, 12, 1, 0, content, "transperent", 450, 110, 20, 30);
            lineObject[12, 3] = new LineObject(12, 1, 13, 1, 0, content, "transperent", 490, 110, 20, 30);

            lineObject[7, 4] = new LineObject(7, 1, 7, 2, 0, content, "transperent", 270, 145, 20, 30);
            lineObject[9, 4] = new LineObject(9, 1, 9, 2, 0, content, "transperent", 350, 145, 20, 30);
            lineObject[11, 4] = new LineObject(11, 1, 11, 2, 0, content, "transperent", 430, 145, 20, 30);
            lineObject[13, 4] = new LineObject(13, 1, 13, 2, 0, content, "transperent", 510, 145, 20, 30);

            lineObject[6, 5] = new LineObject(6, 2, 7, 2, 0, content, "transperent", 250, 180, 20, 30);
            lineObject[7, 5] = new LineObject(7, 2, 8, 2, 0, content, "transperent", 290, 180, 20, 30);
            lineObject[8, 5] = new LineObject(8, 2, 9, 2, 0, content, "transperent", 330, 180, 20, 30);
            lineObject[9, 5] = new LineObject(9, 2, 10, 2, 0, content, "transperent", 370, 180, 20, 30);
            lineObject[10, 5] = new LineObject(10, 2, 11, 2, 0, content, "transperent", 410, 180, 20, 30);
            lineObject[11, 5] = new LineObject(11, 2, 12, 2, 0, content, "transperent", 450, 180, 20, 30);
            lineObject[12, 5] = new LineObject(12, 2, 13, 2, 0, content, "transperent", 490, 180, 20, 30);
            lineObject[13, 5] = new LineObject(13, 2, 14, 2, 0, content, "transperent", 530, 180, 20, 30);

            lineObject[6, 6] = new LineObject(6, 2, 6, 3, 0, content, "transperent", 230, 215, 20, 30);
            lineObject[8, 6] = new LineObject(8, 2, 8, 3, 0, content, "transperent", 310, 215, 20, 30);
            lineObject[10, 6] = new LineObject(10, 2, 10, 3, 0, content, "transperent", 390, 215, 20, 30);
            lineObject[12, 6] = new LineObject(12, 2, 12, 3, 0, content, "transperent", 470, 215, 20, 30);
            lineObject[14, 6] = new LineObject(14, 2, 14, 3, 0, content, "transperent", 550, 215, 20, 30);

            lineObject[5, 7] = new LineObject(5, 3, 6, 3, 0, content, "transperent", 210, 250, 20, 30);
            lineObject[6, 7] = new LineObject(6, 3, 7, 3, 0, content, "transperent", 250, 250, 20, 30);
            lineObject[7, 7] = new LineObject(7, 3, 8, 3, 0, content, "transperent", 290, 250, 20, 30);
            lineObject[8, 7] = new LineObject(8, 3, 9, 3, 0, content, "transperent", 330, 250, 20, 30);
            lineObject[9, 7] = new LineObject(9, 3, 10, 3, 0, content, "transperent", 370, 250, 20, 30);
            lineObject[10, 7] = new LineObject(10, 3, 11, 3, 0, content, "transperent", 410, 250, 20, 30);
            lineObject[11, 7] = new LineObject(11, 3, 12, 3, 0, content, "transperent", 450, 250, 20, 30);
            lineObject[12, 7] = new LineObject(12, 3, 13, 3, 0, content, "transperent", 490, 250, 20, 30);
            lineObject[13, 7] = new LineObject(13, 3, 14, 3, 0, content, "transperent", 530, 250, 20, 30);
            lineObject[14, 7] = new LineObject(14, 3, 15, 3, 0, content, "transperent", 570, 250, 20, 30);

            lineObject[5, 8] = new LineObject(5, 3, 5, 4, 0, content, "transperent", 190, 285, 20, 30);
            lineObject[7, 8] = new LineObject(7, 3, 7, 4, 0, content, "transperent", 270, 285, 20, 30);
            lineObject[9, 8] = new LineObject(9, 3, 9, 4, 0, content, "transperent", 350, 285, 20, 30);
            lineObject[11, 8] = new LineObject(11, 3, 11, 4, 0, content, "transperent", 430, 285, 20, 30);
            lineObject[13, 8] = new LineObject(13, 3, 13, 4, 0, content, "transperent", 510, 285, 20, 30);
            lineObject[15, 8] = new LineObject(15, 3, 15, 4, 0, content, "transperent", 590, 285, 20, 30);

            lineObject[5, 9] = new LineObject(5, 4, 6, 4, 0, content, "transperent", 210, 320, 20, 30);
            lineObject[6, 9] = new LineObject(6, 4, 7, 4, 0, content, "transperent", 250, 320, 20, 30);
            lineObject[7, 9] = new LineObject(7, 4, 8, 4, 0, content, "transperent", 290, 320, 20, 30);
            lineObject[8, 9] = new LineObject(8, 4, 9, 4, 0, content, "transperent", 330, 320, 20, 30);
            lineObject[9, 9] = new LineObject(9, 4, 10, 4, 0, content, "transperent", 370, 320, 20, 30);
            lineObject[10, 9] = new LineObject(10, 4, 11, 4, 0, content, "transperent", 410, 320, 20, 30);
            lineObject[11, 9] = new LineObject(11, 4, 12, 4, 0, content, "transperent", 450, 320, 20, 30);
            lineObject[12, 9] = new LineObject(12, 4, 13, 4, 0, content, "transperent", 490, 320, 20, 30);
            lineObject[13, 9] = new LineObject(13, 4, 14, 4, 0, content, "transperent", 530, 320, 20, 30);
            lineObject[14, 9] = new LineObject(14, 4, 15, 4, 0, content, "transperent", 570, 320, 20, 30);

            lineObject[6, 10] = new LineObject(6, 4, 6, 5, 0, content, "transperent", 230, 355, 20, 30);
            lineObject[8, 10] = new LineObject(8, 4, 8, 5, 0, content, "transperent", 310, 355, 20, 30);
            lineObject[10, 10] = new LineObject(10, 4, 10, 5, 0, content, "transperent", 390, 355, 20, 30);
            lineObject[12, 10] = new LineObject(12, 4, 12, 5, 0, content, "transperent", 470, 355, 20, 30);
            lineObject[14, 10] = new LineObject(14, 4, 14, 5, 0, content, "transperent", 550, 355, 20, 30);

            lineObject[6, 11] = new LineObject(6, 5, 7, 5, 0, content, "transperent", 250, 390, 20, 30);
            lineObject[7, 11] = new LineObject(7, 5, 8, 5, 0, content, "transperent", 290, 390, 20, 30);
            lineObject[8, 11] = new LineObject(8, 5, 9, 5, 0, content, "transperent", 330, 390, 20, 30);
            lineObject[9, 11] = new LineObject(9, 5, 10, 5, 0, content, "transperent", 370, 390, 20, 30);
            lineObject[10, 11] = new LineObject(10, 5, 11, 5, 0, content, "transperent", 410, 390, 20, 30);
            lineObject[11, 11] = new LineObject(11, 5, 12, 5, 0, content, "transperent", 450, 390, 20, 30);
            lineObject[12, 11] = new LineObject(12, 5, 13, 5, 0, content, "transperent", 490, 390, 20, 30);
            lineObject[13, 11] = new LineObject(13, 5, 14, 5, 0, content, "transperent", 530, 390, 20, 30);

            lineObject[7, 12] = new LineObject(7, 5, 9, 6, 0, content, "transperent", 270, 425, 20, 30);
            lineObject[9, 12] = new LineObject(9, 5, 7, 6, 0, content, "transperent", 350, 425, 20, 30);
            lineObject[11, 12] = new LineObject(11, 5, 11, 6, 0, content, "transperent", 430, 425, 20, 30);
            lineObject[13, 12] = new LineObject(13, 5, 13, 6, 0, content, "transperent", 510, 425, 20, 30);

            lineObject[7, 13] = new LineObject(7, 5, 8, 5, 0, content, "transperent", 290, 460, 20, 30);
            lineObject[8, 13] = new LineObject(8, 5, 9, 5, 0, content, "transperent", 330, 460, 20, 30);
            lineObject[9, 13] = new LineObject(9, 5, 10, 5, 0, content, "transperent", 370, 460, 20, 30);
            lineObject[10, 13] = new LineObject(10, 5, 11, 5, 0, content, "transperent", 410, 460, 20, 30);
            lineObject[11, 13] = new LineObject(11, 5, 12, 5, 0, content, "transperent", 450, 460, 20, 30);
            lineObject[12, 13] = new LineObject(12, 5, 13, 5, 0, content, "transperent", 490, 460, 20, 30);

        }

        public static void LoadHexFields(HexField[,] hexFields, LineObject[,] lineObject, Settlement[,] settlement, ContentManager content)
        {
            //10, 7
            hexFields[4, 1] = new HexField(DataGenerator.GenerateHexTerrain(), content, 285, 120, 70, 80,
                                            new List<NodeObject> { settlement[7, 1], settlement[8, 1], settlement[9, 1], settlement[7, 2], settlement[8, 2], settlement[9, 2]},
                                            new List<LineObject> { lineObject[7,3], lineObject[8,3], lineObject[7,4], lineObject[9,4], lineObject[7,5], lineObject[8,5]});
            hexFields[5, 1] = new HexField(DataGenerator.GenerateHexTerrain(), content, 365, 120, 70, 80,
                                            new List<NodeObject> { settlement[9, 1], settlement[10, 1], settlement[11, 1], settlement[9, 2], settlement[10, 2], settlement[11, 2] },
                                            new List<LineObject> { lineObject[9, 3], lineObject[10, 3], lineObject[9, 4], lineObject[11, 4], lineObject[9, 5], lineObject[10, 5] });
            hexFields[6, 1] = new HexField(DataGenerator.GenerateHexTerrain(), content, 445, 120, 70, 80,
                                            new List<NodeObject> { settlement[11, 1], settlement[12, 1], settlement[13, 1], settlement[11, 2], settlement[12, 2], settlement[13, 2] },
                                            new List<LineObject> { lineObject[11, 3], lineObject[12, 3], lineObject[11, 4], lineObject[13, 4], lineObject[11, 5], lineObject[12, 5] });

            hexFields[3, 2] = new HexField(DataGenerator.GenerateHexTerrain(), content, 245, 190, 70, 80,
                                            new List<NodeObject> { settlement[6, 2], settlement[7, 2], settlement[8, 2], settlement[6, 3], settlement[7, 3], settlement[8, 3] },
                                            new List<LineObject> { lineObject[6, 5], lineObject[7, 5], lineObject[6, 6], lineObject[8, 6], lineObject[6, 7], lineObject[7, 7] });
            hexFields[4, 2] = new HexField(DataGenerator.GenerateHexTerrain(), content, 325, 190, 70, 80,
                                            new List<NodeObject> { settlement[8, 2], settlement[9, 2], settlement[10, 2], settlement[8, 3], settlement[9, 3], settlement[10, 3] },
                                            new List<LineObject> { lineObject[8, 5], lineObject[9, 5], lineObject[8, 6], lineObject[10, 6], lineObject[8, 7], lineObject[9, 7] });
            hexFields[5, 2] = new HexField(DataGenerator.GenerateHexTerrain(), content, 405, 190, 70, 80,
                                            new List<NodeObject> { settlement[10, 2], settlement[11, 2], settlement[12, 2], settlement[10, 3], settlement[11, 3], settlement[12, 3] },
                                            new List<LineObject> { lineObject[10, 5], lineObject[11, 5], lineObject[10, 6], lineObject[12, 6], lineObject[10, 7], lineObject[11, 7] });
            hexFields[6, 2] = new HexField(DataGenerator.GenerateHexTerrain(), content, 485, 190, 70, 80,
                                            new List<NodeObject> { settlement[12, 2], settlement[13, 2], settlement[14, 2], settlement[12, 3], settlement[13, 3], settlement[14, 3] },
                                            new List<LineObject> { lineObject[12, 5], lineObject[13, 5], lineObject[12, 6], lineObject[14, 6], lineObject[12, 7], lineObject[13, 7] });

            hexFields[3, 3] = new HexField(DataGenerator.GenerateHexTerrain(), content, 205, 260, 70, 80,
                                            new List<NodeObject> { settlement[5, 3], settlement[6, 3], settlement[7, 3], settlement[5, 4], settlement[6, 4], settlement[7, 4] },
                                            new List<LineObject> { lineObject[5, 7], lineObject[6, 7], lineObject[5, 8], lineObject[7, 8], lineObject[5, 9], lineObject[6, 9] });
            hexFields[4, 3] = new HexField(DataGenerator.GenerateHexTerrain(), content, 285, 260, 70, 80,
                                            new List<NodeObject> { settlement[7, 3], settlement[8, 3], settlement[9, 3], settlement[7, 4], settlement[8, 4], settlement[9, 4] },
                                            new List<LineObject> { lineObject[7, 7], lineObject[8, 7], lineObject[7, 8], lineObject[9, 8], lineObject[7, 9], lineObject[8, 9] });
            hexFields[5, 3] = new HexField(TerrainType.Desert, content, 365, 260, 70, 80,
                                            new List<NodeObject> { settlement[9, 3], settlement[10, 3], settlement[11, 3], settlement[9, 4], settlement[10, 4], settlement[11, 4] },
                                            new List<LineObject> { lineObject[9, 7], lineObject[10, 7], lineObject[9, 8], lineObject[11, 8], lineObject[9, 9], lineObject[10, 9] });
            hexFields[6, 3] = new HexField(DataGenerator.GenerateHexTerrain(), content, 445, 260, 70, 80,
                                            new List<NodeObject> { settlement[11, 3], settlement[12, 3], settlement[13, 3], settlement[11, 4], settlement[12, 4], settlement[13, 4] },
                                            new List<LineObject> { lineObject[11, 7], lineObject[12, 7], lineObject[11, 8], lineObject[13, 8], lineObject[11, 9], lineObject[12, 9] });
            hexFields[7, 3] = new HexField(DataGenerator.GenerateHexTerrain(), content, 525, 260, 70, 80,
                                            new List<NodeObject> { settlement[13, 3], settlement[14, 3], settlement[15, 3], settlement[13, 4], settlement[14, 4], settlement[15, 4] },
                                            new List<LineObject> { lineObject[13, 7], lineObject[14, 7], lineObject[13, 8], lineObject[15, 8], lineObject[13, 9], lineObject[14, 9] });

            hexFields[3, 4] = new HexField(DataGenerator.GenerateHexTerrain(), content, 245, 330, 70, 80,
                                            new List<NodeObject> { settlement[6, 4], settlement[7, 4], settlement[8, 4], settlement[6, 5], settlement[7, 5], settlement[8, 5] },
                                            new List<LineObject> { lineObject[6, 9], lineObject[7, 9], lineObject[6, 10], lineObject[8, 10], lineObject[6, 11], lineObject[7, 11] });
            hexFields[4, 4] = new HexField(DataGenerator.GenerateHexTerrain(), content, 325, 330, 70, 80,
                                            new List<NodeObject> { settlement[8, 4], settlement[9, 4], settlement[10, 4], settlement[8, 5], settlement[9, 5], settlement[10, 5] },
                                            new List<LineObject> { lineObject[8, 9], lineObject[9, 9], lineObject[8, 10], lineObject[10, 10], lineObject[8, 11], lineObject[9, 11] });
            hexFields[5, 4] = new HexField(DataGenerator.GenerateHexTerrain(), content, 405, 330, 70, 80,
                                            new List<NodeObject> { settlement[10, 4], settlement[11, 4], settlement[12, 4], settlement[10, 5], settlement[11, 5], settlement[12, 5] },
                                            new List<LineObject> { lineObject[10, 9], lineObject[11, 9], lineObject[10, 10], lineObject[12, 10], lineObject[10, 11], lineObject[11, 11] });
            hexFields[6, 4] = new HexField(DataGenerator.GenerateHexTerrain(), content, 485, 330, 70, 80,
                                            new List<NodeObject> { settlement[12, 4], settlement[13, 4], settlement[14, 4], settlement[12, 5], settlement[13, 5], settlement[14, 5] },
                                            new List<LineObject> { lineObject[12, 9], lineObject[13, 9], lineObject[12, 10], lineObject[14, 10], lineObject[12, 11], lineObject[13, 11] });
           
            hexFields[4, 5] = new HexField(DataGenerator.GenerateHexTerrain(), content, 285, 400, 70, 80,
                                            new List<NodeObject> { settlement[7, 5], settlement[8, 5], settlement[9, 5], settlement[7, 6], settlement[8, 6], settlement[9, 6] },
                                            new List<LineObject> { lineObject[7, 11], lineObject[8, 11], lineObject[7, 12], lineObject[9, 12], lineObject[7, 13], lineObject[8, 13] });
            hexFields[5, 5] = new HexField(DataGenerator.GenerateHexTerrain(), content, 365, 400, 70, 80,
                                            new List<NodeObject> { settlement[8, 5], settlement[9, 5], settlement[10, 5], settlement[8, 6], settlement[9, 6], settlement[10, 6] },
                                            new List<LineObject> { lineObject[8, 11], lineObject[9, 11], lineObject[8, 12], lineObject[10, 12], lineObject[8, 13], lineObject[9, 13] });
            hexFields[6, 5] = new HexField(DataGenerator.GenerateHexTerrain(), content, 445, 400, 70, 80,
                                            new List<NodeObject> { settlement[10, 5], settlement[11, 5], settlement[12, 5], settlement[10, 6], settlement[11, 6], settlement[12, 6] },
                                            new List<LineObject> { lineObject[10, 11], lineObject[11, 11], lineObject[10, 12], lineObject[12, 12], lineObject[10, 13], lineObject[11, 13] });
        }

        public static ICollection<IDevelopmentCard> GenerateDevelopmentCards()
        {
            int[] developmentCardsTypesCount = new int[] { NumberOfKnightCard, NumberOfVictoryPointCard, NumberOfRoadBuildCard, NumberOfResourceGetCard };
            IDevelopmentCard[] developmentCards = new IDevelopmentCard[CommonConstants.DevelopmentCardsNumber];
            Random rand = new Random();
            for (int i = 0; i < CommonConstants.DevelopmentCardsNumber; i++)
            {
                int cardType = rand.Next(developmentCardsTypesCount.Length);
                while (developmentCardsTypesCount[cardType] <= 0)
                {
                    cardType = (cardType + 1) % developmentCardsTypesCount.Length;
                }
                developmentCardsTypesCount[cardType]--;
                IDevelopmentCard card = null;
                switch (cardType)
                {
                    case 0: card = new KnightCard(); break;
                    case 1: card = new VictoryPointCard(); break;
                    case 2: card = new RoadBuildCard(); break;
                    case 3: card = new ResourceGetCard(); break;
                    default: throw new ArgumentOutOfRangeException("Such development card type does not exist");
                }
                developmentCards[i] = card;
            }
            return developmentCards;
        }
    }
}
