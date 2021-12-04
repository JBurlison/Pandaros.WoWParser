using MongoDB.Driver;
using Pandaros.WoWParser.Parser.Calculators;
using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using Pandaros.WoWParser.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser
{
    public class AllCombatsState : CombatStateBase
    {
        MonitoredFight _allFights = new MonitoredFight()
        {
            BossName = "All Fights in Log"
        };

        public AllCombatsState(IFightMonitorFactory fightMonitorFactory, IPandaLogger logger, IStatsLogger reporter, IMongoClient mongoClient) : base(fightMonitorFactory, logger)
        {
            CurrentFight = _allFights;
            CalculatorFactory = new CalculatorFactory(logger, reporter, this, _allFights, mongoClient);
            CalculatorFactory.StartFight(new CombatEventBase());
        }

        public override void ProcessCombatEvent(ICombatEvent combatEvent, string evtStr)
        {
            base.ProcessCombatEvent(combatEvent, evtStr);

            if (combatEvent != null)
            {
                ProcessCombatEventInternal(combatEvent);
                CalculatorFactory.CalculateEvent(combatEvent);
            }
        }

        public override void ParseComplete()
        {
            CalculatorFactory.FinalizeFight(new CombatEventBase());
            base.ParseComplete();
        }
    }
}
