﻿using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public class DamageAbsorbedCalculator : BaseCalculator
    {
        Dictionary<string, long> _damageAbsorbedByPlayersTotal = new Dictionary<string, long>();

        public DamageAbsorbedCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_DAMAGE,
                LogEvents.RANGE_DAMAGE,
                LogEvents.SWING_DAMAGE,
                LogEvents.SPELL_PERIODIC_DAMAGE
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.DestFlags.FlagType != UnitFlags.UnitFlagType.Player)
                return;

            var damage = (IDamage)combatEvent;

            if (damage.Absorbed != 0)
                _damageAbsorbedByPlayersTotal.AddValue(combatEvent.DestName, damage.Absorbed);
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _statsReporting.Report(_damageAbsorbedByPlayersTotal, "Absorbtion Rankings", Fight, State);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {
            
        }
    }
}
