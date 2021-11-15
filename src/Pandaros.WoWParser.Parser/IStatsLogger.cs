﻿using Pandaros.WoWLogParser.Parser.FightMonitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWLogParser.Parser
{
    public interface IStatsLogger
    {
        public void ReportPerSecondNumbers<T>(Dictionary<T, long> stats, string name, MonitoredFight fight, ICombatState state);
        public void Report<T>(Dictionary<T, long> stats, string name, MonitoredFight fight, ICombatState state);
        public void Report<T, G>(Dictionary<T, Dictionary<G, long>> stats, string name, MonitoredFight fight, ICombatState state);
        public void Report<T, G, B>(Dictionary<T, Dictionary<G, Dictionary<B, long>>> stats, string name, MonitoredFight fight, ICombatState state);
        public void ReportTable(List<List<string>> table, string name, MonitoredFight fight, ICombatState state, List<int> length = default(List<int>));
    }
}