using Catan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.DevelopmentCards
{
    abstract class DevelopmentCard
    {
        private bool isPlayed;

        private Player  owner;

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }


        public bool IsPlayed
        {
            get { return this.isPlayed; }
            set { this.isPlayed = value; }
        }

        public virtual void Activate()
        {
            this.owner.DevCardsPossedsed.Remove(this);
        }

    }
}
