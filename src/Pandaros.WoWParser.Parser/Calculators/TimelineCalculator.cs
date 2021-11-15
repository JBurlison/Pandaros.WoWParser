using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public class TimelineCalculator : BaseCalculator
    {
        public TimelineCalculator(IPandaLogger logger, IStatsReporter reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_DAMAGE,
                LogEvents.RANGE_DAMAGE,
                LogEvents.SWING_DAMAGE,
                LogEvents.SPELL_PERIODIC_DAMAGE,
                LogEvents.DAMAGE_SHIELD,
                LogEvents.SPELL_HEAL,
                LogEvents.SPELL_PERIODIC_HEAL,
                LogEvents.SPELL_DISPEL,
                LogEvents.SWING_MISSED,
                LogEvents.RANGE_MISSED,
                LogEvents.SPELL_MISSED,
                LogEvents.DAMAGE_SHIELD_MISSED,
                LogEvents.ENVIRONMENTAL_DAMAGE,
                LogEvents.SPELL_ABSORBED,
                LogEvents.SPELL_CAST_FAILED,
                LogEvents.SPELL_DISPEL_FAILED,
                LogEvents.SPELL_EXTRA_ATTACKS,
                LogEvents.SPELL_INTERRUPT,
                LogEvents.SPELL_LEECH,
                LogEvents.SPELL_PERIODIC_ENERGIZE,
                LogEvents.SPELL_PERIODIC_LEECH,
                LogEvents.SPELL_PERIODIC_MISSED,
                LogEvents.SPELL_RESURRECT,
                LogEvents.SPELL_SUMMON,
                LogEvents.UNIT_DESTROYED,
                LogEvents.UNIT_DIED
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }
    }
}
