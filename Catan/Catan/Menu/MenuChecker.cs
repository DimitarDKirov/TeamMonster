using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Menu
{
    public struct MenuChecker
    {
        public int openCount;

        public bool AllClosed
        {
            get
            {
                if (this.openCount == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void OnOpen(object menuItem = null, EventArgs e = null)
        {
            openCount += 1;
        }

        public void OnClose(object menuItem = null, EventArgs e = null)
        {
            openCount -= 1;
        }
    }
}
