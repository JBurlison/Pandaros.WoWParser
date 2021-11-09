using PandarosWoWLogParser.FightMonitor;
using PandarosWoWLogParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PandarosWoWLogParser.Calculators
{
    public class HealingDoneCalculator : BaseCalculator
    {
        Dictionary<string, long> _healingDoneByPlayersTotal = new Dictionary<string, long>();
        Dictionary<string, long> _overHealingDoneByPlayersTotal = new Dictionary<string, long>();
        Dictionary<string, long> _castCount = new Dictionary<string, long>();
        Dictionary<string, long> _critCount = new Dictionary<string, long>();
        Dictionary<string, long> _critHeal = new Dictionary<string, long>();
        Dictionary<string, long> _critOverheal = new Dictionary<string, long>();
        Dictionary<string, long> _noncritHeal = new Dictionary<string, long>();
        Dictionary<string, long> _noncritOverheal = new Dictionary<string, long>();
        Dictionary<string, long> _periodicHeal = new Dictionary<string, long>();
        Dictionary<string, long> _periodicOverheal = new Dictionary<string, long>();
        Dictionary<string, Dictionary<string, long>> _playerOwnedHealing = new Dictionary<string, Dictionary<string, long>>();
        Dictionary<string, Dictionary<string, long>> _playerOwnedOverheaing = new Dictionary<string, Dictionary<string, long>>();

        public HealingDoneCalculator(IPandaLogger logger, IStatsReporter reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_HEAL,
                LogEvents.SPELL_PERIODIC_HEAL
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            var damage = (ISpellHeal)combatEvent;

            if (combatEvent.SourceFlags.GetFlagType == UnitFlags.FlagType.Player)
            {
                _healingDoneByPlayersTotal.AddValue(combatEvent.SourceName, damage.HealAmount);
                _overHealingDoneByPlayersTotal.AddValue(combatEvent.SourceName, damage.Overhealing);

                if (combatEvent.EventName == LogEvents.SPELL_HEAL)
                {
                    _castCount.AddValue(combatEvent.SourceName, 1);

                    if (damage.Critical)
                    {
                        _critCount.AddValue(combatEvent.SourceName, 1);
                        _critHeal.AddValue(combatEvent.SourceName, damage.HealAmount);
                        _critOverheal.AddValue(combatEvent.SourceName, damage.Overhealing);
                    }
                    else
                    {
                        _noncritHeal.AddValue(combatEvent.SourceName, damage.HealAmount);
                        _noncritOverheal.AddValue(combatEvent.SourceName, damage.Overhealing);
                    }
                }
                else
                {
                    _periodicHeal.AddValue(combatEvent.SourceName, damage.HealAmount);
                    _periodicOverheal.AddValue(combatEvent.SourceName, damage.Overhealing);
                }
            }

            if (State.TryGetSourceOwnerName(combatEvent, out var owner))
            {
                _healingDoneByPlayersTotal.AddValue(owner, damage.HealAmount);
                _overHealingDoneByPlayersTotal.AddValue(owner, damage.Overhealing);
                _playerOwnedHealing.AddValue(owner, combatEvent.SourceName, damage.HealAmount);
                _playerOwnedOverheaing.AddValue(owner, combatEvent.SourceName, damage.Overhealing);
            }

      
        }

        public override void FinalizeFight()
        {
            Dictionary<string, long> totalLife = new Dictionary<string, long>();
            Dictionary<string, long> effectiveHeal = new Dictionary<string, long>();
            Dictionary<string, long> critChance = new Dictionary<string, long>();

            var shieldCalculator = (ShieldCalculator)State.CalculatorFactory.CalculatorFlatList.First(c => c.GetType() == typeof(ShieldCalculator));

            foreach (var kvp in _healingDoneByPlayersTotal)
            {
                totalLife.AddValue(kvp.Key, kvp.Value);
                effectiveHeal.AddValue(kvp.Key, kvp.Value);
            }

            foreach (var kvp in _overHealingDoneByPlayersTotal)
                totalLife.AddValue(kvp.Key, kvp.Value);


            foreach (var kvp in shieldCalculator._shieldGivenDoneByPlayersTotal)
                foreach (var v in kvp.Value)
                {
                    totalLife.AddValue(kvp.Key, v.Value);
                    effectiveHeal.AddValue(kvp.Key, v.Value);
                }

            foreach (var crit in _critCount)
                if (_castCount.TryGetValue(crit.Key, out var castCount))
                {
                    critChance[crit.Key] = (crit.Value / castCount);
                }

            _statsReporting.Report(_healingDoneByPlayersTotal, "Life Healed Rankings", Fight, State);
            _statsReporting.Report(effectiveHeal, "Effective Healing Rankings (healed + Shield Absorbs)", Fight, State);
            _statsReporting.Report(_overHealingDoneByPlayersTotal, "Overheal Rankings", Fight, State);
            _statsReporting.Report(_critHeal, "Critical Healed Rankings", Fight, State);
            _statsReporting.Report(_noncritHeal, "Non-Critical Healed Rankings", Fight, State);
            _statsReporting.Report(_critOverheal, "Critical Overheal Rankings", Fight, State);
            _statsReporting.Report(_periodicHeal, "Periodic Healed Rankings", Fight, State);
            _statsReporting.Report(_periodicHeal, "Periodic Overheal Rankings", Fight, State);
            _statsReporting.Report(critChance, "Healing Crit Chance", Fight, State);

            _statsReporting.ReportPerSecondNumbers(_healingDoneByPlayersTotal, "Life Healed HPS Rankings", Fight, State);

            _statsReporting.Report(totalLife, "Healing Output Rankings (Life Healed + Overheal + Shields)", Fight, State);
            _statsReporting.ReportPerSecondNumbers(totalLife, "HPS Rankings (Life Healed + Overheal + Shields)", Fight, State);
            _statsReporting.ReportPerSecondNumbers(effectiveHeal, "Effective HPS Rankings (Life Healed + Shields)", Fight, State);
        }

        public override void StartFight()
        {

        }
    }
}
