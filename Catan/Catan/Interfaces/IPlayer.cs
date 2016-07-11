using System.Collections.Generic;
using Catan.DevelopmentCards;
using Catan.GameObjects;
using Microsoft.Xna.Framework;
using Catan.Common;
using System;

namespace Catan.Interfaces
{
    public interface IPlayer
    {
        event EventHandler WinPointsReached;
        Color Color { get; set; }
        ICollection<IDevelopmentCard> DevCardsPossedsed { get; }
        IEnumerable<Harbour> Harbours { get; }
        byte Id { get; }
        int Points { get; }
        ICollection<Town> Towns { get; }
        IEnumerable<Unit> Units { get; }
        string UserName { get; set; }
        ICollection<Village> Villages { get; }
        ICollection<LineObject> LineObjects { get; }
        IEnumerable<Settlement> Settlements { get; }

        void AddPoints(int points);
        uint GetResourceValue(ResourceType resource);
        void RemovePoints(int points);
        void SetResourceValue(ResourceType resource, uint value);
        void AddResourceValue(ResourceType resource, int value);
    }
}