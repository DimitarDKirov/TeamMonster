using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Exceptions
{
    class IllegalActionException : AgentActionException
    {
        public IllegalActionException()
        {

        }

        public IllegalActionException(string message)
            : base(message)
        {

        }
    }
}
