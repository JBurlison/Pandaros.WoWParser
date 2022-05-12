using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public class SpellsCastCalculator : BaseCalculator
    {
        Dictionary<string, Dictionary<string, long>> _spellsCast = new Dictionary<string, Dictionary<string, long>>();

        public SpellsCastCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_CAST_SUCCESS,
                LogEvents.SPELL_SUMMON
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.SourceFlags.FlagType != UnitFlags.UnitFlagType.Player)
                return;
            var spell = (ISpell)combatEvent;

            _spellsCast.AddValue(combatEvent.SourceName, spell.SpellName, 1);
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _statsReporting.Report(_spellsCast, "Spells Cast", Fight, State, true);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {
            
        }
    }
}
