using Pandaros.WoWLogParser.Parser.Calculators;
using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using Pandaros.WoWLogParser.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser
{
    public class CombatState : CombatStateBase
    {
        public CombatState(IFightMonitorFactory fightMonitorFactory, IPandaLogger logger) : base(fightMonitorFactory, logger)
        {
            
        }

        public override void ProcessCombatEvent(ICombatEvent combatEvent, string evtStr)
        {
            base.ProcessCombatEvent(combatEvent, evtStr);

            if (combatEvent != null)
            {
                if (_fightMonitorFactory.IsMonitoredFight(combatEvent, this))
                    _prevFightState = true;
                else if (_prevFightState)
                {
                    CalculatorFactory.StartFight(combatEvent);

                    foreach (var fightEvent in CurrentFight.MonitoredFightEvents)
                    {
                        ProcessCombatEventInternal(fightEvent);
                        CalculatorFactory.CalculateEvent(fightEvent);
                    }

                    CalculatorFactory.FinalizeFight(combatEvent);

                    _prevFightState = false;
                    CalculatorFactory = null;
                    CurrentFight = null;
                }

            }
        }
    }
}
