using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Pandaros.WoWParser.Parser.Calculators
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
                LogEvents.SPELL_PERIODIC_DAMAGE,
                LogEvents.SWING_MISSED,
                LogEvents.SPELL_MISSED,
                LogEvents.SPELL_ABSORBED
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.DestFlags.FlagType != UnitFlags.UnitFlagType.Player)
                return;

            if (combatEvent is IDamage damage && damage.Absorbed != 0)
                _damageAbsorbedByPlayersTotal.AddValue(combatEvent.DestName, damage.Absorbed);
            else if (combatEvent is IMissed missed && missed.Absorbed != 0)
                _damageAbsorbedByPlayersTotal.AddValue(combatEvent.DestName, missed.Absorbed);
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
