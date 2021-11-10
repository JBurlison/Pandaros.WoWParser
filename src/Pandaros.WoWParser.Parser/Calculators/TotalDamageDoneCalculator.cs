using PandarosWoWLogParser.FightMonitor;
using PandarosWoWLogParser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace PandarosWoWLogParser.Calculators
{
    public class TotalDamageDoneCalculator : BaseCalculator
    {
        Dictionary<string, long> _damageDoneByPlayersTotal = new Dictionary<string, long>();
        Dictionary<string, long> _damageCount = new Dictionary<string, long>();
        Dictionary<string, long> _critCount = new Dictionary<string, long>();
        Dictionary<string, long> _periodicDamage = new Dictionary<string, long>();
        Dictionary<string, long> _critDamage = new Dictionary<string, long>();
        Dictionary<string, long> _noncritDamage = new Dictionary<string, long>();
        Dictionary<string, Dictionary<string, long>> _playerOwnedDamage = new Dictionary<string, Dictionary<string, long>>();

        public TotalDamageDoneCalculator(IPandaLogger logger, IStatsReporter reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_DAMAGE,
                LogEvents.RANGE_DAMAGE,
                LogEvents.SWING_DAMAGE,
                LogEvents.SPELL_PERIODIC_DAMAGE,
                LogEvents.DAMAGE_SHIELD
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (!combatEvent.SourceFlags.IsPlayerPet && !combatEvent.SourceFlags.IsPlayer)
                return;

            var damage = (IDamage)combatEvent;

            _damageDoneByPlayersTotal.AddValue(combatEvent.SourceName, damage.Damage);

            // check if its a pet.
            if (State.TryGetSourceOwnerName(combatEvent, out var owner))
            {
                _damageDoneByPlayersTotal.AddValue(owner, damage.Damage);
                _playerOwnedDamage.AddValue(owner, combatEvent.SourceName, damage.Damage);
            }
            else
            {
                if (combatEvent.EventName == LogEvents.SPELL_PERIODIC_DAMAGE)
                {
                    _periodicDamage.AddValue(combatEvent.SourceName, damage.Damage);
                }
                else
                {
                    _damageCount.AddValue(combatEvent.SourceName, 1);

                    if (damage.Critical)
                    {
                        _critCount.AddValue(combatEvent.SourceName, 1);
                        _critDamage.AddValue(combatEvent.SourceName, damage.Damage);
                    }
                    else
                        _noncritDamage.AddValue(combatEvent.SourceName, damage.Damage);

                }
            }
        }

        public override void FinalizeFight()
        {
            Dictionary<string, long> critChance = new Dictionary<string, long>();

            foreach (var crit in _critCount)
                if (_damageCount.TryGetValue(crit.Key, out var castCount))
                {
                    critChance[crit.Key] = Convert.ToInt32(Math.Round(((double)crit.Value / (double)castCount) * 100));
                }

            _statsReporting.Report(_damageDoneByPlayersTotal, "Damage Rankings", Fight, State);
            _statsReporting.Report(_playerOwnedDamage, "Player Owned Damage Rankings", Fight, State);
            _statsReporting.Report(_critDamage, "Crit Damage Rankings", Fight, State);
            _statsReporting.Report(_noncritDamage, "Non Crit Damage Rankings", Fight, State);
            _statsReporting.Report(_periodicDamage, "Periodic Damage Rankings", Fight, State);
            _statsReporting.Report(_critCount, "Crit Count Rankings", Fight, State);
            _statsReporting.Report(_damageCount, "Attack and Spell Count Rankings", Fight, State);
            _statsReporting.Report(critChance, "Attack and Spell Crit Chance Rankings", Fight, State);
            _statsReporting.ReportPerSecondNumbers(_damageDoneByPlayersTotal, "DPS Rankings", Fight, State);
        }

        public override void StartFight()
        {
            
        }
    }
}
