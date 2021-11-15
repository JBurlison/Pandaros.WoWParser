using System;
using System.Collections.Generic;
using System.Text;
using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public interface ICalculatorFactory
    {
        public Dictionary<string, List<ICalculator>> Calculators { get; set; }
        public ICombatState State { get; set; }
        public MonitoredFight Fight { get; set; }
        public void CalculateEvent(ICombatEvent combatEvent);
        public void StartFight(ICombatEvent combatEvent);
        public void FinalizeFight(ICombatEvent combatEvent);
    }
}
