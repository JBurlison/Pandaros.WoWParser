using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pandaros.WoWParser.Parser.Calculators
{
    public class PlayerDiedCalculator : BaseCalculator
    {
        Dictionary<string, long> _playerDeaths = new Dictionary<string, long>();
        Dictionary<string, long> _feignCasts = new Dictionary<string, long>();

        public PlayerDiedCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.UNIT_DIED,
                LogEvents.SPELL_CAST_SUCCESS
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.EventName == LogEvents.UNIT_DIED && combatEvent.DestFlags.FlagType == UnitFlags.UnitFlagType.Player)
            {
                _playerDeaths.AddValue(combatEvent.DestName, 1);
            }
            else if (combatEvent.EventName == LogEvents.SPELL_CAST_SUCCESS && combatEvent is ISpell spell && spell.SpellName == "Feign Death")
            {
                _feignCasts.AddValue(combatEvent.SourceName, 1);
            }
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            foreach (var p in _feignCasts)
                if (_playerDeaths.ContainsKey(p.Key))
                {
                    _playerDeaths[p.Key] = _playerDeaths[p.Key] - p.Value;

                    if (_playerDeaths[p.Key] <= 0)
                        _playerDeaths.Remove(p.Key);
                }

            _statsReporting.Report(_playerDeaths, "Player Deaths", Fight, State, true);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }
    }
}
