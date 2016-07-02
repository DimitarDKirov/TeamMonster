namespace Catan.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Thrown on attempts to draw cards from an empty pile
    /// </summary>
    class NoMoreCardsException : AgentActionException
    {
        public NoMoreCardsException()
        {

        }
        public NoMoreCardsException(string message)
            : base(message)
        {

        }
    }
}
