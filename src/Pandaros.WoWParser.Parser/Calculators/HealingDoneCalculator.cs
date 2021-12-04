using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pandaros.WoWParser.Parser.Calculators
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
        Dictionary<string, Dictionary<string, Dictionary<string, long>>> _playerHealed = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();
        Dictionary<string, Dictionary<string, Dictionary<string, long>>> _playerOverHealed = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();

        public HealingDoneCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
            {
                LogEvents.SPELL_HEAL,
                LogEvents.SPELL_PERIODIC_HEAL
            };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            var healingEvent = (ISpellHeal)combatEvent;
            var spell = (ISpell)combatEvent;

            if (combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Player)
            {
                _healingDoneByPlayersTotal.AddValue(combatEvent.SourceName, healingEvent.HealAmount);
                _overHealingDoneByPlayersTotal.AddValue(combatEvent.SourceName, healingEvent.Overhealing);

                if (combatEvent.DestFlags.IsPlayer)
                {
                    _playerHealed.AddValue(combatEvent.SourceName, combatEvent.DestName, spell.SpellName, healingEvent.HealAmount);
                    _playerOverHealed.AddValue(combatEvent.SourceName, combatEvent.DestName, spell.SpellName, healingEvent.Overhealing);
                }

                if (combatEvent.EventName == LogEvents.SPELL_HEAL)
                {
                    _castCount.AddValue(combatEvent.SourceName, 1);

                    if (healingEvent.Critical)
                    {
                        _critCount.AddValue(combatEvent.SourceName, 1);
                        _critHeal.AddValue(combatEvent.SourceName, healingEvent.HealAmount);
                        _critOverheal.AddValue(combatEvent.SourceName, healingEvent.Overhealing);
                    }
                    else
                    {
                        _noncritHeal.AddValue(combatEvent.SourceName, healingEvent.HealAmount);
                        _noncritOverheal.AddValue(combatEvent.SourceName, healingEvent.Overhealing);
                    }
                }
                else
                {
                    _periodicHeal.AddValue(combatEvent.SourceName, healingEvent.HealAmount);
                    _periodicOverheal.AddValue(combatEvent.SourceName, healingEvent.Overhealing);
                }
            }

            if (State.TryGetSourceOwnerName(combatEvent, out var owner))
            {
                _healingDoneByPlayersTotal.AddValue(owner, healingEvent.HealAmount);
                _overHealingDoneByPlayersTotal.AddValue(owner, healingEvent.Overhealing);
                _playerOwnedHealing.AddValue(owner, combatEvent.SourceName, healingEvent.HealAmount);
                _playerOwnedOverheaing.AddValue(owner, combatEvent.SourceName, healingEvent.Overhealing);
            }

      
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            Dictionary<string, long> totalLife = new Dictionary<string, long>();
            Dictionary<string, long> effectiveHeal = new Dictionary<string, long>();
            Dictionary<string, long> critChance = new Dictionary<string, long>();
            Dictionary<string, Dictionary<string, Dictionary<string, long>>> totalHealedPerson = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();
            Dictionary<string, Dictionary<string, Dictionary<string, long>>> effectiveHealingPerperson = new Dictionary<string, Dictionary<string, Dictionary<string, long>>>();

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
                    critChance[crit.Key] = Convert.ToInt32(Math.Round(((double)crit.Value / (double)castCount) * 100));
                }

            foreach (var healed in _playerHealed)
                foreach (var person in healed.Value)
                    foreach (var spell in person.Value)
                        totalHealedPerson.AddValue(healed.Key, person.Key, spell.Key, spell.Value);

            foreach (var healed in _playerOverHealed)
                foreach (var person in healed.Value)
                    foreach (var spell in person.Value)
                        totalHealedPerson.AddValue(healed.Key, person.Key, spell.Key, spell.Value);

            foreach (var healed in shieldCalculator._playerSHieldedTotal)
                foreach (var person in healed.Value)
                    foreach (var spell in person.Value)
                        totalHealedPerson.AddValue(healed.Key, person.Key, spell.Key, spell.Value);

            foreach (var healed in _playerHealed)
                foreach (var person in healed.Value)
                    foreach (var spell in person.Value)
                        effectiveHealingPerperson.AddValue(healed.Key, person.Key, spell.Key, spell.Value);

            foreach (var healed in shieldCalculator._playerSHieldedTotal)
                foreach (var person in healed.Value)
                    foreach (var spell in person.Value)
                        effectiveHealingPerperson.AddValue(healed.Key, person.Key, spell.Key, spell.Value);

            _statsReporting.Report(_healingDoneByPlayersTotal, "Life Healed Rankings", Fight, State);
            _statsReporting.Report(effectiveHeal, "Effective Healing Rankings (healed + Shield Absorbs)", Fight, State);
            _statsReporting.Report(_overHealingDoneByPlayersTotal, "Overheal Rankings", Fight, State);
            _statsReporting.Report(_critHeal, "Critical Healed Rankings", Fight, State);
            _statsReporting.Report(_noncritHeal, "Non-Critical Healed Rankings", Fight, State);
            _statsReporting.Report(_critOverheal, "Critical Overheal Rankings", Fight, State);
            _statsReporting.Report(_periodicHeal, "Periodic Healed Rankings", Fight, State);
            _statsReporting.Report(_periodicHeal, "Periodic Overheal Rankings", Fight, State);
            _statsReporting.Report(critChance, "Healing Crit Chance", Fight, State);
            _statsReporting.Report(_playerHealed, "Most Healed on Players Rankings", Fight, State);
            _statsReporting.Report(_playerOverHealed, "Most Overhealed on Players Rankings", Fight, State);
            _statsReporting.Report(effectiveHealingPerperson, "Most Effective Healing on Players Rankings (Life Healed + Shields)", Fight, State);
            _statsReporting.Report(totalHealedPerson, "Most Healing Output on Players Rankings (Life Healed + Overheal + Shields)", Fight, State);

            _statsReporting.ReportPerSecondNumbers(_healingDoneByPlayersTotal, "Life Healed HPS Rankings", Fight, State);

            _statsReporting.Report(totalLife, "Healing Output Rankings (Life Healed + Overheal + Shields)", Fight, State);
            _statsReporting.ReportPerSecondNumbers(totalLife, "HPS Rankings (Life Healed + Overheal + Shields)", Fight, State);
            _statsReporting.ReportPerSecondNumbers(effectiveHeal, "Effective HPS Rankings (Life Healed + Shields)", Fight, State);
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }
    }
}
