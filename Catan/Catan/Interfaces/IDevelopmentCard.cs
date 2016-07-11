using Catan.Common;

namespace Catan.Interfaces
{
    public interface IDevelopmentCard
    {
        bool IsPlayed { get; set; }
        IPlayer Owner { get; set; }

        void Activate();
    }
}