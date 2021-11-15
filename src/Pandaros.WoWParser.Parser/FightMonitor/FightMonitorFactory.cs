using MongoDB.Driver;
using Pandaros.WoWLogParser.Parser.Calculators;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.FightMonitor
{
    public class FightMonitorFactory : IFightMonitorFactory
    {
        public static List<string> CombatEventsTriggerInFight { get; set; } = new List<string>()
        {
            LogEvents.SPELL_DAMAGE,
            LogEvents.SPELL_PERIODIC_DAMAGE,
            LogEvents.RANGE_MISSED,
            LogEvents.RANGE_DAMAGE,
            LogEvents.SPELL_MISSED,
            LogEvents.SWING_DAMAGE,
            LogEvents.SWING_MISSED,
            LogEvents.UNIT_DIED
        };

        IPandaLogger _logger;
        IStatsLogger _reporter;
        IMongoClient _mongoClient;

        public FightMonitorFactory(IPandaLogger logger, IStatsLogger reporter, IMongoClient mongoClient)
        {
            _reporter = reporter;
            _logger = logger;
            _mongoClient = mongoClient;
        }

        public bool IsMonitoredFight(ICombatEvent evnt, ICombatState state)
        {
            if (!state.InFight && CombatEventsTriggerInFight.Contains(evnt.EventName))
            {
                string npcName = string.Empty;
                string npcId = string.Empty;

                if (evnt.SourceFlags.Controller == UnitFlags.UnitController.Npc)
                {
                    npcName = evnt.SourceName;
                    npcId = evnt.SourceGuid;
                }
                else if (evnt.DestFlags.Controller == UnitFlags.UnitController.Npc)
                {
                    npcName = evnt.DestName;
                    npcId = evnt.DestGuid;
                }
                
                if (!string.IsNullOrEmpty(npcName))
                {
                    state.InFight = true;
                    state.CurrentFight = new MonitoredFight()
                    {
                        BossName = npcName,
                        FightStart = evnt.Timestamp,
                        MonsterID = new Dictionary<string, bool>() { { npcId, false } }
                    };
                    state.CalculatorFactory = new CalculatorFactory(_logger, _reporter, state, state.CurrentFight, _mongoClient);
                }
            }

            if (state.InFight)
            {
                state.InFight = state.CurrentFight.AddEvent(evnt, state);
            }

            return state.InFight;
        }
    }
}
