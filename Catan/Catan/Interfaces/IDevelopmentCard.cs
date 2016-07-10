using Catan.Common;

namespace Catan.Interfaces
{
    public interface IDevelopmentCard
    {
        bool IsPlayed { get; set; }
        Player Owner { get; set; }

        void Activate();
    }
}