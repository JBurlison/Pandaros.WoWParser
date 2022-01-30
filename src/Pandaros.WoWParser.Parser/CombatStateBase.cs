using Pandaros.WoWParser.Parser.Calculators;
using Pandaros.WoWParser.Parser.FightMonitor;
using Pandaros.WoWParser.Parser.Models;
using System.Collections.Generic;

namespace Pandaros.WoWParser.Parser
{
    public abstract class CombatStateBase : ICombatState
    {
        internal IFightMonitorFactory _fightMonitorFactory;
        internal IPandaLogger _logger;
        internal Dictionary<string, int> _unknown = new Dictionary<string, int>();
        internal Dictionary<string, int> _eventCount = new Dictionary<string, int>();
        internal bool _prevFightState = false;
        private bool _disposedValue;

        public Dictionary<string, List<string>> OwnerToEntityMap { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, string> EntitytoOwnerMap { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, Dictionary<string, string>> PlayerBuffs { get; set; } = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> PlayerDebuffs { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public bool InFight { get; set; }
        public MonitoredFight CurrentFight { get; set; }
        public CalculatorFactory CalculatorFactory { get; set; }

        public CombatStateBase(IFightMonitorFactory fightMonitorFactory, IPandaLogger logger)
        {
            _fightMonitorFactory = fightMonitorFactory;
            _logger = logger;
        }

        public virtual void ParseComplete()
        {
            _logger.Log($"``````````````````````````````````````````````````````````````");
            _logger.Log($"Number of unknown events: {_unknown.Count}");
            _logger.Log($"--------------------------------------------------------------");
            foreach (var ev in _unknown)
                _logger.Log($"{ev.Key}: {ev.Value}");
            _logger.Log($"``````````````````````````````````````````````````````````````");

            _logger.Log($"Number of known events: {_eventCount.Count}");
            _logger.Log($"--------------------------------------------------------------");
            foreach (var ev in _eventCount)
                _logger.Log($"{ev.Key}: {ev.Value}");
            _logger.Log($"``````````````````````````````````````````````````````````````");
        }

        public virtual void ProcessCombatEvent(ICombatEvent combatEvent, string evtStr)
        {
            if (combatEvent == null)
            {
                if (!string.IsNullOrEmpty(evtStr))
                    _unknown.AddValue(evtStr, 1);
            }
            else
            {
                _eventCount.AddValue(combatEvent.EventName, 1);
            }
        }

        public virtual bool TryGetSourceOwnerName(ICombatEvent combatEvent, out string owner)
        {
            owner = null;
            return combatEvent.SourceFlags.Controller == UnitFlags.UnitController.Player &&
                EntitytoOwnerMap.TryGetValue(combatEvent.SourceGuid, out owner);
        }

        public virtual bool TryGetDestOwnerName(ICombatEvent combatEvent, out string owner)
        {
            owner = null;
            return combatEvent.DestFlags.Controller == UnitFlags.UnitController.Player &&
                EntitytoOwnerMap.TryGetValue(combatEvent.DestGuid, out owner);
        }

        internal virtual void ProcessCombatEventInternal(ICombatEvent combatEvent)
        {
            switch (combatEvent.EventName)
            {
                case LogEvents.SPELL_SUMMON:
                    if (combatEvent.SourceFlags.FlagType == UnitFlags.UnitFlagType.Player)
                    {
                        if (!OwnerToEntityMap.TryGetValue(combatEvent.SourceName, out var list))
                        {
                            list = new List<string>();
                            OwnerToEntityMap[combatEvent.SourceName] = list;
                        }

                        if (!list.Contains(combatEvent.DestGuid))
                            list.Add(combatEvent.DestGuid);

                        if (!EntitytoOwnerMap.ContainsKey(combatEvent.DestGuid))
                            EntitytoOwnerMap.Add(combatEvent.DestGuid, combatEvent.SourceName);
                    }
                    break;

                case LogEvents.UNIT_DIED:
                    if (EntitytoOwnerMap.TryGetValue(combatEvent.DestGuid, out var ownerId))
                        EntitytoOwnerMap.Remove(combatEvent.DestGuid);

                    if (OwnerToEntityMap.TryGetValue(combatEvent.SourceName, out var entities))
                        entities.Remove(combatEvent.DestGuid);

                    EntitytoOwnerMap.Remove(combatEvent.DestGuid);
                    break;

                case LogEvents.SPELL_AURA_APPLIED:
                case LogEvents.SPELL_AURA_APPLIED_DOSE:
                case LogEvents.SPELL_AURA_REFRESH:
                    var spell = (ISpell)combatEvent;
                    var aura = (ISpellAura)combatEvent;

                    if (aura.AuraType == BuffType.Buff)
                        PlayerBuffs.AddValue(combatEvent.DestName, spell.SpellName, combatEvent.SourceName);
                    else
                        PlayerDebuffs.AddValue(combatEvent.DestName, spell.SpellName, combatEvent.SourceName);
                    break;

                case LogEvents.SPELL_AURA_BROKEN:
                case LogEvents.SPELL_AURA_REMOVED_DOSE:
                case LogEvents.SPELL_AURA_BROKEN_SPELL:
                    var removedSpell = (ISpell)combatEvent;

                    PlayerBuffs.RemoveValue(combatEvent.DestName, removedSpell.SpellName);
                    PlayerDebuffs.RemoveValue(combatEvent.DestName, removedSpell.SpellName);
                    break;
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {

                }

                _unknown = null;
                _eventCount = null;
                OwnerToEntityMap = null;
                EntitytoOwnerMap = null;
                PlayerBuffs = null;
                PlayerDebuffs = null;
                CurrentFight.Dispose();
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
        }
    }
}