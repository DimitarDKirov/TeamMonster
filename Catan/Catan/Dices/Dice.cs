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

        public void Roll()
        {
            //get a random number object we can the use to determine the die face
            Random rand = new Random();
            FirstDice = rand.Next(1, 6);
            SecondDice = rand.Next(1, 6);
        }
    }
}
