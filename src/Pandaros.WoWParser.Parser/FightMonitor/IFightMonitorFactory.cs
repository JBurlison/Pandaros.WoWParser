using Pandaros.WoWParser.Parser.Calculators;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;

namespace Pandaros.WoWParser.Parser.FightMonitor
{
    public interface IFightMonitorFactory
    {
        bool IsMonitoredFight(ICombatEvent evnt, ICombatState state);
    }
}