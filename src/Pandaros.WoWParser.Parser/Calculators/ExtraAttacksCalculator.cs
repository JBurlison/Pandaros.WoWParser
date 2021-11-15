using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public class ExtraAttacksCalculator : BaseCalculator
    {
        Dictionary<string, Dictionary<string, long>> _extraAttackCount = new Dictionary<string, Dictionary<string, long>>();

        public ExtraAttacksCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_EXTRA_ATTACKS
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.SourceFlags.FlagType != UnitFlags.UnitFlagType.Player)
                return;

            var spell = (SpellExtraAttacks)combatEvent;

            _extraAttackCount.AddValue(combatEvent.SourceName, spell.SpellName, spell.Amount);
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _statsReporting.Report(_extraAttackCount, "Extra Attacks", Fight, State);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {
            
        }
    }
}
