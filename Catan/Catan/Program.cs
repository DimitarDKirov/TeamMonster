using Catan.Common;
using System;

namespace Catan
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = GameClass.Game)
                game.Run();
        }
    }
#endif
}
