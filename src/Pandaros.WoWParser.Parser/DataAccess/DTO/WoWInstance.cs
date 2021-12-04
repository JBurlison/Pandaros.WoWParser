using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWInstance : IdEquatable<WoWInstance>
    {
        [BsonElement("_id")]
        internal string InstanceId { get; set; }
        internal string InstanceName { get; set; }
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal List<string> CharacterIds { get; set; } = new List<string>();
        internal List<string> GuildIds { get; set; } = new List<string>();
        /// <summary>
        ///  In order of fights that happend.
        /// </summary>
        internal List<string> FightIds { get; set; } = new List<string>();

        public bool EquilIds(WoWInstance obj)
        {
            return InstanceId == obj.InstanceId;
        }

        public bool EquilIds(string id)
        {
            return InstanceId == id;
        }

        public string GetId()
        {
            return InstanceId;
        }
    }
}
