using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public class ShieldCalculator : BaseCalculator
    {
        internal Dictionary<string, Dictionary<string, long>> _shieldGivenDoneByPlayersTotal = new Dictionary<string, Dictionary<string, long>>();
        internal Dictionary<string, Dictionary<string, Dictionary<string, long>>> _playerSHieldedTotal = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();

        internal static List<string> _shieldNames = new List<string>()
        {
            "Ardent Defender",
            "Divine Aegis",
            "Mana Shield",
            "Fire Ward",
            "Frost Ward",
            "Sacred Shield",
            "Stoneclaw Totem",
            "Shadow Ward",
            "Ice Barrier",
            "Unfair Advantage",
            "Into Darkness",
            "Power Word: Shield",
            "Tidal Shield"
        };

        public ShieldCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_DAMAGE,
                LogEvents.RANGE_DAMAGE,
                LogEvents.SWING_DAMAGE,
                LogEvents.SPELL_PERIODIC_DAMAGE,
                LogEvents.DAMAGE_SHIELD,
                LogEvents.SPELL_MISSED,
                LogEvents.SWING_MISSED
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            var absorbed = 0;

            if (combatEvent is IDamage damage && damage.Absorbed != 0)
                absorbed = damage.Absorbed;
            else if (combatEvent is IMissed missed && missed.Absorbed != 0)
                absorbed = missed.Absorbed;

            if (combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Npc && 
                combatEvent.DestFlags.FlagType == UnitFlags.UnitFlagType.Player &&
                absorbed > 0 && 
                State.PlayerBuffs.TryGetValue(combatEvent.DestName, out var buffs))
            {
                string activeShield = String.Empty;
                string sheildCaster = String.Empty;
                foreach (var shield in _shieldNames)
                {
                    if (buffs.TryGetValue(shield, out var caster))
                    {
                        activeShield = shield;
                        sheildCaster = caster;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(activeShield))
                {
                    _shieldGivenDoneByPlayersTotal.AddValue(sheildCaster, activeShield, absorbed);
                    _playerSHieldedTotal.AddValue(sheildCaster, combatEvent.DestName, activeShield, absorbed);
           
                }
            }
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _statsReporting.Report(_shieldGivenDoneByPlayersTotal, "Damage Prevented with Shields (absorb) by caster", Fight, State);
            _statsReporting.Report(_playerSHieldedTotal, "Shields cast on players", Fight, State);

            Dictionary<string, Dictionary<string, long>> avgShield = new Dictionary<string, Dictionary<string, long>>();

            foreach (var p in _shieldGivenDoneByPlayersTotal)
                foreach (var s in p.Value)
                {
                    if (State.PlayerBuffCounts.TryGetValue(p.Key, out var dic2) && dic2.TryGetValue(s.Key, out var castCount))
                    {
                        avgShield.AddValue(p.Key, s.Key, s.Value / castCount);
                    }
                }

            _statsReporting.Report(avgShield, "Average Shield", Fight, State);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }
    }
}
