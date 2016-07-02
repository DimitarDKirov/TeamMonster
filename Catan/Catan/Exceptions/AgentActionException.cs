namespace Catan.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AgentActionException : Exception
    {
        public bool StopGame { get; private set; }

        public AgentActionException(bool stopGame = false)
        {
            StopGame = stopGame;
        }

        public AgentActionException(string msg, bool stopGame = false)
            : base(msg)
        {
            StopGame = stopGame;
        }
    }
}
