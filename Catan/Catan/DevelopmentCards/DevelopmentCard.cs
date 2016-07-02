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

        private Player owner;

        public DevelopmentCard()
        {
            this.IsPlayed = false;
        }

        public bool IsPlayed
        {
            get { return this.isPlayed; }
            set { this.isPlayed = value; }
        }

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public virtual void Activate()
        {
            //TODO activated card should be removed from palyr's array in PLayer class
            this.owner.DevCardsPossedsed.Remove(this);
            this.isPlayed = true;
        }

    }
}
