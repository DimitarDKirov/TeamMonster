using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Interfaces
{
    public interface IBuildable
    {
        void Build(IPlayer playerOnTurn, bool buildWithDevCard);

        void Destroy(IPlayer playerOnTurn);
    }
}
