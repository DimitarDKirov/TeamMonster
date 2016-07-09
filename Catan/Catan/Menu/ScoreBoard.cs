using Catan.Common;
using Catan.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Menu
{
    public class ScoreBoard : IDrawableCustom
    {
        private IList<Player> playersScoreBoard;
        private readonly SpriteFont font;

        public ScoreBoard(IList<Player> players, ContentManager content, string texture, int x, int y, int width, int height)
        {
            this.PlayersScoreBoard = players;
            this.font = content.Load<SpriteFont>("Arial");
            this.Texture = content.Load<Texture2D>(texture);
            this.Rectangle = new Rectangle(x, y, width, height);

        }

        //properties
        public IList<Player> PlayersScoreBoard
        {
            get 
            { 
                return this.playersScoreBoard; 
            }
            private set 
            {
                if (value.Count == 0)
                {
                    throw new ArgumentException("The player scoreboard list is empty");
                }
                else this.playersScoreBoard = value;
            }
        }

        public Rectangle Rectangle { get; private set; }

        public Texture2D Texture { get; private set; }

        //methods
        public void Update(IList<Player> players)
        {
            this.PlayersScoreBoard = players.OrderByDescending(p => p.Points)
                                            .ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);

            for (int i = 0; i < this.PlayersScoreBoard.Count; i++)
            {
                var playerInfo = string.Format("{0} - {1}", this.PlayersScoreBoard[i].UserName, this.PlayersScoreBoard[i].Points);
                spriteBatch.DrawString(this.font, playerInfo, new Vector2(10, i * 20 + 10), Color.Brown);
            }
        }
    }
}
