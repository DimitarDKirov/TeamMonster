using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Dices
{
    public class Dice
    {
        public int FirstDice { get; private set; }

        public int SecondDice { get; private set; }

        public int Roll()
        {
            //get a random number object we can the use to determine the die face
            Random rand = new Random();
            this.FirstDice = rand.Next(1, 6);
            this.SecondDice = rand.Next(1, 6);
            return this.FirstDice + this.SecondDice;
        }
    }
}
