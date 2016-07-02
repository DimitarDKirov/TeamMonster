using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.DevelopmentCards
{
    class VictoryPointCard : DevelopmentCard
    {
        public override void Activate()
        {
            this.Owner.AddPoints(1);
        }
    }
}
