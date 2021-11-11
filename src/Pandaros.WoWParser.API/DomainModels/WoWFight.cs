using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.API.DomainModels
{
    public class WoWFight
    {
        public string InstanceId { get; set; }
        public string FightId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> CharacterIds { get; set; } = new List<string>();

        // stats gathered for raid. could be dependant on account.
        // for example HealingStats would be in this list and that would let the UI know to query Healing stats API for this fight, and instance and for each character in character ids.
        public List<string> StatIndexes { get; set; } = new List<string>();
    }
}
