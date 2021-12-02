﻿using Pandaros.WoWLogParser.Parser.FightMonitor;
using Pandaros.WoWLogParser.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pandaros.WoWLogParser.Parser.Calculators
{
    public class HealingTimelineCalculator : BaseCalculator
    {
        Dictionary<string, Dictionary<DateTime, HealingSnapshot>> _healingByPersonPer5Minutes = new Dictionary<string, Dictionary<DateTime, HealingSnapshot>>();

        public HealingTimelineCalculator(IPandaLogger logger, IStatsLogger reporter, ICombatState state, MonitoredFight fight) : base(logger, reporter, state, fight)
        {
            ApplicableEvents = new List<string>()
                {
                    LogEvents.SPELL_HEAL,
                    LogEvents.SPELL_PERIODIC_HEAL
                };
        }

        public override void CalculateEvent(ICombatEvent combatEvent)
        {
            var spell = (ISpell)combatEvent;

            if (combatEvent is ISpellHeal heal)
            {
                var owner = string.Empty;

                if (combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Player)
                {
                    owner = combatEvent.SourceName;
                }
                else 
                {
                    State.TryGetSourceOwnerName(combatEvent, out owner);
                }

                if (!string.IsNullOrEmpty(owner))
                    AddEvent(owner, combatEvent.Timestamp, spell.SpellName, heal.HealAmount + heal.Overhealing);
            }
            else if (combatEvent is IDamage damage &&
                combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Npc &&
                combatEvent.DestFlags.FlagType == UnitFlags.UnitFlagType.Player &&
                damage.Absorbed > 0 &&
                State.PlayerBuffs.TryGetValue(combatEvent.DestName, out var buffs))
            {
                Dictionary<string, string> sheilds = new Dictionary<string, string>();
                foreach (var shield in ShieldCalculator._shieldNames)
                {
                    if (buffs.TryGetValue(shield, out var caster))
                    {
                        sheilds[shield] = caster;
                    }
                }

                if (sheilds.Count != 0)
                {
                    float dmg = damage.Absorbed / sheilds.Count;
                    var resolved = Convert.ToInt32(Math.Round(dmg));

                    foreach (var s in sheilds)
                    {
                        AddEvent(s.Value, combatEvent.Timestamp, s.Key, resolved);
                    }
                }
            }
        }

        public override void FinalizeFight(ICombatEvent combatEvent)
        {
            
        }

        public override void StartFight(ICombatEvent combatEvent)
        {

        }

        private void AddEvent(string name, DateTime eventTime, string spellName, long healAmount)
        {
            if (!_healingByPersonPer5Minutes.TryGetValue(name, out var timeSeries))
            {
                timeSeries = new Dictionary<DateTime, HealingSnapshot>();
                timeSeries[eventTime] = new HealingSnapshot()
                {
                    StartTime = eventTime
                };
                _healingByPersonPer5Minutes[name] = timeSeries;
            }
            var healingBlock = timeSeries.Last();
            var snapshot = healingBlock.Value;

            if (eventTime - healingBlock.Key > TimeSpan.FromSeconds(10))
            {
                snapshot = new HealingSnapshot()
                {
                    StartTime = eventTime
                };

                timeSeries[eventTime] = snapshot;
            }

            snapshot.HealingDone.Add(Tuple.Create(spellName, healAmount));
        }
    }
}