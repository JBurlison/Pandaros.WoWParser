﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pandaros.WoWParser.API.Api.v1.ViewModels;
using Pandaros.WoWParser.API.DomainModels;
using Pandaros.WoWParser.API.DomainModels.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.Api.v1.Controllers
{
    /// <summary>
    ///     
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class HealingController
    {
        private readonly ILogger<UserAccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public HealingController(ILogger<UserAccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets a healing by fight Id
        /// </summary>
        /// <remarks>
        ///     Gets a healing by fight Id, add character id to get the characters specifc healing for the fight.
        /// </remarks>
        /// <response code="201">The healing information</response>
        [HttpPost, Route("GetHealing")]
        [MapToApiVersion("1.0")]
        public HealingStats GetHealing(string fightId, string characterId = "")
        {

            if (!string.IsNullOrEmpty(characterId))
                return new HealingStats()
                {
                    InstanceId = "92183C73-0112-41AC-9441-928EDDFE1E18",
                    CharacterId = "E8C291E6-BCE6-4033-9637-2E6E84045826",
                    FightId = "CF2EC121-83D2-4DBA-92CD-F42E7AA4B81B",
                    CritChance = 25.6f,
                    CritCount = 192,
                    CritHealed = 10984,
                    CritOverheal = 45451,
                    EffectiveHeal = 25604,
                    EffectiveHealingToPlayers = new Dictionary<string, long>()
                    {
                        { "Fireswarn", 19840 }
                    },
                    HealingDone = 2500,
                    HealingDoneBySpell = new Dictionary<string, long>()
                    {
                        { "Chain Heal", 650 },
                        { "Riptide", 2000 }
                    },
                    HealingInstances = 593,
                    NoncriticalHeal = 2000,
                    NoncritOverheal = 19652,
                    Overhealing = 1840,
                    OverhealingDoneBySpell = new Dictionary<string, long>()
                    {
                        { "Chain Heal", 624 },
                        { "Riptide", 5160 }
                    },
                    PeriodicHeal = 845,
                    PeriodicOverheal = 2684,
                    PlayerOwnedHealed = new Dictionary<string, long>()
                    {
                        { "Healing Stream Totem", 1578 }
                    },
                    PlayerOwnedOverHealing = new Dictionary<string, long>()
                    {
                        { "Healing Stream Totem", 5483 }
                    },
                    PlayersHealed = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Riptide", 2000  }, { "Chain Heal", 874 } } }
                    },
                    TotalHealing = 564,
                    PlayersHealedTotal = new Dictionary<string, long>()
                    {
                        { "Fireswarn", 2584 }
                    },
                    PlayersOverhealed = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Riptide", 5356  }, { "Chain Heal", 1728 } } }
                    },
                    ShieldsGiven = new Dictionary<string, long>()
                    {
                        { "Power Word: Shield", 9000 }
                    },
                    ShieldsByEntityShielded = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Power Word: Shield", 9001  }, { "Divine Aegis", 6000 } } }
                    }
                };
            else
                return new HealingStats()
                {
                    InstanceId = "92183C73-0112-41AC-9441-928EDDFE1E18",
                    CharacterId = "E",
                    FightId = "CF2EC121-83D2-4DBA-92CD-F42E7AA4B81B",
                    CritChance = 27.3f,
                    CritCount = 2601,
                    CritHealed = 103984,
                    CritOverheal = 454351,
                    EffectiveHeal = 253604,
                    EffectiveHealingToPlayers = new Dictionary<string, long>()
                    {
                        { "Fireswarn", 149840 }
                    },
                    HealingDone = 2500,
                    HealingDoneBySpell = new Dictionary<string, long>()
                    {
                        { "Chain Heal", 6350 },
                        { "Riptide", 32000 }
                    },
                    HealingInstances = 5953,
                    NoncriticalHeal = 25000,
                    NoncritOverheal = 19652,
                    Overhealing = 15840,
                    OverhealingDoneBySpell = new Dictionary<string, long>()
                    {
                        { "Chain Heal", 5624 },
                        { "Riptide", 51860 }
                    },
                    PeriodicHeal = 845,
                    PeriodicOverheal = 2684,
                    PlayerOwnedHealed = new Dictionary<string, long>()
                    {
                        { "Healing Stream Totem", 10578 }
                    },
                    PlayerOwnedOverHealing = new Dictionary<string, long>()
                    {
                        { "Healing Stream Totem", 59483 }
                    },
                    PlayersHealed = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Riptide", 28000  }, { "Chain Heal", 8784 } } }
                    },
                    TotalHealing = 564,
                    PlayersHealedTotal = new Dictionary<string, long>()
                    {
                        { "Fireswarn", 2584 }
                    },
                    PlayersOverhealed = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Riptide", 95356  }, { "Chain Heal", 12728 } } }
                    },
                    ShieldsGiven = new Dictionary<string, long>()
                    {
                        { "Power Word: Shield", 90400 }
                    },
                    ShieldsByEntityShielded = new Dictionary<string, Dictionary<string, long>>()
                    {
                        { "Fireswarn", new Dictionary<string, long>() { { "Power Word: Shield", 90201  }, { "Divine Aegis", 60050 } } }
                    }
                };
        }
    }
}
