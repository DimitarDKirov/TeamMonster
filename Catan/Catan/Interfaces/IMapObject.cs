using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Interfaces
{
    public interface IMapObject:IBuildable, IClickable, IDrawableCustom
    {
        byte PlayerID { get; }
        bool IsActive { get; }

        
    }
}
