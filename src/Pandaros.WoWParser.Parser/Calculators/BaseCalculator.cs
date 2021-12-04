using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public abstract class BaseCalculator : ICalculator
    {
        public List<string> ApplicableEvents { get; set; }
        public ICombatState State { get; set; }
        public MonitoredFight Fight { get; set; }

        internal IPandaLogger _logger;
        internal IStatsLogger _statsReporting;

        public BaseCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight)
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
