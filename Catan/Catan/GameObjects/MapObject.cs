using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Common;

namespace Catan.GameObjects
{
    public abstract class MapObject
    {

        protected byte playerID;    // 0..4
        protected bool isActive;    // whether the object has been used through the current turn

        //constructor(s)
        public MapObject()
        {
            this.PlayerID = 0;
            this.IsActive = true;
        }
        public MapObject(byte playerID)
        {
            this.PlayerID = playerID;
            this.IsActive = true;
        }

        // properties
        public byte PlayerID
        {
            get
            {
                return this.playerID;
            }
            private set
            {
                this.playerID = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            private set
            {
                this.isActive = value;
            }
        }



        // methods
        public abstract void Build();
        public abstract void Destroy();

        public void Enable()
        {
            this.IsActive = true;
        }
        public void Disable()
        {
            this.IsActive = false;
        }


    }
}
