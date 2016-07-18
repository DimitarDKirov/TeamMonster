using Catan.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Common
{
    public class Dice : IDrawableCustom
    {
        private Texture2D dice1Texture;
        private Texture2D dice2Texture;
        private ContentManager customContent;
        public Dice(ContentManager content, string texture, int x, int y, int width, int height)
        {
            this.Texture = content.Load<Texture2D>(texture);
            this.Rectangle = new Rectangle(x, y, width, height);
            this.customContent = content;
        }
        public int FirstDice { get; private set; }

        public int SecondDice { get; private set; }

        public Rectangle Rectangle {get; private set;}

        public Texture2D Texture {get; private set;}


        public int Roll()
        {
            //get a random number object we can the use to determine the die face
            Random rand = new Random();
            int diceNumber;
            this.FirstDice = rand.Next(1, 7);
            this.SecondDice = rand.Next(1, 7);
            diceNumber = this.FirstDice + this.SecondDice;
            if (diceNumber != 7)
            {
                CollectResources(diceNumber);
            }
            return diceNumber;
        }

        private void CollectResources(int diceNumber)
        {
            if (GameClass.Game.HexFields != null)
            {
                foreach (var hexField in GameClass.Game.HexFields)
                {
                    if (hexField!=null && hexField.ProduceAtNumber == diceNumber)
                    {
                        foreach (var node in hexField.NodeObjects)
                        {
                            foreach (var settlement in GameClass.Game.Settlements)
                            {
                                if (node != null && settlement != null && settlement.PlayerID != 0 &&
                                    settlement.NodeX == node.NodeX && settlement.NodeY == node.NodeY)
                                {
                                    settlement.Produce(hexField.Resource);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            this.dice1Texture = this.customContent.Load<Texture2D>("dice" + this.FirstDice);
            this.dice2Texture = this.customContent.Load<Texture2D>("dice" + this.SecondDice);
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White * 0.0f);
            spriteBatch.Draw(this.dice1Texture, new Rectangle(670, 530, 50, 50), Color.White);
            spriteBatch.Draw(this.dice2Texture, new Rectangle(730, 530, 50, 50), Color.White);
     
        }
    }
}
