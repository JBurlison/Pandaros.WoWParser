using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWFight : IdEquatable<WoWFight>
    {
        [BsonElement("_id")]
        internal string FightId { get; set; }
        internal string InstanceId { get; set; }
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal List<string> CharacterIds { get; set; } = new List<string>();

        // stats gathered for raid. could be dependant on account.
        // for example HealingStats would be in this list and that would let the UI know to query Healing stats API for this fight, and instance and for each character in character ids.
        internal List<string> StatIndexes { get; set; } = new List<string>();

        public bool EquilIds(WoWFight obj)
        {
            return FightId == obj.FightId;
        }

        public bool EquilIds(string id)
        {
            return FightId == id;
        }

        public string GetId()
        {
            return FightId;
        }
    }
}
