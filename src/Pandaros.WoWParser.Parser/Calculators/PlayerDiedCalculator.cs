using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public class PlayerDiedCalculator : BaseCalculator
    {
        Dictionary<string, long> _playerDeaths = new Dictionary<string, long>();

        public PlayerDiedCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.UNIT_DIED
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            if (combatEvent.DestFlags.FlagType == UnitFlags.UnitFlagType.Player)
            {
                _playerDeaths.AddValue(combatEvent.DestName, 1);
            }
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            _statsReporting.Report(_playerDeaths, "Player Deaths", Fight, State);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }
    }
}
