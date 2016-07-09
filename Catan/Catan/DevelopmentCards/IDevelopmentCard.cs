using Catan.Common;

namespace Catan.DevelopmentCards
{
    public interface IDevelopmentCard
    {
        bool IsPlayed { get; set; }
        Player Owner { get; set; }

        void Activate();
    }
}