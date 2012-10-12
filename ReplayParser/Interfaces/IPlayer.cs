using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplayParser.Entities;

namespace ReplayParser.Interfaces
{
    public interface IPlayer
    {

        ColourType ColourType { get; }
        byte ForceIdentifier { get; }
        int Identifier { get; }
        String Name { get; }
        RaceType RaceType { get; }
        SlotType SlotType { get; }
        byte Spot { get; }
        PlayerType PlayerType { get; }

    }
}
