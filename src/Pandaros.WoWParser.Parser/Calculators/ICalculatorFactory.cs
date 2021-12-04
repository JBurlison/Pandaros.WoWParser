using System;
using System.Collections.Generic;
using System.Text;
using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;

namespace Pandaros.WoWParser.Parser.Calculators
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
