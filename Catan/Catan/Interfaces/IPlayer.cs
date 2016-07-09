﻿using System.Collections.Generic;
using Catan.DevelopmentCards;
using Catan.GameObjects;
using Microsoft.Xna.Framework;
using Catan.Common;

namespace Catan.Interfaces
{
    public interface IPlayer
    {
        Color Color { get; set; }
        IList<DevelopmentCard> DevCardsPossedsed { get; set; }
        IList<Harbour> Harbours { get; set; }
        byte Id { get; }
        int Points { get; }
        IList<Town> Towns { get; set; }
        IList<Unit> Units { get; set; }
        string UserName { get; set; }
        IList<Village> Villages { get; set; }
        IList<LineObject> LineObjects { get; set; }
        IList<Settlement> Settlements { get; set; }

        void AddPoints(int points);
        uint GetResourceValue(ResourceType resource);
        void RemovePoints(int points);
        void SetResourceValue(ResourceType resource, uint value);
    }
}