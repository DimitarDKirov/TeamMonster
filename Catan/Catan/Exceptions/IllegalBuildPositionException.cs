namespace Catan.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Thrown when a build fails because it's in an illegal position
    /// </summary>
    class IllegalBuildPositionException : AgentActionException
    {
        public IllegalBuildPositionException()
        {
        }
        public IllegalBuildPositionException(string message)
            : base(message)
        {
        }
    }
}
