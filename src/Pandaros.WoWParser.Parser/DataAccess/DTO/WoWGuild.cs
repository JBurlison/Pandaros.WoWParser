using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.DataAccess.DTO
{
    [BsonIgnoreExtraElements]
    internal class WoWGuild : IdEquatable<WoWGuild>
    {
        [BsonElement("_id")]
        internal string ID { get; set; }
        internal string Name { get; set; }
        internal string URL { get; set; }
        internal string ServerID { get; set; }
        internal List<string> Owners { get; set; } = new List<string>();
        internal List<string> Ranks { get; set; } = new List<string>();

        // key rank name, value list of player ids
        internal Dictionary<string, List<string>> PlayersByRank { get; set; } = new Dictionary<string, List<string>>();

        // key: Instance name, Instance date, instance id
        internal Dictionary<string, Dictionary<string, string>> InstacneIds { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public bool EquilIds(WoWGuild obj)
        {
            return ID == obj.ID;
        }

        public bool EquilIds(string id)
        {
            return ID == id;
        }

        public string GetId()
        {
            return ID;
        }
    }
}
