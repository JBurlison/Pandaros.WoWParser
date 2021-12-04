using Pandaros.WoWParser.Parser.Calculators;
using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using Pandaros.WoWParser.Parser.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser
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
