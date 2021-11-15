using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public abstract class BaseCalculator : ICalculator
    {
        public List<string> ApplicableEvents { get; set; }
        public ICombatState State { get; set; }
        public MonitoredFight Fight { get; set; }

        internal IPandaLogger _logger;
        internal IStatsReporter _statsReporting;

        public BaseCalculator(IPandaLogger logger, IStatsReporter reporter, ICombatState state, MonitoredFight fight)
        {
            _logger = logger;
            _statsReporting = reporter;
            State = state;
            Fight = fight;
        }

        public virtual void CalculateEvent(ICombatEvent combatEvent)
        {
            
        }

        public virtual void FinalizeFight(ICombatEvent combatEvent)
        {
            
        }

        public virtual void StartFight(ICombatEvent combatEvent)
        {
            
        }
    }
}
