using Pandaros.WoWLogParser.Parser.Calculators;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;

namespace Pandaros.WoWLogParser.Parser.FightMonitor
{
    public interface IFightMonitorFactory
    {
        bool IsMonitoredFight(ICombatEvent evnt, ICombatState state);
    }
}