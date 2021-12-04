using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public class FightStartCalculator : BaseCalculator
    {
        string initiator;

        public FightStartCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = FightMonitorFactory.CombatEventsTriggerInFight;
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (!string.IsNullOrEmpty(initiator) || (!combatEvent.DestFlags.IsNPC && !combatEvent.SourceFlags.IsNPC))
                return;

            if (combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Player)
                initiator = combatEvent.SourceName;
            else if (combatEvent.DestFlags.FlagType == UnitFlags.UnitFlagType.Player)
                initiator = combatEvent.DestName;
            else if(State.TryGetSourceOwnerName(combatEvent, out var owner))
                initiator = owner;
            else if (State.TryGetDestOwnerName(combatEvent, out owner))
                initiator = owner;
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _logger.Log("---------------------------------------------");
            _logger.Log($"Person who started the fight for {Fight.BossName}");
            _logger.Log("---------------------------------------------");
            _logger.Log("~~~~~~~" + initiator + "~~~~~~~");

        }

        public override void StartFight(ICombatEvent combatEvent)
        {
            
        }
    }
}
