using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels.Stats
{
    public class HealingStats : StatsBase
    {
        /// <summary>
        ///     Actual Life Healed
        /// </summary>
        public long HealingDone { get; set; }

        /// <summary>
        ///     Overhealing
        /// </summary>
        public long Overhealing { get; set; }

        /// <summary>
        ///     Healing crit chance
        /// </summary>
        public float CritChance { get; set; }

        /// <summary>
        ///     Life Healed + Shields
        /// </summary>
        public long EffectiveHeal { get; set; }

        /// <summary>
        ///     Life Healed + Shields + Overheal
        /// </summary>
        public long TotalHealing { get; set; }

        /// <summary>
        ///     Number of healing instances
        ///         ex: every proc of tranquility or healing touch cast or chain heal but NOT hots.
        /// </summary>
        public long HealingInstances { get; set; }

        /// <summary>
        ///     Number of healing instances that crit. You can use this and <see cref="HealingInstances"/> to get crit %
        /// </summary>
        public long CritCount { get; set; }

        /// <summary>
        ///     Life Healed with a critical heal
        /// </summary>
        public long CritHealed { get; set; }

        /// <summary>
        ///     Overheal with a critical heal
        /// </summary>
        public long CritOverheal { get; set; }

        /// <summary>
        ///     Life Healed with a non-critical heal
        /// </summary>
        public long NoncriticalHeal { get; set; }

        /// <summary>
        ///     Overheal with a non-critical heal
        /// </summary>
        public long NoncritOverheal { get; set; }
        
        /// <summary>
        ///     Periodic Life Healed
        /// </summary>
        public long PeriodicHeal { get; set; }

        /// <summary>
        ///     Periodic Life Overheal
        /// </summary>
        public long PeriodicOverheal { get; set; }

        /// <summary>
        ///    Life Healed with pets/totems, such as healing totem
        ///    Key is the pet/totem name
        /// </summary>
        public Dictionary<string, long> PlayerOwnedHealed { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///    Life overhealed with pets/totems, such as healing totem
        ///    Key is the pet/totem name
        /// </summary>
        public Dictionary<string, long> PlayerOwnedOverHealing { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///     Players Healed
        ///     Key 1 is Player healed, Key 2 is the spell name
        /// </summary>
        public Dictionary<string, Dictionary<string, long>> PlayersHealed { get; set; } = new Dictionary<string, Dictionary<string, long>>();

        /// <summary>
        ///   Players Overhealed
        ///   Key 1 is Player healed, Key 2 is the spell name  
        /// </summary>
        public Dictionary<string, Dictionary<string, long>> PlayersOverhealed { get; set; } = new Dictionary<string, Dictionary<string, long>>();

        /// <summary>
        ///     Total Healing output per person (Healed, overhealing and Shields)
        ///     Key 1 is Player healed
        /// </summary>
        public Dictionary<string, long> PlayersHealedTotal { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///   Players Effective Heal (Healed and Shields)
        ///   Key 1 is Player healed
        /// </summary>
        public Dictionary<string, long> EffectiveHealingToPlayers { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///     Life Healed by spell name
        /// </summary>
        public Dictionary<string, long> HealingDoneBySpell { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///     Overhealing by spell name
        /// </summary>
        public Dictionary<string, long> OverhealingDoneBySpell { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///     Shields given by sheild name
        /// </summary>
        public Dictionary<string, long> ShieldsGiven { get; set; } = new Dictionary<string, long>();

        /// <summary>
        ///     Sheilds given to entity
        ///     Key 1 is entity name (such as player or pet), Key 2 is sheild name
        /// </summary>
        public Dictionary<string, Dictionary<string, long>> ShieldsByEntityShielded { get; set; } = new Dictionary<string, Dictionary<string, long>>();
    }
}
