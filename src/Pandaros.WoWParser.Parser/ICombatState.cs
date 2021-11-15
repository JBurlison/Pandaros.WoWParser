using Pandaros.WoWLogParser.Parser.Calculators;
using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System.Collections.Generic;

namespace Pandaros.WoWLogParser.Parser
{
    public interface ICombatState
    {
        CalculatorFactory CalculatorFactory { get; set; }
        MonitoredFight CurrentFight { get; set; }
        Dictionary<string, string> EntitytoOwnerMap { get; set; }
        bool InFight { get; set; }
        Dictionary<string, List<string>> OwnerToEntityMap { get; set; }
        Dictionary<string, Dictionary<string, string>> PlayerBuffs { get; set; }
        Dictionary<string, Dictionary<string, string>> PlayerDebuffs { get; set; }

        void ParseComplete();
        void ProcessCombatEvent(ICombatEvent combatEvent, string evtStr);
        bool TryGetSourceOwnerName(ICombatEvent combatEvent, out string owner);
        bool TryGetDestOwnerName(ICombatEvent combatEvent, out string owner);
    }
}